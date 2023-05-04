
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY Services/Caching/*.csproj Services/Caching/
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT [ "dotnet","Caching.dll" ]