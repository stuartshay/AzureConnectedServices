ARG APPLICATION_NAME='AzureConnectedServices.web'
ARG BUILD_DATE='1/1/2022'
ARG BUILD_NUMBER=0

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

LABEL org.label-schema.application=${APPLICATION_NAME}
LABEL org.label-schema.build-date=${BUILD_DATE}
LABEL org.label-schema.version=6.0.1.${BUILD_NUMBER}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV PATH="/root/.dotnet/tools:${PATH}"
RUN dotnet tool install --global dotnet-setversion --version 2.4.0
RUN dotnet tool list -g

WORKDIR /src
COPY ["src/AzureConnectedServices.WebApi/AzureConnectedServices.WebApi.csproj", "src/AzureConnectedServices.WebApi/"]
RUN dotnet restore "src/AzureConnectedServices.WebApi/AzureConnectedServices.WebApi.csproj"
COPY . .
WORKDIR "/src/src/AzureConnectedServices.WebApi"
RUN dotnet build "AzureConnectedServices.WebApi.csproj" -c Release -o /app/build


FROM build AS publish
RUN setversion 6.0.1.${BUILD_NUMBER}
RUN dotnet publish "AzureConnectedServices.WebApi.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureConnectedServices.WebApi.dll"]
