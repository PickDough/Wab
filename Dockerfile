FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish ./Wab.Api -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
    
ENTRYPOINT ["dotnet", "Wab.Api.dll"]