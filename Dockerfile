# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./API/API.csproj ./API/
COPY ./DataLayer/DataLayer.csproj ./DataLayer/
COPY ./ServiceLayer/ServiceLayer.csproj ./ServiceLayer/

RUN dotnet restore ./API/API.csproj

COPY . ./

RUN dotnet publish ./API/API.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /out ./

EXPOSE 5086

ENTRYPOINT ["dotnet", "API.dll"]
