FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

COPY *.sln .
COPY OrderService.Api/*.csproj ./OrderService.Api/
COPY OrderService.Application/*.csproj ./OrderService.Application/
COPY OrderService.Contract/*.csproj ./OrderService.Contract/

RUN dotnet restore "OrderService.Api/OrderService.Api.csproj"
RUN dotnet restore "OrderService.Application/OrderService.Application.csproj"
RUN dotnet restore "OrderService.Contract/OrderService.Contract.csproj"

COPY . .

WORKDIR /source/OrderService.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=http://+:5018
EXPOSE 5018
ENTRYPOINT ["dotnet", "OrderService.Api.dll"]