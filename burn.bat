@echo off
setlocal

set SITE=%1

if NOT DEFINED SITE (
    echo Site name is required.
    exit /b 1
)
set URL=http://%SITE%.azurewebsites.net/
set LOOP=5
set I=0

:loop
curl.exe %URL%
sleep 300

set /a I=%I%+1
if /i %I% lss %LOOP% goto :loop

echo Done.
