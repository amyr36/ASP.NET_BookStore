# ASP.NET Web Forms Bookstore

A bookstore web application developed using **ASP.NET Web Forms**. The project provides a web-based interface for managing and browsing books, with separate sections for administrators and users.

## Overview

This project was developed to practice building database-driven web applications using the ASP.NET Web Forms framework.

The application is organized into separate areas for **Admin** and **User** functionality, with book-related resources and application data managed within the project.

## Features

- Bookstore web interface
- Browse available books
- Book-related images and resources
- Separate Admin and User sections
- Server-side application logic using ASP.NET
- Web Forms-based pages
- Application data management
- Client-side JavaScript functionality

## Technology Stack

### Backend

- ASP.NET Web Forms
- C#
- .NET Framework

### Frontend

- HTML
- CSS
- JavaScript
- ASP.NET Web Forms Controls

### Database

- SQL Server / ASP.NET application data

> The exact database configuration depends on the connection string used by the project.

## Project Structure

```text
Bookstore/
│
├── App_Code/
│   └── Application classes and business logic
│
├── App_Data/
│   └── Application/database data
│
├── App_Start/
│   └── Application startup and configuration
│
├── bin/
│   └── Compiled assemblies
│
├── Content/
│   └── Images/
│       └── Books/
│           └── Book images
│
├── fonts/
│   └── Font resources
│
├── obj/
│   └── Build-generated files
│
├── Pages/
│   ├── Admin/
│   │   └── Administrator pages
│   │
│   └── User/
│       └── User-facing pages
│
├── Properties/
│   └── Project properties
│
└── Scripts/
    └── WebForms/
        └── MSAjax/
            └── JavaScript libraries
```

## Main Sections

### Admin

The `Pages/Admin` directory contains pages intended for administrators.

Typical administrative functionality may include:

- Managing books
- Adding new books
- Updating book information
- Removing books
- Managing bookstore data

### User

The `Pages/User` directory contains pages designed for normal users.

Typical user functionality includes:

- Browsing books
- Viewing book information
- Navigating the bookstore
- Interacting with available bookstore features

### Book Images

Book-related images are stored under:

```text
Content/Images/Books/
```

This allows the application to associate visual content with books displayed on the website.

## Getting Started

### Prerequisites

To run this project, you will typically need:

- Windows
- Visual Studio
- .NET Framework
- IIS Express or IIS
- SQL Server, if the project uses an external SQL Server database

### 1. Clone the Repository

```bash
git clone <repository-url>
cd <project-directory>
```

### 2. Open the Project

Open the project or solution file in **Visual Studio**.

Make sure the required .NET Framework version is installed.

### 3. Configure the Database

If the application uses SQL Server, configure the database connection string in the project's configuration file.

For example:

```xml
<connectionStrings>
    <add name="BookstoreConnection"
         connectionString="YOUR_CONNECTION_STRING"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

Replace the connection string with the appropriate configuration for your local environment.

### 4. Build the Project

In Visual Studio:

```text
Build → Build Solution
```

or use:

```text
Ctrl + Shift + B
```

### 5. Run the Application

Start the application using Visual Studio:

```text
F5
```

or:

```text
Ctrl + F5
```

The application will run using IIS Express or the configured web server.

## Architecture

The project follows the traditional **ASP.NET Web Forms** architecture.

```text
User
  │
  ▼
Web Forms Pages
  │
  ├── Admin Pages
  │
  └── User Pages
  │
  ▼
C# Application Logic
  │
  ▼
Database
```

ASP.NET Web Forms handles server-side page processing and controls, while HTML, CSS, and JavaScript provide the client-side interface.

## Purpose

The main purpose of this project is to demonstrate practical experience with:

- ASP.NET Web Forms
- C#
- Server-side web development
- Database-driven applications
- Web Forms controls
- Admin/User separation
- Static resources and media management
- JavaScript integration

## Future Improvements

Possible improvements include:

- User authentication and authorization
- Shopping cart
- Online ordering
- Book categories
- Book search and filtering
- Pagination
- Book reviews and ratings
- Improved responsive design
- Payment integration
- REST API
- Migration to ASP.NET Core

## Author

**Amir Hossein Hamidi**

GitHub: [@amyr36](https://github.com/amyr36)

## License

This project is intended for educational and learning purposes.
