FROM microsoft/aspnetcore-build:2.0.5-2.1.4 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "MemberServiceAPI.dll"]
