
# Upster API

**Upster** is a sample API project written in **C# (.NET)** designed to simplify the process of **creating**, **updating**, and **soft deleting** records through a single `POST` API. The project focuses on simplicity, ease of integration, and extensibility for real-world systems.

## 🔧 Technologies Used

* ASP.NET Core Web API (.NET 8)
* Entity Framework Core
* C#
* Visual Studio

## ⚙️ Key Features

### Unified `POST` Endpoint

A single `POST` endpoint handles all three operations:

* **Create**: When no ID is provided or the ID does not exist in the system.
* **Update**: When a valid ID exists and data needs to be updated.
* **Soft Delete**: When a flag (e.g., `IsDeleted` or `IsActive`) is included in the payload to indicate deletion. The record is not physically removed from the database.

### Soft Delete

* Records are not physically deleted from the database.
* Marked using an `IsDeleted` (or similar) field.
* Can be restored or excluded during queries.

## 📁 Project Structure

```plaintext
Upster/
├── APICommon.sln             # Solution file
├── .vs/                      # Visual Studio config
├── Test/                     # Main API project
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   └── ...
```

## ▶️ Getting Started

1. Clone the repository:

```bash
git clone https://github.com/dungzum22/Upster.git
```

2. Open the solution in Visual Studio.

3. Run migrations and start the API.

4. Send a `POST` request to the unified endpoint to create, update, or soft delete data.

## 📌 Notes

* This is a sample project and is **not recommended** for direct use in production environments.
* You can extend it with authentication, logging, and exception handling as needed.

