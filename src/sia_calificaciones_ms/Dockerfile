FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
ENV ASPNETCORE_URLS http://*:5059
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

EXPOSE 27017
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY ["sia_calificaciones_ms.csproj", "./"]
RUN dotnet restore "./sia_calificaciones_ms.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "sia_calificaciones_ms.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "sia_calificaciones_ms.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sia_calificaciones_ms.dll"]