# 📊 GuessIT — **IN DEVELOPEMENT**

### This application is an online test management system designed to create, assign, and evaluate quizzes and exams efficiently. It provides tools for managing users, creating tests and questions, conducting test sessions, and analyzing results.


## Tech Stack

- **Backend**: C#, ASP.NET Core Web API  
- **Frontend**: Angular, TypeScript  
- **Database**: PostgreSQL
- **Authentication**: JWT  
- **Architecture**: Repository-Service-Controller pattern  

## Features

- **Test Creation**: Teachers can create tests with multiple question types and assign them to users.  
- **Question Management**: Add, edit, or remove questions and organize them into test banks.  
- **Test Taking Process**: Users can take tests in a guided session with real-time validation.  
- **Statistics and Reports**: View detailed performance statistics for users, tests, and questions.  
- **User Management**: Manage users, roles, and permissions within the platform.  
- **Authentication & Security**: JWT-based authentication ensures secure access to APIs.  
- **Containerization**: Backend, frontend, and PostgreSQL database are fully Dockerized for easy deployment.

## Architecture Overview

- **Repository Layer**: Handles all database operations.  
- **Service Layer**: Contains business logic for managing tests, questions, and users.  
- **Controller Layer**: Exposes REST API endpoints for the frontend.
