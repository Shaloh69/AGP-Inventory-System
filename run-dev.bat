@echo off
REM Double-click, or run from a terminal in this folder:
REM   Command Prompt:        run-dev [all|setup|check|server|client|clean] [page]
REM   PowerShell / VS Code:  .\run-dev.bat [all|setup|check|server|client|clean] [page]
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0run-dev.ps1" %*
