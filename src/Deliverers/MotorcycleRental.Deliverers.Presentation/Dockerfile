FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source
COPY . .

RUN dotnet restore
RUN dotnet build -c Release
RUN dotnet publish ./src/Deliverers/MotorcycleRental.Deliverers.Presentation/MotorcycleRental.Deliverers.Presentation.csproj -c Release -o /App

ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /App
COPY --from=build /App .

ENTRYPOINT ["dotnet", "MotorcycleRental.Deliverers.Presentation.dll", "--urls=http://+:5001"]