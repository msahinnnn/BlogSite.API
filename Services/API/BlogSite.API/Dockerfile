FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
#EXPOSE 443

COPY Shared/Messages/BlogSite.Messages/*.csproj Shared/Messages/BlogSite.Messages/
COPY Services/API/BlogSite.Business/*.csproj Services/API/BlogSite.Business/
COPY Services/API/BlogSite.Core/*.csproj Services/API/BlogSite.Core/
COPY Services/API/BlogSite.DataAccsess/*.csproj Services/API/BlogSite.DataAccsess/
COPY Services/API/BlogSite.Entities/*.csproj Services/API/BlogSite.Entities/
COPY Services/API/BlogSite.API/*.csproj Services/API/BlogSite.API/

RUN dotnet restore Services/API/BlogSite.API/*.csproj
COPY . .

RUN dotnet publish Services/API/BlogSite.API/*.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BlogSite.API.dll"]









# COPY /app/BlogSite.Entities/*.csproj /app/BlogSite.Entities/
# COPY BlogSite.DataAccsess/*.csproj BlogSite.DataAccsess/
# COPY BlogSite.Core/*.csproj BlogSite.Core/
# COPY BlogSite.Business/*.csproj BlogSite.Business/    
# #COPY SharedMessages/*.csproj SharedMessages/
# COPY BlogSite.API/*.csproj BlogSite.API/

# COPY . .
# RUN dotnet publish ./BlogSite.API/*.csproj -c Release -o out
# FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
# WORKDIR /app
# COPY --from=build /app/out .
# ENTRYPOINT [ "dotnet","BlogSite.API.dll" ]

#COPY *.sln .
# RUN dotnet restore
# COPY . .
# RUN dotnet publish ./BlogSite.API/*.csproj -o /publish/

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# COPY --from=build /publish .
# #ENV ASPNETCORE_URLS="http://*:5174"
# ENTRYPOINT [ "dotnet","BlogSite.API.dll" ]



