FROM mcr.microsoft.com/dotnet/core/sdk:3.1
EXPOSE 5000
EXPOSE 22
WORKDIR /app
COPY . .
RUN apt-get update && \
    apt-get install unzip openssh-server curl -y && \
    mkdir ~/.ssh/
ENTRYPOINT  /bin/echo $SSH_PUBLIC_KEY > ~/.ssh/authorized_keys && \
    service ssh start && \
    dotnet watch run --no-launch-profile