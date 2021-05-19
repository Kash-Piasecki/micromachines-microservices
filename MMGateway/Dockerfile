FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MMGateway/MMGateway.csproj", "MMGateway/"]
RUN dotnet restore "MMGateway/MMGateway.csproj"
COPY . .
WORKDIR "/src/MMGateway"
RUN dotnet build "MMGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MMGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMGateway.dll"]
