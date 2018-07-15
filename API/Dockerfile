FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app
EXPOSE 80/tcp



# Copy csproj and restore as distinct layers
COPY API.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o output


# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/output ./
#Defaults Routes DoNet Core URLs to be through to port 80
ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "Ford.Tracker.Api.dll", "--server.urls", "http://*:8080"]