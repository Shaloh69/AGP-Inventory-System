<#
  Opens the client on every page (AppClient.exe --page <name>), takes a screenshot of the
  window and saves it under docs/screenshots/. Start the server first (run-dev.bat server)
  so the Home and Products pages show real data.

  Usage (from the repository root):
      powershell -ExecutionPolicy Bypass -File client\capture-pages.ps1
#>
param(
    [string]$ApiBaseUrl = "http://localhost:3000/",
    [int]$RenderDelayMs = 4000,
    [string[]]$Only = @()          # e.g. -Only "Products","WPF UI"
)

$ErrorActionPreference = "Stop"
$Docs = Join-Path $PSScriptRoot "..\docs\screenshots"

# page name in the menu  ->  screenshot file
$pages = [ordered]@{
    "Home"           = "app\home.png"
    "First function" = "app\first-function.png"
    "Products"       = "app\products.png"
    "MaterialSkin"   = "hello-world\WinForms.MaterialSkin2.png"
    "ReaLTaiizor"    = "hello-world\WinForms.ReaLTaiizor.png"
    "Krypton"        = "hello-world\WinForms.Krypton.png"
    "MaterialDesign" = "hello-world\Wpf.MaterialDesign.png"
    "WPF UI"         = "hello-world\Wpf.WpfUi.png"
    "MahApps"        = "hello-world\Wpf.MahApps.png"
    "HandyControl"   = "hello-world\Wpf.HandyControl.png"
}

Add-Type -AssemblyName System.Drawing
Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class Win {
    [StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left, Top, Right, Bottom; }
    [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
    [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr h, IntPtr after, int x, int y, int cx, int cy, uint flags);
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
    [DllImport("dwmapi.dll")] public static extern int DwmGetWindowAttribute(IntPtr h, int attr, out RECT r, int size);
}
"@
[Win]::SetProcessDPIAware() | Out-Null

Write-Host "Building client..." -ForegroundColor Cyan
dotnet build (Join-Path $PSScriptRoot "AppClient.slnx") -nologo -v q | Out-Null
if ($LASTEXITCODE -ne 0) { throw "Build failed. Run 'dotnet build' in client/ to see the errors." }
$exe = Get-ChildItem (Join-Path $PSScriptRoot "AppClient\bin\Debug") -Recurse -Filter AppClient.exe | Select-Object -First 1

$env:API_BASE_URL = $ApiBaseUrl
foreach ($page in $pages.Keys) {
    if ($Only.Count -gt 0 -and $Only -notcontains $page) { continue }
    $file = Join-Path $Docs $pages[$page]
    New-Item -ItemType Directory -Force (Split-Path $file) | Out-Null

    $proc = Start-Process $exe.FullName -ArgumentList "--page", "`"$page`"" -PassThru
    $deadline = (Get-Date).AddSeconds(30)
    while ($proc.MainWindowHandle -eq 0 -and (Get-Date) -lt $deadline -and -not $proc.HasExited) {
        Start-Sleep -Milliseconds 300; $proc.Refresh()
    }
    if ($proc.HasExited -or $proc.MainWindowHandle -eq 0) { Write-Warning "$page did not open"; continue }

    $h = $proc.MainWindowHandle
    [Win]::SetWindowPos($h, [IntPtr](-1), 0, 0, 0, 0, 0x0003) | Out-Null   # topmost
    [Win]::SetForegroundWindow($h) | Out-Null
    [Win]::SetCursorPos(5, 5) | Out-Null                                  # keep the mouse off the menu
    Start-Sleep -Milliseconds $RenderDelayMs

    $r = New-Object Win+RECT
    [Win]::DwmGetWindowAttribute($h, 9, [ref]$r, 16) | Out-Null          # visible window bounds
    $bmp = New-Object System.Drawing.Bitmap ($r.Right - $r.Left), ($r.Bottom - $r.Top)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.CopyFromScreen($r.Left, $r.Top, 0, 0, $bmp.Size)
    $bmp.Save($file, [System.Drawing.Imaging.ImageFormat]::Png)
    $g.Dispose(); $bmp.Dispose()

    if (-not ($proc.CloseMainWindow() -and $proc.WaitForExit(3000))) { $proc.Kill() }
    Write-Host "Saved $file" -ForegroundColor Green
}
