FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /CreditCardApi

COPY *.csproj ./
RUN dotnet restore

COPY . ./
# This will build the app and generate artifacts
RUN dotnet publish -c Release -o artifacts

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /CreditCardApi/artifacts
EXPOSE 7000
ENTRYPOINT ["dotnet", "CreditCardApi.dll"]