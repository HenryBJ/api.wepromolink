FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["/WePromoLink.StatsWorker/WePromoLink.StatsWorker.csproj","./WePromoLink.StatsWorker/"]
COPY ["/WePromoLink.Shared/WePromoLink.Shared.csproj", "WePromoLink.Shared/"]
RUN dotnet restore "./WePromoLink.StatsWorker/WePromoLink.StatsWorker.csproj"  

# Copy everything else and build
COPY . ./
RUN dotnet publish "./WePromoLink.StatsWorker/WePromoLink.StatsWorker.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env "/app/out" .

# Expose ports
EXPOSE 443/tcp


# Start
ENTRYPOINT ["dotnet", "WePromoLink.StatsWorker.dll"]