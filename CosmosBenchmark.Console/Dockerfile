FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish --configuration Release --output out --runtime ubuntu.18.04-x64 --self-contained

FROM microsoft/dotnet:2.2-runtime
WORKDIR /app
COPY --from=build /app/out .

ENV COSMOS_URI='https://[account].documents.azure.com:443/'
ENV COSMOS_KEY='[key]'