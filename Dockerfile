FROM microsoft/dotnet:2.2-sdk
WORKDIR /src
COPY . .
RUN dotnet build SkoleTrackerApi.csproj -c Release -o /app

RUN dotnet publish SkoleTrackerApi.csproj -c Release -o /app
WORKDIR /app
ENTRYPOINT ["dotnet", "SkoleTrackerApi.dll"]