#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY ["src/zbw.Auftragsverwaltung.BlazorApp/zbw.Auftragsverwaltung.BlazorApp.csproj", "src/zbw.Auftragsverwaltung.BlazorApp/"]
COPY ["src/zbw.Auftragsverwaltung.Domain/zbw.Auftragsverwaltung.Domain.csproj", "src/zbw.Auftragsverwaltung.Domain/"]
COPY ["src/zbw.Auftragsverwaltung.Client/zbw.Auftragsverwaltung.Client.csproj", "src/zbw.Auftragsverwaltung.Client/"]
COPY ["src/Lib/zbw.Auftragsverwaltung.Lib.HttpClient/zbw.Auftragsverwaltung.Lib.HttpClient.csproj", "src/Lib/zbw.Auftragsverwaltung.Lib.HttpClient/"]
COPY ["src/Lib/zbw.Auftragsverwaltung.Lib.ErrorHandling/zbw.Auftragsverwaltung.Lib.ErrorHandling.csproj", "src/Lib/zbw.Auftragsverwaltung.Lib.ErrorHandling/"]

RUN dotnet restore "src/zbw.Auftragsverwaltung.BlazorApp/zbw.Auftragsverwaltung.BlazorApp.csproj"
COPY . .
WORKDIR "/src/src/zbw.Auftragsverwaltung.BlazorApp"
RUN dotnet build "zbw.Auftragsverwaltung.BlazorApp.csproj" -c Release -o /build

FROM nginx:1.21
WORKDIR /usr/share/nginx/html
COPY --from=publish /build/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf