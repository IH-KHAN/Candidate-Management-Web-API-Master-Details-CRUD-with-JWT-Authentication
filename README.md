# Candidate Management Web API

A robust ASP.NET Core Web API application demonstrating a Master-Details relationship, secured with JSON Web Token (JWT) authentication. This project manages a directory of job candidates, their associated skills, and their profile images.

## Features

* **JWT Authentication:** Secure API endpoints using token-based authentication.
* **Master-Details CRUD Operations:** Manage `Candidate` (Master) records alongside their associated `CandidateSkills` (Details).
* **Image Upload Handling:** Supports multipart/form-data for uploading and storing candidate profile pictures on the server.
* **Entity Framework Core:** Utilizes Code-First approach with SQL Server for database modeling and migrations.
* **Seeded Data:** Automatically seeds initial skill categories (C++, PHP, Java, C#) into the database.
* **Swagger Integration:** Includes OpenAPI documentation for easy endpoint testing and exploration.

## Technologies Used

* ASP.NET Core Web API (Targeting .NET 10.0)
* Entity Framework Core (SQL Server)
* JWT (JSON Web Tokens) for Authentication
* C#

## Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) (Version matching the project target, e.g., .NET 10.0)
* SQL Server (LocalDB or a dedicated instance)
* IDE: Visual Studio 2022, Visual Studio Code, or Rider
