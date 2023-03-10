FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY ./BlogSite.Entities/*.csproj ./BlogSite.Entities/
COPY ./BlogSite.DataAccsess/*.csproj ./BlogSite.DataAccsess/
COPY ./BlogSite.Core/*.csproj ./BlogSite.Core/
COPY ./BlogSite.Business/*.csproj ./BlogSite.Business/
COPY ./BlogSite.API/*.csproj ./BlogSite.API/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./BlogSite.API/*.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="https://*:5000"
ENTRYPOINT [ "dotnet","BlogSite.API.dll" ]




















