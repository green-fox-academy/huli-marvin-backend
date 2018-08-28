FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY MemberService/MemberService.csproj MemberService/
RUN dotnet restore MemberService/MemberService.csproj
COPY . .
WORKDIR /src/MemberService
RUN dotnet build MemberService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MemberService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MemberService.dll"]
