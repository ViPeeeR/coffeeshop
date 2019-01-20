FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CoffeeShops/CoffeeShops.csproj", "CoffeeShops/"]
RUN dotnet restore "CoffeeShops/CoffeeShops.csproj"
COPY . .
WORKDIR "/src/CoffeeShops"
RUN dotnet build "CoffeeShops.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoffeeShops.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoffeeShops.dll"]