FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50916

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Schedule-API/Schedule-API/ScheduleAPI.csproj Schedule-API/Schedule-API/
RUN dotnet restore Schedule-API/Schedule-API/ScheduleAPI.csproj
COPY . .
WORKDIR /src/Schedule-API/Schedule-API
RUN dotnet build ScheduleAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ScheduleAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ScheduleAPI.dll"]
