FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CoffeeShops.Orders.API/CoffeeShops.Orders.API.csproj", "CoffeeShops.Orders.API/"]
RUN dotnet restore "CoffeeShops.Orders.API/CoffeeShops.Orders.API.csproj"
COPY . .
WORKDIR "/src/CoffeeShops.Orders.API"
RUN dotnet build "CoffeeShops.Orders.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoffeeShops.Orders.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoffeeShops.Orders.API.dll"]