
🚀 GameReviewHub

GameReviewHub is a web application developed as part of the ASP.NET Fundamentals course. It allows users to browse games and after authentication to create, edit, and delete reviews.
Users can also interact with reviews by **upvoting them and adding comments**, enabling discussion around each review.

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

├── GameReviewHub.Web/          # Presentation Layer (MVC)
│   ├── Areas/
│   │   └── Admin/              # Administrator Area
│   │       ├── Controllers/
│   │       │   ├── DashboardController
│   │       │   └── GamesController
│   │       └── Views/
│   │           ├── Dashboard/
│   │           └── Games/
│   │
│   ├── Controllers/
│   │   ├── ReviewsController
│   │   ├── ReviewCommentsController
│   │   └── GamesController
│   │
│   ├── Views/
│   │   ├── Games/
│   │   ├── Reviews/
│   │   ├── ReviewComments/
│   │   └── Shared/
│   │       ├── _GameCardsPartial.cshtml
│   │       └── _GameFormPartial.cshtml
│   │
│   ├── ViewModels/
│   │   ├── Game/
│   │   ├── Game/Admin/
│   │   ├── Review/
│   │   └── ReviewComment/
│   │
│   └── Program.cs
│
├── GameReviewHub.Services/     # Business Logic Layer
│   ├── Core/
│   │   ├── GameService
│   │   ├── ReviewService
│   │   ├── ReviewVoteService
│   │   └── ReviewCommentService
│   │
│   └── Interfaces/
│       ├── IGameService
│       ├── IReviewService
│       ├── IReviewVoteService
│       └── IReviewCommentService
│
├── GameReviewHub.Data/
│   ├── Models/
│   │   ├── Game
│   │   ├── Genre
│   │   ├── GameGenre
│   │   ├── Review
│   │   ├── ReviewVote
│   │   └── ReviewComment
│
├── GameReviewHub.Tests/        # Unit Tests
│   ├── GameServiceTests
│   ├── ReviewServiceTests
│   ├── ReviewVoteServiceTests
│   └── ReviewCommentServiceTests



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

Review Interaction

Users can **upvote reviews** to show appreciation for helpful feedback.

Each review displays the current **vote count**, and users can vote once per review.

Review Comments

Authenticated users can add **comments under reviews**, enabling discussion and feedback between users.
- Creating comments under a review
- Viewing comments directly beneath each review
- Displaying comment author and creation date

Search games by title

Filter games by genre

Game Administration

Administrators can:

 Create new games with:
  - Title, Developer, Description
  - Release date validation
  - Image path (local project assets)
  - Multiple genre selection
 Edit existing games
 Delete games safely (including related data)

Genres are predefined and seeded in the database, ensuring consistency and preventing invalid data entry.

Authentication & Authorization (Extended)

The application uses the built-in ASP.NET Core Identity system for managing users and roles.

👤 User Roles
The system defines two primary roles:
User
Administrator

⚙️ Role Management
Roles are automatically seeded on application startup
Implemented via a custom IdentitySeeder class
Ensures required roles always exist in the database

👑 Administrator Setup
An administrator user is assigned automatically during application startup:

🛠️ Administrator Panel

An Administrator area is implemented using ASP.NET Core Areas.

Features include:
- Access restricted to users with the **Administrator** role
- Manage games (Create, Edit, Delete)
- Reusable UI components using partial views
- Clean separation between public and admin functionality

✅ Validation

Server-side validation using Data Annotations

Client-side validation with unobtrusive validation scripts

🔐 Security & Reliability

Custom error pages for 400 Bad Request, 404 Not Found, and 500 Internal Server Error

CSRF protection using Anti-Forgery tokens on all POST forms

XSS-safe rendering of user-generated content through Razor HTML encoding

Authorization and ownership checks so users can edit and delete only their own reviews

Input validation and defensive handling of invalid IDs and missing resources

⚠️ Error Handling

🏗️ Architecture Highlights

Clean layered structure (Web / Services / Data)

Thin controllers with business logic handled in services

Entity Framework Core (Code-First approach)

Separation between Entities and ViewModels

Fully asynchronous operations for database access

Reusable partial views for shared UI components

Area-based architecture for role-specific functionality (Admin area)

Centralized validation using ValidationConstants

Centralized error messages using ErrorMessages

Consistent validation across ViewModels and services

AJAX-based partial rendering for improved performance and UX

Client-side state persistence using Local Storage

Separation between full views and partial views for dynamic updates

🔍 Dynamic Search & Pagination 
Live search (AJAX-based) for games:
-Results update instantly while typing (no page reload)
-Debounced input for performance optimization

Dynamic pagination (AJAX):
-Navigate between pages without full page reload
-Works together with search and filtering seamlessly

Genre filtering with instant results
-Combine search + genre filters dynamically

State persistence (Local Storage):
-Search term and selected genre are saved in the browser
-Filters are automatically restored on page reload

7. 💻 Usage

Launch the application.

Browse available games from the Games page.

Use the search field to find games by title.

Use the genre filter to narrow the list of displayed games.

Click View Reviews to see reviews for a specific game.

Register and log in to:

Add a new review  
Edit your own reviews  
Delete your own reviews  
Upvote helpful reviews  
Add comments to reviews

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

10. 🧪 Testing

The project includes unit tests covering the business logic layer.

🧪 Testing Approach

- Framework: NUnit
- Mocking: Moq (used where appropriate)
- Database: In-Memory Database (EF Core)

📊 Coverage

- Tests focus on service layer logic:
  - GameService
  - ReviewService
  - ReviewVoteService
  - ReviewCommentService

- Covers:
  - Success scenarios
  - Failure/edge cases
  - Data validation logic
  - Entity-to-ViewModel mapping

The test suite is designed to achieve the required minimum coverage of the business logic.

11. 📬 Contact

Kamen Bodzhakov
GitHub: https://github.com/KamenBodzhakov

Project Link:
https://github.com/KamenBodzhakov/GameReviewHub
