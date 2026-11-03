@echo off
REM Double-click or run from a terminal:  run-dev [all|setup|check|server|client] [page]
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0run-dev.ps1" %*
