@echo off

start cmd /k "cd .\Archery.Web && npm run serve"
start cmd /k "cd .\Archery.Api && dotnet watch"