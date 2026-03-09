# STIM - Online Game Store

## Original Authors
This project is created and maintained by:

* **Khairul Aqib Bin Jazrul Fuad** 
* **Akmal Hakimi Bin Abd Rashid** 
* **Khaizuran Amshar Bin Mohd Tarmizi** 
* **Chan Boon Hong** 

## Introduction
**STIM** is an ASP.NET Web Forms application developed as part of the **ISB42403 - Web Application Development** course at **Universiti Kuala Lumpur (UniKL MIIT)** during the March 2025 session. The application serves as an online game store where users can browse, purchase, and manage their game library.

The goal of this project is to provide a fully functional web application that allows customers to explore a catalog of games, add them to a shopping cart, complete purchases, and manage their profiles — while giving administrators a dashboard to oversee games and users. The app is powered by **ASP.NET Web Forms** with **SQL Server LocalDB** for data management.

## Problem Statements & Objectives

### Problem Statements
* **Manual Game Purchasing**: Browsing and purchasing games without a centralized digital storefront is inconvenient and lacks organization.
* **Lack of Centralized Management**: Without a digital system, administrators have no unified view of all users, games, and order records.
* **Limited Customer Access**: Customers cannot easily browse available games by category, manage their library, or track purchase history online.

### Objectives
* Develop a web application that allows customers to **browse**, **purchase**, and **manage** their game library
* Implement **Forms Authentication** for secure user registration and login
* Use **SQL Server LocalDB** as the database for storing user profiles, games, orders, and cart data
* Provide an **administrator dashboard** with oversight of all games and users
* Support full **CRUD operations** for both games (admin-side) and user management (admin-side)
* Deliver a clean, modern **UI/UX** with consistent styling and intuitive navigation

## Program Scope
The STIM application allows users to:
* **Browse Games** — View a catalog of games with prices, filterable by category and searchable by name
* **Register & Login** — Create an account with email, username, and password validation
* **Purchase Games** — Add games to cart, review cart contents, and complete checkout
* **Manage Library** — View owned games and recently played titles on the homepage
* **View Profile** — See account details and order history with a logout option
* **Admin Dashboard** — Administrators can manage games (add/edit/delete), manage users, and edit user-owned game libraries

## Tech Stack

| Layer | Technology |
| :--- | :--- |
| `Framework` | ASP.NET Web Forms (.NET Framework 4.7.2) |
| `Language` | C# |
| `Database` | SQL Server LocalDB (GameStoreDb.mdf) |
| `Data Access` | ADO.NET (SqlConnection, SqlCommand, SqlDataReader) |
| `Authentication` | ASP.NET Forms Authentication with Session management |
| `IDE` | Visual Studio 2022 |

## Prerequisites
* **Visual Studio 2022** with ASP.NET and web development workload installed
* **.NET Framework 4.7.2** targeting pack
* **SQL Server LocalDB** (included with Visual Studio)
* **NuGet Package Manager** (included with Visual Studio)

## Getting Started

1. **Clone or extract** the project to your local machine.

2. **Open the solution:**
   ```
   Open WebProject.sln in Visual Studio 2022
   ```

3. **Restore NuGet packages:**
   ```
   Right-click solution → Restore NuGet Packages
   ```

4. **Build the solution:**
   ```
   Build → Build Solution (Ctrl+Shift+B)
   ```

5. **Run the application:**
   ```
   Press F5 to launch with IIS Express (https://localhost:44391/)
   ```

## Project Structure

```
WebProject/
├── WebProject.sln                  # Solution file
├── packages/                       # NuGet packages
└── WebProject/                     # Main web project
    ├── WebProject.csproj           # Project file
    ├── Web.config                  # App configuration & connection string
    ├── Site.Master                 # Master page (shared navbar & layout)
    ├── HomePage.aspx               # User's game library & recently played
    ├── Store.aspx                  # Game catalog with search & category filters
    ├── GameDetails.aspx            # Individual game detail & add-to-cart
    ├── Checkout.aspx               # Shopping cart & order confirmation
    ├── Login.aspx                  # User login page
    ├── Register.aspx               # User registration page
    ├── Profile.aspx                # User profile & order history
    ├── AboutUs.aspx                # About business page
    ├── AdminDashboard.aspx         # Admin control panel hub
    ├── AdminGameManager.aspx       # Admin game CRUD (add/edit/delete)
    ├── AdminUserManager.aspx       # Admin user management
    ├── EditUserGames.aspx          # Admin edit user's owned games
    ├── Styles/                     # CSS stylesheets
    │   ├── StyleSheet1.css         # Main stylesheet
    │   ├── NavBar.css              # Navigation bar styles
    │   └── AboutUs.css             # About page styles
    ├── images/                     # Game cover images & logo
    └── App_Data/
        └── GameStoreDb.mdf         # SQL Server LocalDB database file
```

