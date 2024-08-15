# BookStore Api (.NET 8 / Sqlite / Minimal Api / Swagger)

This repository provides an example implementation of Microsoft's Minimal API for a simple BookStore application. The application demonstrates CRUD (Create, Read, Update, Delete) operations using SQLite as the database.

## Overview

Minimal APIs in .NET 8 provide a streamlined way to create HTTP APIs with minimal configuration and setup. They are perfect for small microservices, quick prototypes, or low-resource applications.

This project follows the principles and guidelines outlined in the [Microsoft Learn module for building web APIs with Minimal API](https://learn.microsoft.com/tr-tr/training/modules/build-web-api-minimal-api/2-what-is-minimal-api).

## Features

- **Swagger Integration**: API documentation and testing with Swagger.
- **SQLite Integration**: Lightweight database integration using SQLite.
- **CRUD Operations**: Perform create, read, update, and delete operations on book records.
- **Minimal API**: Simple and concise API setup with minimal configuration.


## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/samedbilgili/MinimalApi-Sqlite-BookStore.git
    cd MinimalApi-Sqlite-BookStore
    ```

2. Restore the dependencies:
    ```bash
    dotnet restore
    ```

3. Build the project:
    ```bash
    dotnet build
    ```

4. Run the application:
    ```bash
    dotnet run
    ```


## API Endpoints
Examine **/swagger** for details.
- **GET /books**: Retrieve all books.
- **GET /books/{id}**: Retrieve a book by ID.
- **GET /booksGetByTitle/{string}**: Search books by name.
- **POST /books**: Create a new book.
- **PUT /books/{id}**: Update an existing book.
- **DELETE /books/{id}**: Delete a book by ID.
- **POST /bookimage**: Save book cover image a book by ID.
  

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
