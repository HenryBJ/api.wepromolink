FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["/WePromoLink.Controller/WePromoLink.Controller.csproj","./WePromoLink.Controller/"]
COPY ["/WePromoLink.Shared/WePromoLink.Shared.csproj", "WePromoLink.Shared/"]
RUN dotnet restore "./WePromoLink.Controller/WePromoLink.Controller.csproj"  

# Copy everything else and build
COPY . ./
RUN dotnet publish "./WePromoLink.Controller/WePromoLink.Controller.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env "/app/out" .

# Expose ports
EXPOSE 443/tcp


# Start
ENTRYPOINT ["dotnet", "WePromoLink.Controller.dll"]