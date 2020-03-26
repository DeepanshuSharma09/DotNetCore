FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /CreditCardApi

COPY *.csproj ./
RUN dotnet restore

COPY . ./
# This will build the app and generate artifacts
RUN dotnet publish -c Release -o artifacts

WORKDIR /CreditCardApi/artifacts
EXPOSE 7000
ENTRYPOINT ["dotnet", "CreditCardApi.dll"]