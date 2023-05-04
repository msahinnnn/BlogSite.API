FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Caching.dll"]




# FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
# WORKDIR /app
 
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY *.sln ./
# COPY . *.csproj /
# RUN dotnet restore /*.csproj
# COPY . .
# WORKDIR /src/
# RUN dotnet build -c Release -o /app
 
# FROM build AS publish
# RUN dotnet publish -c Release -o /app
 
# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/ .
# ENTRYPOINT ["dotnet", "Caching.dll"]


# FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
# WORKDIR /app


# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ./*.csproj ./
# RUN dotnet restore ./*.csproj
# COPY . .
# WORKDIR "/src/Services/Caching"
# RUN dotnet build ./*.csproj -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish ./*.csproj -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/ .
# ENTRYPOINT ["dotnet", "Caching.dll"]


# FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
# WORKDIR /app

# COPY ./*.csproj ./
# RUN dotnet restore
# COPY . .
# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build /publish .
# ENTRYPOINT [ "dotnet","Caching.dll" ]