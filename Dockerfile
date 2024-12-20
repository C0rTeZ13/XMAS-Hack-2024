
FROM mcr.microsoft.com/dotnet/sdk:8.0 

WORKDIR /app

COPY ./API/API.csproj ./API/
COPY ./DataLayer/DataLayer.csproj ./DataLayer/
COPY ./ServiceLayer/ServiceLayer.csproj ./ServiceLayer/

RUN dotnet restore ./API/API.csproj

COPY . ./

ENTRYPOINT ["dotnet", "run", "--project", "API/API.csproj", "--configuration", "Release"]

EXPOSE 5086
