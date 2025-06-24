.NET Todo List API - A Backend Learning Project
This project is my journey into building a complete RESTful API from scratch using modern .NET practices. The goal was to create a simple but fully functional backend for a Todo list application, focusing on clean design, database integration, and core API development principles.

This was built as part of a guided learning roadmap to solidify my understanding of the entire backend development lifecycle, from database design to endpoint implementation.

Core Features
This API provides the backend functionality for a complete todo management system. 

Todo Management: Full CRUD (Create, Read, Update, Delete) operations for todo items.
Categories: Users can organize their todos into custom categories , complete with optional color-coding.

Prioritization: Todos can be assigned a priority level (Low, Medium, or High).
Task Tracking: You can track the completion status of each todo  and set due dates to stay on schedule.

User System: Supports basic user management to ensure todos and categories are tied to a specific user.

Technology Stack
I chose a modern, high-performance tech stack for this project:

Framework: ASP.NET Core 9
Language: C#
Database: PostgreSQL
Data Access: Dapper (a high-performance micro-ORM)
Containerization: Docker for running the PostgreSQL database consistently.
API Endpoints
Here is a summary of the available API endpoints.

Users
Method	Endpoint	Description
POST	/api/users	Registers a new user.
GET	/api/users/{id}	Gets a specific user's public information.
DELETE	/api/users/{id}	Deletes a user and all their associated data.

Export to Sheets
Categories
Method	Endpoint	Description
POST	/api/categories	Creates a new category for a user. 
GET	/api/categories	Lists all categories for a specific user (using ?userId=...). 
GET	/api/categories/{id}	Gets a single category by its ID.
PUT	/api/categories/{id}	Updates a category's name or color.
DELETE	/api/categories/{id}	Deletes a category.



Export to Sheets
Todos
Method	Endpoint	Description
POST	/api/todos	Creates a new todo item. 
GET	/api/todos	Lists all todos for a user with optional filtering. 
GET	/api/todos/{id}	Gets a single todo item by its ID. 
PUT	/api/todos/{id}	Updates a todo item (e.g., content, status, priority). 
DELETE	/api/todos/{id}	Deletes a todo item. 
