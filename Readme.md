# Employee Management API (Raw ASP.NET Core)

A pure, scratch-built RESTful Web API using the **ASP.NET Core Empty Template**. This project avoids default framework magic to focus heavily on core backend concepts like middleware pipeline configuration, dependency injection, and raw routing mechanics.

## 🚀 Key Features

* **Built from Scratch:** Configured the request pipeline and routing completely from an empty `Program.cs` file.
* **RESTful Architecture:** Strictly follows REST conventions (proper use of `200 OK`, `201 Created` with `Location` headers, `204 No Content`, and `404 Not Found`).
* **In-Memory Datastore:** Uses `List<T>` and LINQ for high-performance, non-persistent CRUD operations without relying on heavy ORMs.
* **API Documentation:** Integrated **Swagger UI** for seamless endpoint testing without needing a decoupled frontend client.

## 🛠️ Tech Stack

* **Language:** C# 10
* **Framework:** .NET 6 (ASP.NET Core Empty)
* **Testing Tool:** Swagger (Swashbuckle.AspNetCore)

## 📡 API Endpoints

| HTTP Method | Endpoint | Description | Status Codes |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/employees` | Retrieves a list of all employees. | 200 |
| `GET` | `/api/employees/{id}`| Retrieves a specific employee by ID. | 200, 404 |
| `POST` | `/api/employees` | Creates a new employee record. | 201 (Returns Location Header) |
| `PUT` | `/api/employees/{id}`| Updates an existing employee's details. | 204, 404 |
| `DELETE` | `/api/employees/{id}`| Removes an employee from the system. | 204, 404 |

## 🧠 System Architecture Notes

This project demonstrates the transition from a monolithic MVC mindset to a modern API-first approach. By decoupling the UI/UX rendering from the server, this API is strictly responsible for business logic and JSON data serialization, making it highly scalable and ready to be consumed by any client (Vanilla JS, React, Mobile Apps, etc.).

## 🏃‍♂️ How to Run

1. Clone this repository to your local machine.
2. Open the directory in your terminal.
3. Install the required Swagger package (if not restored automatically):

```bash
dotnet add package Swashbuckle.AspNetCore
```

4. Run the application:
```bash
dotnet run
```

5. Open your browser and navigate to the Swagger UI:
```text
http://localhost:<PORT>/swagger
```