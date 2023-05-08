FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY Services/Caching/*.csproj Services/Caching/
COPY Shared/Messages/BlogSite.Messages/*.csproj Shared/Messages/BlogSite.Messages/
RUN dotnet restore Services/Caching/*.csproj 
COPY . .

RUN dotnet publish Services/Caching/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Caching.dll"]