## Database Schema

### SQL Server Tables (Total: 7)

**`Users`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `UserId` | Int (PK) | Unique user identifier |
| `Username` | String | User's chosen username |
| `PasswordHash` | String | Hashed password |

**`Games`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `GameID` | Int (PK) | Unique game identifier |
| `GameName` | String | Name of the game |
| `GameImage` | String | Path to game cover image |
| `GameDesc` | String | Game description |
| `GamePrice` | Decimal | Price of the game |
| `CatId` | Int (FK) | Reference to Categories |

**`Categories`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `CatId` | Int (PK) | Unique category identifier |
| `CatTitle` | String | Category name (e.g., RPG, FPS) |

**`Cart`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `UserId` | Int (FK) | Reference to Users |
| `GameID` | Int (FK) | Reference to Games |

**`Orders`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `OrderId` | Int (PK) | Unique order identifier |
| `UserId` | Int (FK) | Reference to Users |
| `OrderDate` | DateTime | Date of the order |
| `TotalAmount` | Decimal | Total order amount |

**`OrderDetails`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `OrderId` | Int (FK) | Reference to Orders |
| `GameId` | Int (FK) | Reference to Games |
| `Quantity` | Int | Quantity ordered |

**`UserGames`**

| Field | Type | Description |
| :--- | :--- | :--- |
| `UserId` | Int (FK) | Reference to Users |
| `GameId` | Int (FK) | Reference to Games |
| `LastPlayed` | DateTime | Last time the game was played |

## Application Flow

```
┌──────────────┐
│  Login Page  │──── Register ───►┌────────────────┐
└──────┬───────┘                  │ Register Page  │
       │                          └────────────────┘
       │ Login
       ▼
┌──────────────┐    isAdmin?     ┌──────────────────────┐
│  Auth Check  │──── Yes ───────►│  Admin Dashboard     │
└──────┬───────┘                 │  ├── Game Manager    │
       │                         │  ├── User Manager    │
      No                         │  └── Edit User Games │
       │                         └──────────────────────┘
       ▼
┌──────────────┐
│   HomePage   │ (User's game library & recently played)
└──────┬───────┘
       │
       ▼
┌──────────────────────────────────────────────────┐
│              Site.Master Navigation               │
│  HOMEPAGE  │  STORE  │  ABOUT BUSINESS │ PROFILE │
└──────┬───────────┬────────────┬──────────┬───────┘
       │           │            │          │
       ▼           ▼            ▼          ▼
  ┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐
  │HomePage │ │ Store   │ │ AboutUs │ │ Profile │
  │         │ │         │ │         │ │         │
  └─────────┘ └────┬────┘ └─────────┘ └─────────┘
                   │
              View Game
                   ▼
             ┌────────────┐
             │ GameDetails│─── Add to Cart ──► Cart
             └────────────┘                     │
                                           Checkout
                                                ▼
                                        ┌──────────────┐
                                        │  Checkout    │
                                        │  Order Done! │
                                        └──────────────┘
```

## Features

### Customer Features
* **Registration** — Create an account with email, username, and password with validation (email format, username uniqueness, password length, password confirmation)
* **Login** — Secure username/password authentication with error handling for invalid credentials
* **Homepage** — Personalized game library showing owned games and recently played titles
* **Store** — Browse game catalog with category filters and search bar functionality
* **Game Details** — View individual game information and add to cart
* **Shopping Cart** — Review cart items, view total amount, and proceed to checkout
* **Checkout** — Complete purchase with order confirmation popup
* **Profile** — View account details and order history with logout option
* **About Business** — Learn about STIM's story, mission, and vision

### Administrator Features
* **Admin Dashboard** — Central hub with buttons for managing games and users
* **Game Manager** — Add new games with details (name, image, category, price, description); view all listed games; delete games
* **User Manager** — View all registered users with usernames and emails; update user details; remove user accounts
* **Edit User Games** — View and remove games from individual user libraries
* **Admin Profile** — View admin account info with exclusive Admin Dashboard access

## Members Task Distribution

| Member | Tasks |
| :--- | :--- |
| **Khairul Aqib Bin Jazrul Fuad** | Product Listing (Store page), Shopping Cart, Edit User (Admin) page |
| **Akmal Hakimi Bin Abd Rashid** | Profile page, Login/Registration page |
| **Khaizuran Amshar Bin Mohd Tarmizi** | Game pictures & info, About Business page, CSS & Masterpage, Reporting |
| **Chan Boon Hong** | Homepage, Create database, Edit Games Data (Admin) page |
