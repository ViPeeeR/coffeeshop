FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CoffeeShops.Users.API/CoffeeShops.Users.API.csproj", "CoffeeShops.Users.API/"]
RUN dotnet restore "CoffeeShops.Users.API/CoffeeShops.Users.API.csproj"
COPY . .
WORKDIR "/src/CoffeeShops.Users.API"
RUN dotnet build "CoffeeShops.Users.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoffeeShops.Users.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoffeeShops.Users.API.dll"]