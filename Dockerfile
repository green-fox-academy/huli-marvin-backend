FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
RUN git clone https://github.com/green-fox-academy/huli-marvin-backend.git
RUN cd /src/huli-marvin-backend && git pull && git checkout sprint06
WORKDIR /src/huli-marvin-backend/Member-API/MemberService
RUN dotnet restore MemberService.csproj
RUN dotnet build MemberService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MemberService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MemberService.dll"]
