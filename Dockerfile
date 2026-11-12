FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore InventoryManagement.slnx

RUN dotnet publish InventoryManagement.APIs/InventoryManagement.APIs.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 10000

ENTRYPOINT ["sh", "-c", "dotnet InventoryManagement.APIs.dll --urls http://0.0.0.0:${PORT:-10000}"]
