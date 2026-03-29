# Use the official SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["GymManagement.API/GymManagement.API.csproj", "GymManagement.API/"]
COPY ["GymManagement.Core/GymManagement.Core.csproj", "GymManagement.Core/"]
COPY ["GymManagement.Infrastructure/GymManagement.Infrastructure.csproj", "GymManagement.Infrastructure/"]
RUN dotnet restore "GymManagement.API/GymManagement.API.csproj"

# Copy the rest of the code and build
COPY . .
WORKDIR "/src/GymManagement.API"
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GymManagement.API.dll"]
