FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Vote.Queue/Vote.Queue.csproj Vote.Queue/
COPY Vote.Core/Vote.Core.csproj Vote.Core/
RUN dotnet restore Vote.Queue/Vote.Queue.csproj
COPY . .
WORKDIR /src/Vote.Queue
RUN dotnet build Vote.Queue.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish Vote.Queue.csproj -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vote.Queue.dll"]
