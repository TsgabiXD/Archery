# Archery
Schulprojekt

## After Git-Clone:
> dotnet tool install --global dotnet-ef

## Bei Änderungen im Archery.Model:
remove Migrations Folder in Archery.Api & go to Archery.Api and execute:
> dotnet ef migrations add Init

## Azure Publish via:
> dotnet publish -c Release -o ./bin/Publish
und den Ordner dann auf den App Service deployen