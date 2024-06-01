# Then we create the final image using instructions adapted from https://www.wintellect.com/devops-sql-server-dacpac-docker/
FROM mcr.microsoft.com/mssql/server:2022-latest AS final

USER root

### Install Unzip
RUN apt-get update \
    && apt-get install unzip -y


# Install SQLPackage for Linux and make it executable
RUN wget -progress=bar:force -q -O sqlpackage.zip https://aka.ms/sqlpackage-linux \
    && unzip -qq sqlpackage.zip -d /opt/sqlpackage \
    && chmod +x /opt/sqlpackage/sqlpackage \
    && chown -R mssql /opt/sqlpackage \
    && mkdir /tmp/db \
    && chown -R mssql /tmp/db


# Add the DACPAC to the image
COPY ./source/infrastructure/Badop.Infrastructure.Database/bin/Release/Badop.Infrastructure.Database.dacpac /tmp/db.dacpac

# Configure external build arguments to allow configurability.
ARG DBNAME="Badop"
ARG SAPASSWORD="password"

# Configure the required environmental variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=$SAPASSWORD

# Launch SQL Server, confirm startup is complete, deploy the DACPAC, then delete the DACPAC and terminate SQL Server.
# See https://stackoverflow.com/a/51589787/488695
RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/sqlpackage/sqlpackage /a:Publish /tsn:localhost /tdn:${DBNAME} /tu:sa /tp:$SA_PASSWORD /sf:/tmp/db/db.dacpac \
    && rm -r /tmp/db \
    && pkill sqlservr \
    && rm -r /opt/sqlpackage