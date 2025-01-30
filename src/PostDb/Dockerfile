# Choose ubuntu version
FROM mcr.microsoft.com/mssql/server:2019-latest

USER root
# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY ./PostDb /usr/src/app
             
RUN apt-get update && apt-get install -y curl gnupg2 \
    && curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev \
    && echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
			 
			 
USER mssql
# Set environment variables, not have to write them with the docker run command
# Note: make sure that your password matches what is in the run-initialization script 
ENV SA_PASSWORD password123!
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

# Expose port 1433 in case accessing from other container
# Expose port externally from docker-compose.yml
EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time)
CMD /bin/bash ./entrypoint.sh