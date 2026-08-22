@echo off
setlocal
cd /d "%~dp0"

title Crasher Unlocker V1.2 - Build

set "CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe"
if not exist "%CSC%" set "CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe"

if not exist "%CSC%" (
  echo.
  echo ERROR: Windows .NET Framework C# compiler was not found.
  echo Enable/install .NET Framework 4.x, then run this file again.
  echo.
  pause
  exit /b 1
)

if not exist "%~dp0CUI.ico" (
  echo.
  echo ERROR: CUI.ico is missing from this folder.
  echo.
  pause
  exit /b 1
)

echo.
echo Building Crasher Unlocker V1.2...
echo.

"%CSC%" /nologo /target:winexe /platform:anycpu /optimize+ /debug- ^
  /out:"%~dp0Crasher Unlocker V1.2.exe" ^
  /win32icon:"%~dp0CUI.ico" ^
  /reference:System.dll ^
  /reference:System.Core.dll ^
  /reference:System.Drawing.dll ^
  /reference:System.Windows.Forms.dll ^
  "%~dp0CrasherUnlocker.cs"

if errorlevel 1 (
  echo.
  echo BUILD FAILED.
  pause
  exit /b 1
)

echo.
echo ==========================================
echo  Crasher Unlocker V1.2 build complete
echo ==========================================
echo.
echo Output:
echo "%~dp0Crasher Unlocker V1.2.exe"
echo.
pause
