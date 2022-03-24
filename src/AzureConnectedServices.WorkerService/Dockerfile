#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0.201 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AzureConnectedServices.WorkerService/AzureConnectedServices.WorkerService.csproj", "src/AzureConnectedServices.WorkerService/"]
RUN dotnet restore "src/AzureConnectedServices.WorkerService/AzureConnectedServices.WorkerService.csproj"
COPY . .
WORKDIR "/src/src/AzureConnectedServices.WorkerService"
RUN dotnet build "AzureConnectedServices.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzureConnectedServices.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final

RUN cat /etc/os-release
RUN dotnet --version

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureConnectedServices.WorkerService.dll"]
