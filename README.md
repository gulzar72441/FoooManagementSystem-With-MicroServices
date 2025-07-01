# 🍔 Food Ordering System 🚀

This is a **robust** and **scalable** microservices-based Food Ordering System built with .NET. It's designed to provide a seamless and efficient experience for ordering food online, from browsing restaurants to tracking deliveries. 🍽️🚚

## 🌟 Services Overview

Each service operates independently, allowing for better maintainability and scalability.

-   **ApiGateway** 🚪: The central entry point for all client requests. It intelligently routes incoming requests to the appropriate backend services, acts as a single point of access, and can handle authentication and rate limiting.
-   **Authentication** 🔐: Manages all aspects of user identity. This includes user registration, secure login, password management, and generating/validating authentication tokens.
-   **Notification** 🔔: Handles real-time and scheduled notifications. This service ensures users receive timely updates on their order status (e.g., "Order Placed!", "Food is Ready!", "Out for Delivery!"). It can be extended to support various communication channels.
-   **Order** 📝: The core service for managing the entire order lifecycle. It handles order creation, item selection, pricing, status updates (e.g., pending, accepted, preparing, delivered), and order history.
-   **Payment** 💳: Facilitates secure payment transactions. This service integrates with various payment gateways to process credit card payments, digital wallets, and other payment methods. It handles payment authorization, capture, and refunds.
-   **Restaurant** 🧑‍🍳: Manages all restaurant-related data. This includes restaurant profiles, menus, dish details, pricing, availability, and order acceptance/rejection from the restaurant's side.
-   **Review** ⭐: Enables users to provide feedback on restaurants and food items. This service allows users to post ratings and written reviews, which helps in maintaining quality and improving services.
-   **Rider** 🚴: Manages the delivery process. This service tracks rider availability, assigns orders to riders, manages delivery routes, and provides real-time location updates for ongoing deliveries.

## 💻 Technologies Used (The Secret Sauce! 🤫)

This project leverages a modern .NET ecosystem to deliver a high-performance and reliable system:

-   **Backend Framework**: **.NET 9** 🎯 - The primary framework for building fast, scalable, and secure backend services.
-   **Database Interaction**: **Entity Framework Core** 📊 - A powerful ORM that simplifies data access and management, supporting various databases.
-   **Database**: **SQL Server** / **In-Memory Database** 🗄️ - Configurable for development (in-memory) and production (SQL Server) environments.
-   **API Gateway**: **Ocelot** 🛣️ - A lightweight, fast, and scalable API Gateway, perfect for microservices architectures.
-   **Real-time Communication**: **ASP.NET Core SignalR** ⚡ - Enables real-time, bi-directional communication between server and clients for instant notifications and updates.
-   **API Documentation**: **Swagger/OpenAPI** 📖 - Automatically generates interactive API documentation, making it easy to understand and test endpoints.
-   **Cross-Origin Resource Sharing**: **CORS** 🔗 - Securely configured to allow web applications from different domains to interact with our APIs.
-   **Dependency Injection**: Built-in .NET Core DI for managing service dependencies efficiently.
-   **Logging**: Integrated logging for better error tracking and monitoring.

## 🚀 Setup and Running the Project (Let's Get Cooking! 👨‍💻)

Follow these steps to get the Food Ordering System up and running on your local machine:

1.  **Prerequisites** ✅:\
    -   **.NET SDK** (version 9.0 or higher recommended)
    -   **SQL Server** (optional, for persistent data storage; otherwise, in-memory database will be used for development)
    -   **Visual Studio** or **Visual Studio Code** (highly recommended IDEs for .NET development)
    -   **Git**

2.  **Clone the repository** ⬇️:
    ```bash
    git clone <repository_url> # Replace with your actual repository URL
    cd FoodOrderingSystem
    ```

3.  **Restore NuGet packages** 📦:
    This command will download all necessary dependencies for the projects.
    ```bash
    dotnet restore
    ```

4.  **Build the solution** 🏗️:
    Compile all projects in the solution.
    ```bash
    dotnet build
    ```

5.  **Run the services** ▶️:
    You have a few options to run the services:

    -   **Run a specific API (e.g., Authentication API)**:
        Navigate to the API project directory and run it.
        ```bash
        cd src/Services/Authentication/API
        dotnet run
        ```

    -   **Run the API Gateway**:
        This is typically the first service you'll want to run as it routes to others.
        ```bash
        cd src/ApiGateway
        dotnet run
        ```
    -   **Run all services using your IDE**: If you are using Visual Studio, you can set multiple startup projects to run all APIs simultaneously.

6.  **Access APIs** 🌐:
    -   **API Gateway**: Typically accessible at `http://localhost:5000` (for HTTP) or `http://localhost:5001` (for HTTPS).
    -   **Individual Services**: Check the `launchSettings.json` file located in each API project's `Properties` folder for their specific local host URLs and ports.

## 🔒 CORS Configuration

CORS (Cross-Origin Resource Sharing) is configured to allow requests from `http://localhost:3000` (a common frontend development port) and `http://localhost:5000` (the API Gateway's default HTTP port). If you are running your frontend application or other services on different ports or domains, you **must** update the `Program.cs` files in the respective API projects to include your specific origins. Failing to do so will result in "CORS" errors.

```csharp
// Example in Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:5000") // Add your origins here
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
```

Feel free to contribute, report issues, or suggest improvements! Happy coding! ✨ 