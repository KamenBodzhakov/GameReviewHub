🚀 GameReviewHub

GameReviewHub is a web application developed as part of the ASP.NET Fundamentals course.
It allows users to browse games, create and manage reviews, and interact with other users through comments and voting.



📖 Overview

The project demonstrates core ASP.NET concepts including:

MVC architecture
Entity Framework Core (Code-First)
ASP.NET Core Identity (authentication & roles)
Dependency Injection
Validation & error handling
Clean layered architecture

The application is designed to be structured, testable, and extendable, serving as a foundation for further development.



  ✨ Key Features

🔐 Authentication & Authorization
User registration and login (ASP.NET Identity)
Role-based access:
User
Administrator
Users can manage only their own reviews
Roles and demo users are automatically seeded

🎮 Games
Browse a list of pre-seeded games
Search games by title
Filter games by genre
View detailed game information

📝 Reviews
Create, edit, and delete reviews (authenticated users)
View reviews per game or globally
Rating system (1–10)

👍 Interaction System
Upvote reviews (1 vote per user per review)
View total vote count

💬 Comments
Add comments under reviews
View discussions in real time
Display author and creation date

🛠️ Admin Panel
Separate Admin Area
Restricted to Administrator role
Full game management:
Create
Edit
Delete
Multi-genre assignment
Data validation

⚡ Dynamic UI (AJAX)
Live search (debounced)
Dynamic pagination (no reload)
Genre filtering
LocalStorage persistence (filters saved on refresh)

🔐 Security & Validation
Anti-forgery (CSRF protection)
Server-side & client-side validation
XSS-safe rendering (Razor encoding)
Ownership checks for edits/deletes
Custom error pages (400 / 404 / 500)

🛠️ Technologies Used
Technology	Version	Purpose
ASP.NET Core MVC	8.0	Web framework
Entity Framework Core	8.0	ORM / Data access
SQL Server (LocalDB)	—	Database
ASP.NET Identity	8.0	Authentication & Roles
Bootstrap	5.x	UI styling
Razor Views	—	Server-side rendering

🧪 Testing
Framework: NUnit
Database: EF Core In-Memory
Focus: Service layer
Covered:
Success scenarios
Edge cases
Validation logic
Mapping logic

Services tested:

GameService
ReviewService
ReviewVoteService
ReviewCommentService



  🚀 Getting Started
  
1. Clone repository
git clone https://github.com/KamenBodzhakov/GameReviewHub.git
cd GameReviewHub

2. Restore dependencies
dotnet restore

3. Apply migrations
dotnet ef database update

4. Run the app
dotnet run



  🔐 Demo Accounts (Seeded)

👑 Administrator
Email: newadmin@gmail.com
Password: Admin123!

👤 Users
user1@gamereviewhub.com / User123!
user2@gamereviewhub.com / User234!
user3@gamereviewhub.com / User345!

👉 These accounts are automatically created on startup.



  🗄️ Database & Seeding

The project uses Code-First EF Core.

Automatically seeded:
Roles (User / Administrator)
Users (admin + 3 demo users)
Games (15 entries)
Reviews
Comments
Votes
Genres

👉 No manual setup required - the app is ready after migration.



  ⚙️ Configuration

Located in appsettings.json:

ConnectionStrings
Logging

Example:

"ConnectionStrings": {
  "DevConnection": "Server=localhost\\SQLEXPRESS;Database=GameReviewHub;Trusted_Connection=True;"
}



  📁 Project Structure
GameReviewHub
│
├── Web (MVC Layer)
├── Services (Business Logic)
├── Data (Entities & DbContext)
├── Tests (Unit Tests)
Highlights:
Clean separation of concerns
Thin controllers
Service-based logic
ViewModels for UI
Admin area isolation



  💻 Usage
Browse games
Search/filter games
Open a game → view reviews
Register/login to:
Create reviews
Edit/delete own reviews
Comment
Upvote



   📬 Contact

Kamen Bodzhakov
GitHub: https://github.com/KamenBodzhakov

Project:
https://github.com/KamenBodzhakov/GameReviewHub