@echo off
echo ========================================
echo   TECHFUTURE CRM - INICIANDO SISTEMA
echo ========================================
echo.
echo Abrindo Backend...
start powershell -NoExit -Command "cd /d 'C:\Users\Emilly\OneDrive\Documentos\SA- 3\backend' && dotnet run"
timeout /t 3 /nobreak >nul
echo.
echo Abrindo Frontend...
start powershell -NoExit -Command "cd /d 'C:\Users\Emilly\OneDrive\Documentos\SA- 3\frontend' && dotnet run"
echo.
echo ? Sistemas iniciados!
echo Backend: http://localhost:5000
echo.
pause
