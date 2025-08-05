# WaterLogger (Razor Pages Project)

This project is a **Razor Pages** application built to practice web development with ASP.NET Core. The main goal was to follow a tutorial that introduces Razor Pages fundamentals, then extend it with additional features and styling.

---

## **About Razor Pages**

Razor Pages is a simplified web application model in ASP.NET Core. Unlike MVC, it uses **file-based routing**, meaning each `.cshtml` page under the `Pages` directory corresponds directly to an endpoint. This approach removes much of the overhead of traditional MVC, making it beginner-friendly while still being powerful enough for production apps.

One key advantage of learning Razor Pages is that it uses **Razor syntax**, the same syntax shared by **MVC** and **Blazor**. This makes your skills transferable across many .NET technologies.

---


## **Features Implemented**

* Add, update, and delete water intake records.
* Track quantity as **decimal values** (e.g., `2.5 L`).
* Filter records by **type** (Glass, Big Bottle, etc.).
* Extended functionality to **track an additional habit** (e.g., Food).

---

## **Challenges Completed**

1. Allow splitting water intake (e.g., `2.5` liters).
2. Add filters to view records by **Glass** or **Big Bottle**.
3. Add tracking for an additional habit (e.g., **Food**) alongside water.

---

## **Tech Stack**

* **ASP.NET Core Razor Pages**
* **C#**
* **ADO.Net**
* **SQLite**
* **Bootstrap 5**
---

## **How to Run**

1. Clone the repository.

   ```bash
   git clone https://github.com/Eyad-Mostafa/CodeReviews.MVC.WaterLogger
   ```
2. Open the project in Visual Studio.
3. Update the connection string in `appsettings.json` **if needed**.
4. Run the project (F5 or `dotnet run`).
