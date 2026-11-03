<#
  run-dev.ps1 - run the whole project on your PC for testing.

  Usage (from the repository root):
      .\run-dev.bat                  check tools, start the server, then the C# app
      .\run-dev.bat setup            first time only: npm install + create tables + sample data
      .\run-dev.bat check            only check that tools and config files are ready
      .\run-dev.bat server           only start the Express server (http://localhost:3000)
      .\run-dev.bat client           only start the C# app (pointed at localhost)
      .\run-dev.bat client Products  start the C# app on a specific page (Home, Products, MahApps ...)
      .\run-dev.bat clean            delete the C# build folders (bin/obj) - fixes most strange build errors
      .\run-dev.bat install          install missing tools (Git, Node.js LTS, .NET 10 SDK) with winget

  In VS Code / PowerShell type .\run-dev.bat (with .\ in front). In Command Prompt run-dev.bat also works.
#>
param(
    [ValidateSet("all", "setup", "check", "server", "client", "clean", "install")]
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
    Write-Bad "$name is not installed - run: .\run-dev.bat install   (or see Section 3 of docs/Beginners-Guide.pdf)"
    return $false
}

# The tools this project needs, with their winget package ids and download pages.
$Tools = @(
    @{ Name = "Git";          Command = "git";    Id = "Git.Git";                 Url = "https://git-scm.com/download/win" },
    @{ Name = "Node.js LTS";  Command = "node";   Id = "OpenJS.NodeJS.LTS";       Url = "https://nodejs.org" },
    @{ Name = ".NET 10 SDK";  Command = "dotnet"; Id = "Microsoft.DotNet.SDK.10"; Url = "https://dotnet.microsoft.com/download/dotnet/10.0" }
)

function Test-DotnetSdk10 {
    if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) { return $false }
    $sdks = @(dotnet --list-sdks 2>$null | ForEach-Object { ($_ -split ' ')[0] })
    return [bool]($sdks | Where-Object { [int]($_ -split '\.')[0] -ge 10 })
}

function Install-Tools {
    Write-Step "Installing missing tools"
    if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
        Write-Bad "winget is not available on this PC. Install the tools from these pages instead:"
        $Tools | ForEach-Object { Write-Host "    $($_.Name): $($_.Url)" }
        return
    }
    $installed = 0
    foreach ($tool in $Tools) {
        $present = if ($tool.Command -eq "dotnet") { Test-DotnetSdk10 } else { [bool](Get-Command $tool.Command -ErrorAction SilentlyContinue) }
        if ($present) { Write-Ok "$($tool.Name) already installed"; continue }
        Write-Host "  Installing $($tool.Name) (a Windows permission prompt may appear)..."
        winget install --id $tool.Id -e --accept-package-agreements --accept-source-agreements
        if ($LASTEXITCODE -eq 0) { Write-Ok "$($tool.Name) installed"; $installed++ }
        else { Write-Bad "$($tool.Name) failed - install it from $($tool.Url)" }
    }
    if ($installed -gt 0) {
        Write-Host "`n  IMPORTANT: close this terminal (and VS Code) and open a new one, so Windows finds the new tools." -ForegroundColor Yellow
        Write-Host "  Then run: .\run-dev.bat check" -ForegroundColor Yellow
    }
}

