FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster
RUN mkdir -p /usr/greetings
WORKDIR /usr/greetings
COPY out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "GreetingsApp.dll"]


