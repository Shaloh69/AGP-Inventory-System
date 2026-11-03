@echo off
REM Runs run-dev.ps1. Double-click it, or type in a terminal opened in this folder:
REM   PowerShell or VS Code:  .\run-dev.bat check
REM   Command Prompt:         run-dev.bat check
REM Commands: install, check, setup, server, client, clean (no command = server + app)
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0run-dev.ps1" %*
