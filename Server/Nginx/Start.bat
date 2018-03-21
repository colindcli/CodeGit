@echo off
cd /d C:\nginx-1.13.9\nginx-1.13.9
TASKKILL /F /IM nginx.exe /T
start "" "nginx.exe"
cls
ping localhost -n 1 > nul
tasklist | findstr /i "nginx.exe"
cls
if %errorlevel% == 0 (echo started) else (echo error...)