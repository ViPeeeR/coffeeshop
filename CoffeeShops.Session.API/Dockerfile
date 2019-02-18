FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CoffeeShops.Session.API/CoffeeShops.Session.API.csproj", "CoffeeShops.Session.API/"]
RUN dotnet restore "CoffeeShops.Session.API/CoffeeShops.Session.API.csproj"
COPY . .
WORKDIR "/src/CoffeeShops.Session.API"
RUN dotnet build "CoffeeShops.Session.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoffeeShops.Session.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoffeeShops.Session.API.dll"]