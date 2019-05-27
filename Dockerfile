FROM microsoft/dotnet:2.2-sdk AS installer-env

WORKDIR /src
COPY . /src

COPY ../../CommonLib/ ./src/CommonLib/

##create publish folder
#RUN mkdir -p /publish
#RUN dotnet publish Elenktis.Informer.MandateServiceInformer.csproj publish -c Release -o /publish
#
#COPY . /publish
#WORKDIR /publish
#
#RUN mkdir -p /home/site/wwwroot 
#COPY /publish /home/site/wwwroot 
#
#
#
## RUN cd /app && \
##     mkdir -p /home/site/wwwroot && \
##     dotnet publish *.csproj --output /home/site/wwwroot
#
#FROM mcr.microsoft.com/azure-functions/dotnet:2.0
#ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    #AzureFunctionsJobHost__Logging__Console__IsEnabled=true
#
#COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]