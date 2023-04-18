# ASP.NET Core API (Renting cars)

This is a sample ASP.NET Core API that demonstrates how to build a simple RESTful API using ASP.NET Core and C#.

This API was heavily based on [CodeMaze](https://code-maze.com/net-core-series/) and [Asp .Net Web Api Documentation](https://learn.microsoft.com/en-us/aspnet/web-api/).

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) 6 or later
- [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
- [Docker (Optional)](https://www.docker.com/) 

## Running the API (Visual Studio)

1. Clone this repository to your local machine.
2. Open the solution file `Asp_net_web_api_template.sln` in Visual Studio.
3. Build the solution to restore NuGet packages and build the project.
4. Press `F5` or run the `Asp_net_web_api_template` project to start the API.
5. Open a web browser or API client tool (e.g., [Postman](https://www.postman.com/downloads/)) and navigate to `https://localhost:7239/swagger/index.html` to access the API endpoints.

### Running a MySQL Database Server

1. There's a file named `docker-compose.yml` in the root directory of your project describing the services that will run.
2. Run the following command in the terminal to start the MySQL server:

    ```
    docker-compose up -d
    ```

3. You should now have a MySQL server running on port 1433. You can connect to it using a universal database client such as [DBeaver Community](https://dbeaver.io/download/).

### Running the Migrations

1. If running, stop you API service in Visual Studio.
2. Run the following command in the `Package Manager Console` to create the migrations:

    ```
    update-database
    ```
3. Run your API again.

## Running the API (Docker)

1. Clone this repository to your local machine.
2. Run the following command in the terminal to start the MySQL server:

    ```
    docker-compose up -d
    ```

## API Endpoints

The following endpoint documentations are available in this API:

### Swager (OpenApi)

`https://localhost:7239/swagger/index.html` ou `https://localhost:5001/swagger/index.html`

### Postman Collection

To simplify testing the API, we have included a Postman collection in the root folder of the project. Follow these steps to import the collection into Postman:

1. Open Postman and click on the "Import" button in the top-left corner of the window.
2. In the Import dialog, click on the "Upload Files" tab.
3. Click on the "Select Files" button and navigate to the root folder of the project.
4. Select the file named `RentCarsRequests.postman_collection.json` and click "Open".
5. Click on the "Import" button to import the collection into Postman.

You should now see a new collection named "RentCarsRequests" in your Postman workspace. This collection includes pre-defined requests for each API endpoint, making it easy to test the API without manually constructing HTTP requests.

Note: If you make changes to the API (e.g., adding or modifying endpoints), you may need to update the Postman collection to reflect these changes. To do this, simply re-export the collection from Postman and replace the existing `RentCarsRequests.postman_collection.json` file in the root folder of the project.


## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue if you find a bug or have a feature request.
