# SprintHEMone
# RacerBooks_Sprint1_EHM
# SprintOne Online Store

## Overview
SprintOne is an online store developed for *RacerSprints*, an imaginary bookshop, to provide customers with a convenient platform to browse, purchase, and manage their orders. The application is designed to meet specific requirements outlined by the client.

## Key Features
- **User Registration**: Customers can register and create profiles to manage their orders.
- **Product Management**: Users can view items, their details, and search the inventory for specific products.
- **Shopping Cart**: Customers can add multiple items to their cart and perform a checkout.
- **Persistent Cart**: Each user's cart is persisted between devices.
- **Authentication**: Users must be logged in to add items to their cart.
- **Logging**: Unsuccessful login attempts are logged for security purposes.

## System Architecture
The system is built using the following technologies and architecture:

- **Frontend**: HTML, CSS, Bootstrap for styling, and Razor syntax for server-side rendering.
- **Backend**: ASP.NET Core MVC for server-side logic and RESTful API endpoints.
- **Database**: Microsoft SQL Server for data storage.
- **ORM**: Entity Framework Core for database interaction.
- **Dependency Injection**: Utilized to inject dependencies such as database context.
- **Logging**: Logging implemented for monitoring unsuccessful login attempts.
- **Security**: Passwords hashed using SHA256 for security.
- **Deployment**: Deployed using Microsoft Azure or similar cloud platform.

## Entity Relationship Diagram (ERD)
The database schema consists of the following entities:

- **Customer**: Represents registered users with details like email, password hash, full name, and address.
- **Item**: Represents products available for purchase, including details like item name, cost, and description.
- **Cart**: Stores the items added to the shopping cart by customers, associated with the user's email and item details.
- **OrderList**: Tracks the orders placed by customers, including details like order ID, customer email, item ID, and price.

## Setup Instructions
To set up the SprintOne application locally, follow these steps:

1. **Clone Repository**: Clone the GitHub repository containing the project code.
2. **Database Setup**: Ensure you have Microsoft SQL Server installed. Update the connection string in the `appsettings.json` file with your SQL Server details.
3. **Build and Run**: Build and run the application using Visual Studio or the .NET CLI.
4. **Register Users**: Access the application through a web browser, register new users, and explore the available products.
5. **Interact with the Application**: Navigate through the application, add items to the cart, and simulate the checkout process.

## Contributors
- [Hiren Thulasaie](https://github.com/Hirenr12)
- [Ethan Swanepoel](https://github.com/EthanSwanepoel)
- [Mishka Dewlok](https://github.com/MishkaDewlok)

## License
This project is licensed under the [MIT] License
