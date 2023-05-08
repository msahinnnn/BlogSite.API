FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
#EXPOSE 443

COPY ./BlogSite.Entities/*.csproj ./BlogSite.Entities/
COPY ./BlogSite.DataAccsess/*.csproj ./BlogSite.DataAccsess/
COPY ./BlogSite.Core/*.csproj ./BlogSite.Core/
COPY ./BlogSite.Business/*.csproj ./BlogSite.Business/    
#COPY SharedMessages/*.csproj SharedMessages/
COPY ./BlogSite.API/*.csproj ./BlogSite.API/

COPY . .
RUN dotnet publish ./BlogSite.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","BlogSite.API.dll" ]

#COPY *.sln .
# RUN dotnet restore
# COPY . .
# RUN dotnet publish ./BlogSite.API/*.csproj -o /publish/

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build /publish .
# #ENV ASPNETCORE_URLS="http://*:5174"
# ENTRYPOINT [ "dotnet","BlogSite.API.dll" ]


















