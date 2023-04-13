#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . .
WORKDIR "./Achery/"
RUN dotnet restore "Archery.Api/Archery.Api.csproj"
RUN dotnet build "Archery.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Archery.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Archery.Api.dll"]