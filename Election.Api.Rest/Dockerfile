FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Election.Api.Rest/Election.Api.Rest.csproj Election.Api.Rest/
COPY Vote.Core/Vote.Core.csproj Vote.Core/
RUN dotnet restore Election.Api.Rest/Election.Api.Rest.csproj
COPY . .
WORKDIR /src/Election.Api.Rest
RUN dotnet build Election.Api.Rest.csproj -c Debug -o /app

FROM build AS publish
RUN dotnet publish Election.Api.Rest.csproj -c Debug -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Election.Api.Rest.dll"]
