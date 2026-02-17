
🚀 GameReviewHub

A web application for browsing games and creating user-generated reviews, built to demonstrate ASP.NET Core MVC fundamentals and clean architecture principles.


📋 Table of Contents

1. About the Project

2. Technologies Used

3. Prerequisites

4. Getting Started

5. Project Structure

6. Features

7. Usage

8. Database Setup

9. Configuration

10. Contact


1. 📖 About the Project

GameReviewHub is a web application developed as part of the ASP.NET Fundamentals course. It allows users to browse games and after authentication, to create, edit, and delete reviews.

The project demonstrates core ASP.NET concepts such as MVC architecture, Entity Framework Core, ASP.NET Identity, dependency injection, validation, and clean separation of concerns. It is designed as a structured and extendable foundation for further development in the ASP.NET Advanced module.


2. 🛠️ Technologies Used
Technology	Version	Purpose
ASP.NET Core MVC	8.0	Web framework
Entity Framework Core	8.0	ORM / Database access
SQL Server (LocalDB)	-	Database
ASP.NET Core Identity	8.0	Authentication & Authorization
Bootstrap	5.x	Responsive UI styling
Razor Views	-	Server-side HTML rendering


3. ✅ Prerequisites

Make sure you have the following installed:

.NET SDK 8.0+

Visual Studio 2022 (recommended)

SQL Server LocalDB (installed with Visual Studio)

Git


4. 🚀 Getting Started

Clone the repository
git clone https://github.com/KamenBodzhakov/GameReviewHub.git
cd GameReviewHub

Restore dependencies
dotnet restore

Apply database migrations
dotnet ef database update

Run the application
dotnet run


The application will start on a local development URL (e.g., https://localhost:xxxx).


5. 📁 Project Structure

GameReviewHub/
│
├── GameReviewHub.Web/          # Presentation Layer (MVC)
│   ├── Controllers/            # MVC Controllers
│   ├── Views/                  # Razor Views
│   ├── wwwroot/                # Static files (CSS, JS, images)
│   ├── ViewModels/             # ViewModels for UI separation
│   └── Program.cs              # Application entry point
│
├── GameReviewHub.Services/     # Business Logic Layer
│   ├── Core/                   # Service implementations
│   └── Interfaces/             # Service contracts
│
├── GameReviewHub.Data/         # Data Access Layer
│   ├── Models/                 # Entity models
│   ├── Configuration/          # Entity configurations
│   ├── Migrations/             # EF Core migrations
│   └── GameReviewHubDbContext.cs
│
└── GameReviewHub.sln           # Solution file



6. ✨ Features

Authentication & Authorization

User registration and login (ASP.NET Identity)

Only authenticated users can create, edit, and delete reviews

Users can modify only their own reviews

Game & Review Management

Browse seeded games

View reviews for individual games

Create reviews (authenticated users only)

Edit and delete own reviews

View all reviews across all games


✅ Validation

Server-side validation using Data Annotations

Client-side validation with unobtrusive validation scripts


🏗️ Architecture Highlights

Clean layered structure (Web / Services / Data)

Thin controllers with business logic handled in services

Entity Framework Core (Code-First approach)

Separation between Entities and ViewModels

Fully asynchronous operations for database access


7. 💻 Usage

Launch the application.

Browse available games from the Games page.

Click View Reviews to see reviews for a specific game.

Register and log in to:

Add a new review

Edit your own reviews

Delete your own reviews

Use All Reviews to browse reviews across all games.

Unauthenticated users can browse games and reviews but cannot create, edit, or delete reviews.


8. 🗄️ Database Setup

The project uses Entity Framework Core (Code-First approach).

The default connection string in appsettings.json uses SQL Server LocalDB:

"ConnectionStrings": {
  "GameReviewHubContextConnection": "Server=(localdb)\\mssqllocaldb;Database=GameReviewHub;Trusted_Connection=True;MultipleActiveResultSets=true"
}


To create the database:

dotnet ef database update


The application seeds initial game data. Reviews are created through the application by registered users.


9. ⚙️ Configuration

Important settings in appsettings.json:

ConnectionStrings – Database configuration

Logging – Log level configuration

⚠️ No sensitive data (passwords or API keys) are stored in the repository.


10. 📬 Contact

Kamen Bodzhakov
GitHub: https://github.com/KamenBodzhakov

Project Link:
https://github.com/KamenBodzhakov/GameReviewHub
