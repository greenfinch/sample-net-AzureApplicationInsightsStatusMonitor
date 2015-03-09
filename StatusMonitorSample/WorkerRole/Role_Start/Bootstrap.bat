:: The basis for this script is described here
:: http://blogs.msdn.com/b/visualstudioalm/archive/2014/04/16/new-agent-for-application-insights-available.aspx
:: The scripts can be downloaded directly from
:: http://go.microsoft.com/fwlink/?LinkID=329971

@echo off

:: Do not attempt to install the agent on an emulated environment
if "%EMULATED%"=="true" goto :EndOfScript

:: Set appropriate execution policy on the host machine
set ExecutionPolicyLevel=RemoteSigned
for /F "usebackq" %%i in (`powershell -noprofile -command "Get-ExecutionPolicy"`) do (
    set ExecutionPolicy=%%i
    if /I "%%i"=="Unrestricted" goto :AllIsWell
    if /I "%%i"=="RemoteSigned" goto :AllIsWell
    Powershell.exe -NoProfile -Command "Set-ExecutionPolicy RemoteSigned" < NUL >> NUL 2>> NUL
)

:AllIsWell

Powershell.exe -NoProfile -Command "& '%~dp0InstallStatusMonitor.ps1'" < NUL  >> NUL 2>> NUL
echo "done" >"%ROLEROOT%\startup.task.done.sem"

:EndOfScript

exit 0