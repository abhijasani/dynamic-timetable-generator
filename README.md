
# Dynamic Timetable Generator

This is a full-stack project that dynamically generates a timetable based on user-defined constraints.

- `dynamic-timetable-generator/dynamic_timetable-ui` — Angular standalone app for user interface  
- `dynamic-timetable-generator/Dynamic-Timetable-generator` — .NET Core Web API for generating timetable logic

## 🚀 Frontend (Angular)

📂 Location: `dynamic-timetable-generator/dynamic-timetable-ui`

### 📦 Install Dependencies
```bash
npm install
````

### ▶️ Run App

```bash
ng serve
```

App will run at: [http://localhost:4200](http://localhost:4200)


## 🛠 Backend (ASP.NET Core Web API)

📂 Location: `dynamic-timetable-generator/Dynamic-Timetable-generator`

### 🧱 Build Project

```bash
dotnet build
```

### ▶️ Run Web API

```bash
dotnet run
```

API will be available at: `http://localhost:5237/api/timetable`

---

## ✅ Features

* Input: Working days, subjects per day, total subjects
* Validation: Ensures total subject hours match total calculated hours
* Timetable: Dynamically generated matrix output based on constraints

---

## 📦 Technologies Used

* Angular 19 (Standalone API + Reactive Forms)
* .NET 8 Web API
* TypeScript, C#

