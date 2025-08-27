## DevTaskFlow
AI-Powered Task Assigner for Developer Teams - [DevTaskFlow](http://devtaskaskflow.runasp.net/)<br>

**📌 Overview** <br>
DevTaskFlow is an AI-powered smart task assignment tool for developer teams.
It automatically matches tasks to the most suitable developers, balances workloads, and adapts to priority changes in real-time.<br>

**🚨 The Problem**
<li>Developer teams often face challenges such as:

<li>Skill mismatches – Tasks assigned without considering actual expertise

<li>Unbalanced workloads – Some developers overloaded while others are underutilized

<li>Rigid assignments – Tasks don’t adapt when priorities change

**Research-backed insights:**

<li>58% of developers work on mismatched tasks (2023 Stack Overflow Survey)

<li>15% of sprint planning time is lost on manual assignment (2022 Atlassian Report)

<li>Imbalanced workloads accelerate burnout (Harvard Business Review, 2021)<br>

**💡 Why DevTaskFlow**<br>
DevTaskFlow solves these issues with AI-powered task allocation:

<li>Extracts skills automatically from task descriptions using Gemini AI

<li>Matches the right developer to the right task

<li>Ensures fair workload distribution

<li>Handles priority changes dynamically (urgent tasks override less critical ones)

<li>Prevents burnout by enforcing a hard cap of 5 active tasks per developer<br>

**🎯 Motivation**<br>
Built with developers in mind, DevTaskFlow aims to:

<li>Eliminate inefficient manual task assignment

<li>Reduce burnout risks with balanced workloads

<li>Provide managerial transparency without micromanagement<br>

**🔐 Role-Based Access**<br>
<li><b>Admin</b> → Full control with override capabilities

<li><b>Manager</b> → Create and manage tasks, track progress

<li><b>Developer</b> → View, manage, and update assigned tasks

<li><b>Guest</b> → Temporary, read-only access for oversight<br>

**⚖️ Feature Comparison**<br>
**Task Assignment**

<li>Jira / Trello: Manual guesswork
<li>DevTaskFlow: AI auto-assigns by skills & availability<br>

**Workload Management**

<li>Jira / Trello: No task limits
<li>DevTaskFlow: Enforces max 5 active tasks per developer<br>

**Skill Matching**

<li>Jira / Trello: Not considered
<li>DevTaskFlow: Extracts skills via Gemini AI<br>

**Priority Handling**

<li>Jira / Trello: Labels only
<li>DevTaskFlow: AI rebalances tasks dynamically<br>



**🛠 Tech Stack**<br>
**Backend**

<li>.NET Core

<li>Entity Framework (ORM)

<li>SQL Server

**Frontend**

<li>HTML5, CSS3, JavaScript

<li>jQuery, Bootstrap

<li>Google Visualization (PieChart for dashboards and metrics)

**AI & Utilities**

<li>Gemini AI → Skill extraction & smart matching

<li>AutoMapper → Object-to-object mapping

<li>ClosedXML → Generate Excel exports/reports

<li>Serilog → Logging and monitoring

🚀 Getting Started
bash
# 1. Clone the repository
git clone https://github.com/sairamu-dev/DevTaskFlow.git

# 2. Navigate into project
cd devtaskflow

# 3. Update appsettings.json
Configure your SQL Server connection string & Serilog settings<br>
generate your gemini api key & replace in the file

# 4. Run Database Migrations
dotnet ef database update

# 5. Build and Run
dotnet run
Access the application:
http://localhost:5000 (default port)

**🤝 Contributing**
<li>Contributions are welcome!

<li>Open issues for bugs and suggestions

<li>Submit pull requests with improvements or enhancements

**📄 License**<br>
This project is licensed under the MIT License.<br>
See the LICENSE file for details.
