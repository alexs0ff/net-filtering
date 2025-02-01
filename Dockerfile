#Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env 

WORKDIR /src
COPY ./ShopApp ./ShopApp
COPY ./RaCruds ./RaCruds

RUN dotnet publish ./ShopApp/ShopApp.csproj -c Release -o /published/Web /p:UseAppHost=false


#Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app

COPY --from=build-env /published/Web ./

ENTRYPOINT ["dotnet", "./ShopApp.dll"]
