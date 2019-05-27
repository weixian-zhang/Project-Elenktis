FROM microsoft/dotnet:2.2-sdk AS installer-env

WORKDIR /src

COPY CommonLib /src/CommonLib/

WORKDIR /src/CommonLib/Elenktis.Common.AzResourceManager
RUN dotnet restore Elenktis.Common.AzResourceManager.csproj

WORKDIR /src/CommonLib/Elenktis.Common.Configuration
RUN dotnet restore Elenktis.Common.Configuration.csproj

WORKDIR /src/CommonLib/Elenktis.Common.Messaging
RUN dotnet restore Elenktis.Common.Messaging.csproj

WORKDIR /src/CommonLib/Elenktis.Common.Command
RUN dotnet restore Elenktis.Common.Command.csproj

COPY Informer/MandateServiceInformer /src/Informer/MandateServiceInformer/
RUN mkdir -p /home/site/wwwroot
WORKDIR /src/Informer/MandateServiceInformer
RUN dotnet publish -c Debug -o /home/site/wwwroot

#RUN mkdir -p /home/site/wwwroot
#WORKDIR /src/app
#COPY /src/app /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:2.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]