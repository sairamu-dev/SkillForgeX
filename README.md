# SkillForgeX
AI-Powered Smart Task Assigner for Developer Teams - [Live Demo](http://skillforgex.runasp.net/)

### Table of Contents
- Overview

- The Problem

- Why SkillForgeX

- Motivation

- Role-Based Access

- Feature Comparison

- Tech Stack

- Getting Started

- Contributing

- License

### Overview
SkillForgeX is an AI-powered task assignment tool designed for developer teams.
It ensures that tasks are matched to the most suitable developers, balances workloads, and adapts automatically to shifting priorities in real time.

### The Problem
**Developer teams often face:**

- Skill mismatches (assignments not aligned with expertise)

- Unbalanced workloads (some developers overloaded, others underutilized)

- Rigid task allocation (lacking adaptability to shifting priorities)

**Supporting research:**

- 58% of developers are assigned mismatched tasks **(Stack Overflow, 2023)**

- 15% of sprint planning is wasted on manual assignment **(Atlassian, 2022)**

- Imbalanced workloads accelerate burnout **(Harvard Business Review, 2021)**

### Why SkillForgeX
- AI-powered skill extraction from task descriptions (via Gemini AI)

- Smart task-developer matching based on availability and expertise

- Balanced workload management enforcing a cap of 5 active tasks per developer

- Dynamic priority handling for urgent tasks

- Burnout prevention by avoiding overloads

### Motivation
- SkillForgeX was built with developer well-being and team efficiency in mind:

- Eliminates inefficient manual task assignment

- Prevents burnout with balanced workloads

- Provides transparency for managers without the need for micromanagement

### Role-Based Access
**Role	Capabilities**
- Admin	- Full control with override permissions
- Manager	- Create & manage tasks, monitor progress
- Developer	- View, manage, and update assigned tasks
- Guest	- Manager rights granted for oversight (limited time).

### Feature Comparison
| Feature             | Jira / Trello         | SkillForgeX                               |
|---------------------|-----------------------|-------------------------------------------|
| Task Assignment     | Manual                | AI auto-assigns by skill & availability   |
| Workload Management | None                  | Max 5 active tasks per developer          |
| Skill Matching      | Not supported         | AI-powered                                |
| Priority Handling   | Labels only           | Dynamic rebalancing                       |


### Tech Stack
**Backend:**

- .NET Core

- Entity Framework (ORM)

- SQL Server
- Repository pattern

**Frontend:**

- HTML5, CSS3, JavaScript

- jQuery, Ajax, Bootstrap

- Google Visualization (PieCharts for dashboards)

**AI & Utilities:**

- Gemini AI – Skill extraction & task matching

- AutoMapper – Object-to-object mapping

- ClosedXML – Excel reporting & exports

- Serilog – Logging & monitoring

### Note: 
- This is currently a **prototype / proof of concept**.  
- It demonstrates the idea of AI-powered task assignment and workload balancing but is **not yet production-ready.**
- Future updates will focus on making users and skills **fully configurable.**

### Getting Started
Clone the Repository:

**bash**
- git clone https://github.com/sairamu-dev/SkillForgeX.git
- Navigate into the Project Directory:
- cd SkillForgeX
  
**Configure appsettings.json:**

- Add SQL Server connection string - **(in this project i'm provided sql scripts)**
- Update Serilog logging settings
- Insert Gemini AI API key - [get your api key here](https://aistudio.google.com/apikey)

**Run Database Migrations:**

bash
- dotnet ef database update
- Build & Run the Application:
- dotnet run
- Access the application at: http://localhost:5000

### Contributing
**We welcome contributions:**

- Fork the repository

- Create a feature branch

- Commit your changes

- Submit a Pull Request

### License
- This project is licensed under the MIT License.
- See the [MIT License](https://opensource.org/licenses/MIT) for details.
