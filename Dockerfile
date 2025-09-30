# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["MoviesAPI.sln", "."]
COPY ["MoviesAPI/Movies.API.csproj", "MoviesAPI/"]
COPY ["Movies.Application/Movies.Application.csproj", "Movies.Application/"]
COPY ["Movies.Contracts/Movies.Contracts.csproj", "Movies.Contracts/"]

# Restore dependencies
RUN dotnet restore "MoviesAPI.sln"

# Copy all source code
COPY . .

WORKDIR "/src/MoviesAPI"
RUN dotnet publish "Movies.API.csproj" -c Release -o /app/publish --no-restore

# Stage 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Add image version
LABEL version="1.0.0"

# Set ASP.NET Core URL
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Movies.API.dll"]