function Invoke-Check {
    Write-Step "Checking tools"
    $ok = $true
    $ok = (Test-Tool "git"    { git --version })      -and $ok
    $ok = (Test-Tool "node"   { node -v })            -and $ok
    $ok = (Test-Tool "npm"    { npm -v })             -and $ok
    $ok = (Test-Tool "dotnet" { dotnet --version })   -and $ok

    Write-Step "Checking versions"
    # The C# app targets .NET 10, so an older SDK cannot build it.
    $sdks = @()
    if (Get-Command dotnet -ErrorAction SilentlyContinue) { $sdks = @(dotnet --list-sdks 2>$null | ForEach-Object { ($_ -split ' ')[0] }) }
    $sdk10 = $sdks | Where-Object { [int]($_ -split '\.')[0] -ge 10 } | Select-Object -Last 1
    if ($sdk10) { Write-Ok ".NET SDK $sdk10" }
    else {
        $found = if ($sdks) { "found: $($sdks -join ', ')" } else { "no .NET SDK installed" }
        Write-Bad ".NET 10 SDK needed ($found) - run: .\run-dev.bat install"
        $ok = $false
    }
    $nodeMajor = 0
    if (Get-Command node -ErrorAction SilentlyContinue) {
        [void][int]::TryParse(((node -v 2>$null) -replace '^v(\d+).*', '$1'), [ref]$nodeMajor)
    }
    if ($nodeMajor -ge 20) { Write-Ok "Node.js $nodeMajor (20 or newer needed)" }
    elseif ($nodeMajor -gt 0) { Write-Bad "Node.js $nodeMajor is too old - install the LTS version from https://nodejs.org"; $ok = $false }

    if (-not $ok) { Write-Host "`n  Fix: .\run-dev.bat install   then close and reopen the terminal." -ForegroundColor Yellow }

    Write-Step "Checking project files"
    $envFile = Join-Path $ServerDir ".env"
    if (Test-Path $envFile) { Write-Ok "server/.env" }
    else { Write-Warn "server/.env missing - copy server/.env.example to server/.env and fill it in" }

    $localDb = (Test-Path $envFile) -and (Select-String -Path $envFile -Pattern '^\s*DB_SSL\s*=\s*false' -Quiet)
    if (Test-Path (Join-Path $ServerDir "certs\ca.pem")) { Write-Ok "server/certs/ca.pem" }
    elseif ($localDb) { Write-Ok "DB_SSL=false (local database, no certificate needed)" }
    else { Write-Warn "server/certs/ca.pem missing - download the CA certificate from Aiven" }

    if (Test-Path (Join-Path $ServerDir "node_modules")) { Write-Ok "server packages installed" }
    else { Write-Warn "server packages not installed - run: .\run-dev.bat setup" }

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

    # Without .env the server cannot connect to the database, so do not start it at all.
    if (-not (Test-Path (Join-Path $ServerDir ".env"))) {
        Write-Warn "Skipped: server\.env does not exist yet."
        Write-Host "         1. Copy server\.env.example to server\.env and paste your Aiven DATABASE_URL"
        Write-Host "         2. Save the Aiven CA certificate as server\certs\ca.pem"
        Write-Host "         3. Run: .\run-dev.bat setup"
        Write-Host "         The app will still open; Home will say the server is not reachable."
        return $false
    }

    if (-not (Test-Path (Join-Path $ServerDir "node_modules"))) {
        Write-Host "  Installing server packages (first run only)..."
        Push-Location $ServerDir; npm install; Pop-Location
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

function Clear-ClientBuild {
    Write-Step "Deleting C# build folders (bin / obj)"
    dotnet build-server shutdown *> $null
    Get-ChildItem $ClientDir -Directory -Recurse -Include bin, obj -ErrorAction SilentlyContinue |
        ForEach-Object { Remove-Item $_.FullName -Recurse -Force -ErrorAction SilentlyContinue; Write-Ok "deleted $($_.FullName.Replace($Root, '.'))" }
}

# Builds the app. If the build fails (for example "Object reference not set to an instance of an object"
# from old bin/obj folders copied from another PC), clean once and try again, then show the real errors.
function Invoke-ClientBuild {
    Write-Step "Building the C# app"
    $solution = Join-Path $ClientDir "AppClient.slnx"
    for ($attempt = 1; $attempt -le 2; $attempt++) {
        $output = @(dotnet build $solution -nologo -v q 2>&1 | ForEach-Object { "$_" })
        if ($LASTEXITCODE -eq 0) { Write-Ok "Build succeeded"; return $true }
        if ($attempt -eq 1) {
            Write-Warn "Build failed - cleaning old build files and trying once more"
            Clear-ClientBuild
        }
    }
    Write-Bad "Build failed. The errors were:"
    $output | Where-Object { $_ -match 'error' } | Select-Object -Unique -First 15 | ForEach-Object { Write-Host "    $_" -ForegroundColor Red }
    Write-Host "  Tips: run .\run-dev.bat check, close Visual Studio, then try again. See Section 23 of the guide." -ForegroundColor Yellow
    return $false
}

function Start-Client {
    if (-not (Invoke-ClientBuild)) { return }
    Write-Step "Starting the C# app"
    # AppConfig.cs reads API_BASE_URL first, so the app talks to the local server.
    $env:API_BASE_URL = "http://localhost:$Port/"
    Write-Ok "API_BASE_URL = $env:API_BASE_URL"
    $appArgs = @()
    if ($Page) { $appArgs = @("--", "--page", $Page); Write-Ok "Opening page: $Page" }
    dotnet run --no-build --project (Join-Path $ClientDir "AppClient\AppClient.csproj") @appArgs
}

switch ($Target) {
    "setup"  { Invoke-Setup }
    "check"  { Invoke-Check | Out-Null }
    "server" { Start-Server | Out-Null }
    "client" { Start-Client }
    "clean"  { Clear-ClientBuild }
    "install" { Install-Tools }
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
