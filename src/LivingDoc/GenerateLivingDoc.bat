@echo off 
set ProjectTitle="KeyMash Test Page"
set JIRAProject=AUT
set JIRALink=https://keymash.atlassian.net/browse/%JIRAProject%-{id}
set tmpProjectTitle=%ProjectTitle%
set tmpProjectTitle=%tmpProjectTitle:"=%
set tmpProjectTitle=%tmpProjectTitle: =%
set OutputLocation=%CD%/Test Evidence/%tmpProjectTitle%.html

@echo on
mkdir "Test Evidence"
livingdoc test-assembly ..\bin\Debug\net5.0\KeyMashFramework.dll -t ..\bin\Debug\net5.0\TestExecution*.json --title %ProjectTitle% --output "%OutputLocation%" --work-item-prefix %JIRAProject%- --work-item-url-template %JIRALink%
move "..\TestResults\*.png" ".\Test Evidence\"
move "..\TestResults\*.json" ".\Test Evidence\"