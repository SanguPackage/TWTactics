@echo off
cls
REM XSD.exe needs to be in a known locaton!
REM The WorldSettings.designer.cs is generated with xsd2code (https://xsd2code.codeplex.com/)

xsd WorldSettings.xml

echo.
echo This batch file updated WorldSettings.xsd
echo Now right click it in VS to regenerate WorldSettings.designer.cs

pause