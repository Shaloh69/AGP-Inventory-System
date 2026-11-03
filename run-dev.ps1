<#
  run-dev.ps1 – run the whole project on your PC for testing.

  Usage (from the repository root):
      .\run-dev.bat                  check tools, start the server, then the C# app
      .\run-dev.bat setup            first time only: npm install + create tables + sample data
      .\run-dev.bat check            only check that tools and config files are ready
      .\run-dev.bat server           only start the Express server (http://localhost:3000)
      .\run-dev.bat client           only start the C# app (pointed at localhost)
      .\run-dev.bat client Products  start the C# app on a specific page (Home, Products, MahApps ...)
#>
param(
    [ValidateSet("all", "setup", "check", "server", "client")]
    [string]$Target = "all",
    [string]$Page = ""
)

$Root      = $PSScriptRoot
$ServerDir = Join-Path $Root "server"
$ClientDir = Join-Path $Root "client"
$Port      = 3000
$HealthUrl = "http://localhost:$Port/api/health"

function Write-Step($text) { Write-Host "`n==> $text" -ForegroundColor Cyan }
function Write-Ok($text)   { Write-Host "  [ok]   $text" -ForegroundColor Green }
function Write-Warn($text) { Write-Host "  [warn] $text" -ForegroundColor Yellow }
function Write-Bad($text)  { Write-Host "  [miss] $text" -ForegroundColor Red }

function Test-Tool($name, $command) {
    try {
        $output = @(& $command 2>$null)
        if ($LASTEXITCODE -eq 0 -and $output) { Write-Ok "$name $($output[0])"; return $true }
    } catch { }
    Write-Bad "$name is not installed (see Section 3 of docs/Beginners-Guide.pdf)"
    return $false
}

function Invoke-Check {
    Write-Step "Checking tools"
    $ok = $true
    $ok = (Test-Tool "git"    { git --version })      -and $ok
    $ok = (Test-Tool "node"   { node -v })            -and $ok
    $ok = (Test-Tool "npm"    { npm -v })             -and $ok
    $ok = (Test-Tool "dotnet" { dotnet --version })   -and $ok

    Write-Step "Checking project files"
    $envFile = Join-Path $ServerDir ".env"
    if (Test-Path $envFile) { Write-Ok "server/.env" }
    else { Write-Warn "server/.env missing - copy server/.env.example to server/.env and fill it in" }

    $localDb = (Test-Path $envFile) -and (Select-String -Path $envFile -Pattern '^\s*DB_SSL\s*=\s*false' -Quiet)
    if (Test-Path (Join-Path $ServerDir "certs\ca.pem")) { Write-Ok "server/certs/ca.pem" }
    elseif ($localDb) { Write-Ok "DB_SSL=false (local database, no certificate needed)" }
    else { Write-Warn "server/certs/ca.pem missing - download the CA certificate from Aiven" }

    if (Test-Path (Join-Path $ServerDir "node_modules")) { Write-Ok "server packages installed" }
    else { Write-Warn "server packages not installed - run: run-dev.bat setup" }

    return $ok
}

function Invoke-Setup {
    Write-Step "Installing server packages"
    Push-Location $ServerDir
    try {
        npm install
        if (-not (Test-Path ".env")) {
            Write-Warn "server/.env missing - copy .env.example to .env, fill it in, then run setup again"
            return
        }
        Write-Step "Creating tables (database/migrations)"
        npm run db:migrate
        Write-Step "Adding sample data (database/seeds)"
        npm run db:seed
    } finally { Pop-Location }

    Write-Step "Restoring C# packages"
    dotnet restore (Join-Path $ClientDir "AppClient.slnx")
}

function Test-ServerUp {
    try { (Invoke-WebRequest $HealthUrl -UseBasicParsing -TimeoutSec 2).StatusCode -eq 200 } catch { $false }
}

function Start-Server {
    Write-Step "Starting the Express server"
    if (Test-ServerUp) { Write-Ok "Server already running at $HealthUrl"; return $true }
    if (-not (Test-Path (Join-Path $ServerDir "node_modules"))) {
        Write-Warn "Server packages are not installed - run: run-dev.bat setup"
        return $false
    }

    # Run the server in its own window so its logs stay visible. Close that window to stop it.
    Start-Process powershell -WorkingDirectory $ServerDir `
        -ArgumentList "-NoExit", "-Command", "`$host.UI.RawUI.WindowTitle='AGP API (npm run dev)'; npm run dev"

    Write-Host "  Waiting for $HealthUrl ..."
    for ($i = 0; $i -lt 30; $i++) {
        if (Test-ServerUp) { Write-Ok "Server is up: $HealthUrl"; return $true }
        Start-Sleep -Seconds 1
    }
    Write-Warn "Server did not answer within 30 s - check the server window for errors"
    return $false
}

function Start-Client {
    Write-Step "Starting the C# app"
    # AppConfig.cs reads API_BASE_URL first, so the app talks to the local server.
    $env:API_BASE_URL = "http://localhost:$Port/"
    Write-Ok "API_BASE_URL = $env:API_BASE_URL"
    $appArgs = @()
    if ($Page) { $appArgs = @("--", "--page", $Page); Write-Ok "Opening page: $Page" }
    dotnet run --project (Join-Path $ClientDir "AppClient\AppClient.csproj") @appArgs
}

switch ($Target) {
    "setup"  { Invoke-Setup }
    "check"  { Invoke-Check | Out-Null }
    "server" { Start-Server | Out-Null }
    "client" { Start-Client }
    "all" {
        if (-not (Invoke-Check)) { Write-Host "`nInstall the missing tools first." -ForegroundColor Red; exit 1 }
        Start-Server | Out-Null
        Start-Client
    }
}

# When run-dev.bat was double-clicked (powershell <- cmd <- explorer), keep the window open
try {
    $cmdPid = (Get-CimInstance Win32_Process -Filter "ProcessId=$PID").ParentProcessId
    $starter = (Get-CimInstance Win32_Process -Filter "ProcessId=$cmdPid").ParentProcessId
    if ((Get-Process -Id $starter -ErrorAction Stop).Name -eq "explorer") { Read-Host "`nPress Enter to close" | Out-Null }
} catch { }
