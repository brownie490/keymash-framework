@echo off 
set ProjectTitle="KeyMash Test Page"
set JIRAProject=WCC
set JIRALink=https://jira.wcgclinical.com/browse/%JIRAProject%-{id}
set tmpProjectTitle=%ProjectTitle%
set tmpProjectTitle=%tmpProjectTitle:"=%
set tmpProjectTitle=%tmpProjectTitle: =%
set OutputLocation=%CD%/%tmpProjectTitle%.html

@echo on
livingdoc test-assembly ..\bin\Debug\net5.0\SkeletonFramework.dll -t ..\bin\Debug\net5.0\TestExecution*.json --title %ProjectTitle% --output %OutputLocation% --work-item-prefix %JIRAProject%- --work-item-url-template %JIRALink%