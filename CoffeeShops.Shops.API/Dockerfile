FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CoffeeShops.Shops.API/CoffeeShops.Shops.API.csproj", "CoffeeShops.Shops.API/"]
RUN dotnet restore "CoffeeShops.Shops.API/CoffeeShops.Shops.API.csproj"
COPY . .
WORKDIR "/src/CoffeeShops.Shops.API"
RUN dotnet build "CoffeeShops.Shops.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoffeeShops.Shops.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoffeeShops.Shops.API.dll"]