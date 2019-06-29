FROM microsoft/dotnet:2.2-sdk AS installer-env

COPY . /src/dotnet-function-app
RUN cd /src/dotnet-function-app && \
    mkdir -p /home/site/wwwroot && \
    dotnet publish *.csproj --configuration Debug --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:2.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

RUN apt-get update
RUN apt-get install -y --no-install-recommends gnupg zip unzip curl procps -y
RUN rm -rf /var/lib/apt/lists/*
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
COPY --from=installer-env ["/src/dotnet-function-app", "/src/dotnet-function-app"]

WORKDIR /src/dotnet-function-app