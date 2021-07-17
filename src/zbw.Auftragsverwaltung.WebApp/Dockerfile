#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/zbw.Auftragsverwaltung.WebApp/zbw.Auftragsverwaltung.WebApp.csproj", "src/zbw.Auftragsverwaltung.WebApp/"]
RUN dotnet restore "src/zbw.Auftragsverwaltung.WebApp/zbw.Auftragsverwaltung.WebApp.csproj"
COPY . .
WORKDIR "/src/src/zbw.Auftragsverwaltung.WebApp"
RUN dotnet build "zbw.Auftragsverwaltung.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zbw.Auftragsverwaltung.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zbw.Auftragsverwaltung.WebApp.dll"]


FROM node:10.15-alpine AS client
EXPOSE 4200 49153
USER node
RUN mkdir /home/node/.npm-global
ENV PATH=/home/node/.npm-global/bin:$PATH
ENV NPM_CONFIG_PREFIX=/home/node/.npm-global
RUN npm install -g @angular/cli@8.1.0
WORKDIR /app
CMD ["ng", "serve", "--port", "4200", "--host", "0.0.0.0", "--disable-host-check", "--poll", "2000"]