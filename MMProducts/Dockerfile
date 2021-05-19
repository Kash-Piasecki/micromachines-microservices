FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MMProducts/MMProducts.csproj", "MMProducts/"]
RUN dotnet restore "MMProducts/MMProducts.csproj"
COPY . .
WORKDIR "/src/MMProducts"
RUN dotnet build "MMProducts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MMProducts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MMProducts.dll"]
