# ChatRoom API

This project is a web API built with ASP.NET Core that allows users to register, join chat rooms, starting private chat, and engage in real-time conversations with support agents. The application utilizes the SignalR library to enable real-time communication.

## Features

- User Registration: Users can create an account to access the chat functionality.
- Join Chat Room: Users can join a specific chat room to engage in group conversations.
- Private Messaging: Users can initiate private conversations with specific support agents or other users.
- Real-Time Updates: The application leverages SignalR to provide real-time updates and instant message delivery.
- Security: The API implements secure authentication and authorization mechanisms to protect user data and ensure a safe chat environment.


## Technologies Used

- ASP.NET Core: A cross-platform framework for building web APIs and applications.
- SignalR: A library that provides real-time web functionality, enabling bi-directional communication between the server and clients.
- Entity Framework Core: An object-relational mapper (ORM) that simplifies database operations.
- SQL Server: A relational database management system used for storing user data and chat history.
- Authentication and Authorization: The API uses JWT (JSON Web Tokens) for authentication and role-based authorization.

## Prerequisites

- .NET Core SDK [link to download](https://dotnet.microsoft.com/download)
- SQL Server [link to download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

1. Clone the repository:

```
git clone https://github.com/sulaimanmugahed/ChatRoom.git
```

2. Navigate to the project directory:

```
cd ChatRoom/ChatRoom.Api
```

3. Install the required dependencies:

```
dotnet restore
```

4. Change the database connection in the `appsettings.json` file:

```json
{
  "ConnectionStrings": {
    "SqlServerConnection": "Server=YourServer;Database=YourDatabase;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

5. Apply the database migrations to create the required tables:

```
dotnet ef database update
```

6. Build and run the API:

```
dotnet run
```

## API Endpoints

The following API endpoints are available:

- **POST /api/Account/Register**: Register a new user.
- **POST /api/Account/Authenticate**: Authenticate a user and obtain an access token.
- **POST /api/Rooms/Create**: Create a new chat room.

## Authentication

To access protected API endpoints, include the access token in the `Authorization` header of your requests:

```
Authorization: Bearer <access_token>
```

The access token can be obtained by making a successful login request to the `/api/Account/Authenticate` endpoint.

## Signalr
 
To use SignalR in your client application, you'll need to follow these steps: 

1. Install the SignalR library for your client platform or programming language. SignalR is available for various platforms, including JavaScript, .NET, Java, and more. You can find the appropriate library and installation instructions in the documentation specific to your platform. 

2. Connect to the SignalR hub from your client application. You'll need to provide the URL of the SignalR hub, which typically follows the format `http(s)://<hostname>/chatHub`. Replace `<hostname>` with the appropriate server address and port.


Feel free to contribute to this project by submitting bug reports, feature requests, or pull requests. Happy coding!
