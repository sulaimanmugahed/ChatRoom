# ChatRoom API

Ready to turn your client's online vision into a thriving hub? My .NET 8
Real-Time API is like magic sprinkles for building awesome communities.
 **Let's sprinkle some together! **


## Why ChatRoom?

- Instant Fun: Sign up and jump into exciting chat rooms
- Chat Like a Champ: Connect with friends & support in real-time
- New Friends Forever: Find awesome people and follow them with a tap
- Alawys Connected: Chat instantly - cinversations never stop!


## Technologies Used

- ASP.NET Core: A cross-platform framework for building web APIs and applications.
- SignalR: A library that provides real-time web functionality, enabling bi-directional communication between the server and clients.
- Entity Framework Core: An object-relational mapper (ORM) that simplifies database operations.
- SQL Server: A relational database management system used for storing user data and chat history.
- Authentication and Authorization: The API uses JWT (JSON Web Tokens) for authentication and role-based authorization.

## Prerequisites

- .NET 8 Core SDK [link to download](https://dotnet.microsoft.com/download)
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
