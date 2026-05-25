# Project Task Management API

## Overview
A backend system built using ASP.NET Core Web API following Clean Architecture principles.

The system allows authenticated users to manage:
- Projects
- Tasks inside projects

## Technologies Used

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Clean Architecture
- Dependency Injection

## Features

### Authentication
- Register
- Login with JWT Token

### Projects
- Create Project
- Get All Projects
- Get Project By Id
- Update Project
- Delete Project

### Tasks
- Create Task
- Get Tasks By Project
- Update Task Status
- Delete Task

## Architecture

Project structure:

ProjectTaskManagement.API
ProjectTaskManagement.Application
ProjectTaskManagement.Domain
ProjectTaskManagement.Infrastructure

## Database

Run migrations:

```powershell
Update-Database
