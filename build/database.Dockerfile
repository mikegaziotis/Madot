# STEP I: BUILD SQLPROJ
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG SQLPROJ_NAME
WORKDIR /app
RUN echo pwd
COPY ./database/$SQLPROJ_NAME/ .
RUN sed -i '/<Project /a <Sdk Name="Microsoft.Build.Sql" Version="0.1.14-preview" />' $SQLPROJ_NAME.sqlproj
RUN sed -i -e '/Import.*Microsoft\.Data\.Tools\.Schema\.SqlTasks\.targets/d' \
           -e '/Import.*Microsoft\.Common\.props/d' $SQLPROJ_NAME.sqlproj
RUN dotnet build $SQLPROJ_NAME.sqlproj /p:NetCoreBuild=true /p:OutputPath=/app/dacpac

# STEP II: DEPLOY DATABASE
FROM mcr.microsoft.com/mssql/server:2022-latest
ARG SQLPROJ_NAME
ARG DATABASE_NAME
ARG DATABASE_SA_PASSWORD
ENV SA_PASSWORD=$DATABASE_SA_PASSWORD
ENV ACCEPT_EULA=Y
USER root

RUN apt-get update \
&& apt-get install unzip libunwind8 libssl-dev -y \
&& apt-get clean

RUN wget -progress=bar:force -q -O sqlpackage.zip https://aka.ms/sqlpackage-linux \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod +x /opt/sqlpackage/sqlpackage \
    && chown -R mssql /opt/sqlpackage \
    && mkdir /tmp/db \
    && chown -R mssql /tmp/db

WORKDIR /

COPY --from=build /app/dacpac/$SQLPROJ_NAME.dacpac /tmp/db/db.dacpac

RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" && ( echo "SQLServer started" && sleep 10s ) || ( echo "SQLSERVER failed to start" && exit ) && \
    /opt/sqlpackage/sqlpackage \
    /Action:Publish \
    /TargetServerName:localhost \
    /TargetDatabaseName:${DATABASE_NAME} \
    /TargetUser:sa \
    /TargetPassword:$SA_PASSWORD \
    /TargetTrustServerCertificate:True \
    /SourceFile:/tmp/db/db.dacpac \
    && rm -r /tmp/db \
    && pkill sqlservr \
    && rm -r /opt/sqlpackage
    
EXPOSE 1433
