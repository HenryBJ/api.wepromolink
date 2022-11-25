FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["WePromoLink.csproj","."]
RUN dotnet restore "WePromoLink.csproj" -s https://api.nuget.org/v3/index.json 

# Copy everything else and build
COPY . ./
RUN dotnet publish "WePromoLink.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env "/app/out" .

# Expose ports
EXPOSE 80/tcp
EXPOSE 443/tcp


# Start
ENTRYPOINT ["dotnet", "WePromoLink.dll"]