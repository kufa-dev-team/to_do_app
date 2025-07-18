<<<<<<< HEAD
# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react) uses [Babel](https://babeljs.io/) for Fast Refresh
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh

## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

```js
export default tseslint.config([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...

      // Remove tseslint.configs.recommended and replace with this
      ...tseslint.configs.recommendedTypeChecked,
      // Alternatively, use this for stricter rules
      ...tseslint.configs.strictTypeChecked,
      // Optionally, add this for stylistic rules
      ...tseslint.configs.stylisticTypeChecked,

      // Other configs...
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```

You can also install [eslint-plugin-react-x](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-x) and [eslint-plugin-react-dom](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-dom) for React-specific lint rules:

```js
// eslint.config.js
import reactX from 'eslint-plugin-react-x'
import reactDom from 'eslint-plugin-react-dom'

export default tseslint.config([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...
      // Enable lint rules for React
      reactX.configs['recommended-typescript'],
      // Enable lint rules for React DOM
      reactDom.configs.recommended,
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```
=======
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
>>>>>>> 50ab02b7d54bf8cefcd9b2b7963e1531fb94dc3e
#   t o _ d o _ a p p  
 #   t o _ d o _ a p p  
 