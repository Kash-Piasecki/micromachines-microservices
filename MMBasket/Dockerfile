FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MMBasket/MMBasket.csproj", "MMBasket/"]
RUN dotnet restore "MMBasket/MMBasket.csproj"
COPY . .
WORKDIR "/src/MMBasket"
RUN dotnet build "MMBasket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MMBasket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMBasket.dll"]
