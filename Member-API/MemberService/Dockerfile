FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8080

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Member-API/MemberService/MemberService.csproj Member-API/MemberService/
RUN dotnet restore Member-API/MemberService/MemberService.csproj
COPY . .
WORKDIR /src/Member-API/MemberService
RUN dotnet build MemberService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MemberService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MemberService.dll"]
