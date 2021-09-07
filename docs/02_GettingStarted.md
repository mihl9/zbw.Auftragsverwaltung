# Getting Started

## Prerequisites

Make sure you have installed all of the following prerequisites on your development machine:

- Microsoft SQL Server 2019
- .Net Core 3.1

The points above aren't needed if you are running the [Project on Docker](#docker-installation)

- .Net IDE (Rider / Visual Studio, etc...)

For the Documentation you need the following tools

- Phyton 3.9
- PIP Installer

Run the following commands to install the plugins for MkDoc

``` bash linenums="1"
$ pip install mkdocs-material
$ pip install mkdocs-drawio-exporter
$ pip install mkdocs-git-revision-date-plugin
```
## Installation

Clone the Github repository
```
$ git clone git@github.com:elbeachboy/zbw.Auftragsverwaltung.git
```
This will clone the latest version of the Repository

To install the dependencies use
```
$ dotnet restore zbw.Auftragsverwaltung.sln
```

To Test the Projects use
```
$ dotnet test zbw.Auftragsverwaltung.sln --no-restore
```

To build the executables use
```
$ dotnet build zbw.Auftragsverwaltung.sln --no-restore
```
??? warning "Only relevant if local installation of Microsoft SQL Server is used"
    If a local installation of Microsoft SQL Server is used you need to create the user for the database Access
    
        - username: sa
        - password: Your_password123

    or you can use the following docker command
    ```
    $ docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 -d mcr.microsoft.com/mssql/server
    ```

to start the Project without any configuration you can use the [Docker](#docker-installation) approach.

## Docker installation
Requires:

- Docker
- Docker Compose

The Docker Compose file contains the definitions how to build and execute the services and it setups the MSSQL Server
To run the Project as is and for testing execute the following command in the Project root folder

```
$ docker-compose up
```

You can also select the Docker-Compose file in the IDE and start it in Debug Mode.
![Docker-Compose](02_GettingStarted/Docker_Compose_VS2019.png)

