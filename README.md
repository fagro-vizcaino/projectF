# Project-F

Blazor WebUI CI:<br>
[![Build Status](https://dev.azure.com/fagrovizcaino/Project-F/_apis/build/status/fagro-vizcaino.projectF-WebUi?branchName=master)](https://dev.azure.com/fagrovizcaino/Project-F/_build/latest?definitionId=5&branchName=master)

Web Api CI: <br>
[![Build Status](https://dev.azure.com/fagrovizcaino/Project-F/_apis/build/status/fagro-vizcaino.projectF-WebUi?branchName=master)](https://dev.azure.com/fagrovizcaino/Project-F/_build/latest?definitionId=5&branchName=master)

### you can use Docker for SQL server for development purpose:<br>

`docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourFavoritePassword" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql microsoft/mssql-server-linux:latest`

### In your connection string:<br>

`"Server=localhost,1433;Initial Catalog=alphaDb;Integrated Security=False;User Id=sa;Password=YourFavoritePassword;"`
