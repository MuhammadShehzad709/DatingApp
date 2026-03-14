# 💘 DatingApp — Full Stack Dating Platform

> A complete dating web application built solo with **Angular** and **ASP.NET Core**, featuring real-time messaging, user profiles, matching system, and secure JWT authentication.

![Angular](https://img.shields.io/badge/Angular-DD0031?style=flat-square&logo=angular&logoColor=white)
![.NET](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT_Auth-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-0078D4?style=flat-square)
![Solo Project](https://img.shields.io/badge/Solo_Project-purple?style=flat-square)

---

## 🚀 Live Demo

🔗 [View Live App](#) &nbsp;|&nbsp; 📂 [Backend Repo](#) &nbsp;|&nbsp; 📂 [Frontend Repo](#)

---

## ✨ Features

### 👤 User Profiles
- Register & login with secure JWT authentication
- Upload and manage profile photos (Cloudinary)
- Edit bio, interests, age, and location
- View other users' detailed profiles

### 💬 Real-Time Chat
- Instant messaging using **SignalR WebSockets**
- Message read/unread status
- Full chat history stored in database
- Online/offline presence indicators

### 💕 Matching System
- Like / Unlike other users
- See who liked you
- Mutual likes = Match!
- Filter users by age, gender, location

### 🔒 Security & Auth
- JWT token-based authentication
- Role-based authorization (Admin / Member)
- Password hashing with ASP.NET Identity
- Protected API endpoints

### 🛠️ Admin Panel
- Manage users and roles
- Photo moderation (approve/reject)
- Platform overview

---

## 🏗️ Tech Stack

### Backend — ASP.NET Core
| Technology | Purpose |
|-----------|---------|
| ASP.NET Core 8 Web API | REST API |
| Entity Framework Core | ORM / Database |
| SQL Server | Database |
| ASP.NET Identity | User management |
| JWT Bearer Tokens | Authentication |
| SignalR | Real-time messaging |
| AutoMapper | DTO mapping |
| Cloudinary | Photo storage & upload |

### Frontend — Angular
| Technology | Purpose |
|-----------|---------|
| Angular 17 | SPA Framework |
| TypeScript | Language |
| Bootstrap 5 | UI Styling |
| Angular Route Guards | Protected routes |
| HTTP Interceptors | Auto token attach |
| RxJS | Async / Reactive streams |

---

## 📁 Project Structure

```
DatingApp/
├── API/                            # ASP.NET Core Backend
│   ├── Controllers/
│   │   ├── AccountController.cs    # Register, Login
│   │   ├── UsersController.cs      # Profile CRUD
│   │   ├── MessagesController.cs   # Chat messages
│   │   └── LikesController.cs      # Like / Unlike
│   ├── Hubs/
│   │   └── MessageHub.cs           # SignalR real-time
│   ├── Models/
│   ├── DTOs/
│   ├── Data/
│   └── Helpers/
│
└── client/                         # Angular Frontend
    └── src/app/
        ├── members/                # User profiles
        ├── messages/               # Chat UI
        ├── lists/                  # Likes & matches
        ├── admin/                  # Admin panel
        └── _guards/                # Route protection
```

---

## 🔑 API Endpoints

### Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/account/register` | Register new user |
| POST | `/api/account/login` | Login & get JWT token |

### Users
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/users` | Get all users (with filters) |
| GET | `/api/users/{username}` | Get user profile |
| PUT | `/api/users` | Update own profile |
| POST | `/api/users/add-photo` | Upload profile photo |

### Messages
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/messages` | Send message |
| GET | `/api/messages` | Get inbox / outbox |
| GET | `/api/messages/thread/{username}` | Get chat thread |
| DELETE | `/api/messages/{id}` | Delete message |

### Likes
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/likes/{username}` | Like a user |
| GET | `/api/likes` | Get liked / likedBy list |

---

## ⚙️ Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- SQL Server
- Cloudinary account (free)

```bash
# Clone the repo
git clone https://github.com/YOUR_USERNAME/DatingApp.git
cd DatingApp

# Backend
cd API
# Add TokenKey and Cloudinary keys in appsettings.json
dotnet restore
dotnet ef database update
dotnet run

# Frontend (new terminal)
cd client
npm install
ng serve
```

Open browser at `http://localhost:4200`

---

## 📸 Screenshots

> *(Add screenshots here)*

| Login | Profiles | Chat |
|-------|----------|------|
| ![](#) | ![](#) | ![](#) |

---

## 🎯 What I Built & Learned

- Designed complete **REST API** with clean architecture and repository pattern
- Implemented **real-time bidirectional chat** using SignalR WebSockets
- Built **JWT auth flow** from scratch including role-based access
- Handled **photo uploads** to Cloudinary with transformations
- Created reusable **Angular components**, services, interceptors, and guards
- Applied **pagination, filtering & sorting** at API level
- Used **AutoMapper** for clean DTO-to-Entity mapping

---

## 👨‍💻 Developer

**Muhammad Shehzad** — Full Stack .NET Developer
📧 your.email@gmail.com | 🔗 [GitHub](https://github.com/YOUR_USERNAME) | [Upwork](#) | [Fiverr](#)

---

## 📄 License

MIT License — feel free to use this project as reference.
