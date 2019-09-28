FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM LocalRegistry:5000/ryandb-image

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PeopleFinder/PeopleFinder.csproj", "PeopleFinder/"]
RUN dotnet restore "PeopleFinder/PeopleFinder.csproj"
COPY . .
WORKDIR "/src/PeopleFinder"
RUN dotnet build "PeopleFinder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PeopleFinder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeopleFinder.dll"]