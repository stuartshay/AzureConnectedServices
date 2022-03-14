
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AzureConnectedServices/AzureConnectedServices.csproj", "src/AzureConnectedServices/"]
RUN dotnet restore "src/AzureConnectedServices/AzureConnectedServices.csproj"
COPY . .
WORKDIR "/src/src/AzureConnectedServices"
RUN dotnet build "AzureConnectedServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzureConnectedServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureConnectedServices.dll"]
