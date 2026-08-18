# Popeye Marina — Juvie Setup

Setup guide for getting the WinForms app running on a new computer. 

## 1. Prerequisites

Install these in order. Don't skip SSMS — you need it to run the schema script, and it doesn't come bundled with Visual Studio.

| Software | Notes |
|---|---|
| [Visual Studio 2022](https://visualstudio.microsoft.com/) | Community edition is fine. During install, select the **.NET desktop development** workload. |
| [SQL Server 2019/2022 Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | The database engine. Free edition, sufficient for this project. |
| [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) | Can be installed directly from the SQL Server installer's completion screen via the "Install SSMS" button. |
| Git | For cloning the repo. |

## 2. Clone the repo

```
git clone https://github.com/<your-username>/popeye-marina.git
cd popeye-marina
```

Open `PopeyeMarina.sln` in Visual Studio.

## 3. Restore NuGet packages

The repo doesn't (and shouldn't) commit `/bin`, `/obj`, or `/packages`. Visual Studio should restore automatically on first build, but if it doesn't:

- Right-click the solution in Solution Explorer → **Restore NuGet Packages**

Confirm you land on **ReaLTaiizor 3.8.2.1** specifically — a newer version can shift the API and break the custom `RoundedPanel`/`RoundedButton` controls.

## 4. Install SQL Server Express

Run the installer, choose **Basic** setup (or Custom if you want to name the instance yourself). Note the values on the completion screen — you'll need them in step 6:

- **Instance name** (default is `SQLEXPRESS`)
- **Connection string** shown on that screen — copy it, it's a good starting point

Click **Install SSMS** on that same screen if you haven't already installed it separately.

## 5. Build the database from schema_sql.sql

1. Open SSMS.
2. In the **Connect to Server** dialog:
   - Server Name: `localhost\SQLEXPRESS` (or `.\SQLEXPRESS`, or whatever instance name you set in step 4)
   - Authentication: **Windows Authentication**
3. Once connected, open `schema_sql.sql` from the repo (**File → Open → File**).
4. Run it (F5 / Execute). This creates `PopeyeMarinaDB` with all 10 tables and one sample lease row.
5. Expand **Databases → PopeyeMarinaDB → Tables** in Object Explorer and confirm all 10 tables exist before moving on. Don't assume the script ran clean — check.

### If you want your existing data too, not just the schema

`schema_sql.sql` only gives you structure plus one test row. To carry over real data from another machine:

1. On the **original** machine, right-click `PopeyeMarinaDB` in SSMS → **Tasks → Generate Scripts**
2. Choose **Schema and Data** as the scripting option
3. Run the generated script against the **new** machine's SSMS

## 6. Point the app at this machine's database

Open `DatabaseHelper.cs` and update the connection string to match this machine's instance:

```csharp
private const string ConnectionString =
    @"Server=localhost\SQLEXPRESS;Database=PopeyeMarinaDB;Trusted_Connection=True;TrustServerCertificate=True;";
```

Notes on each piece:

- **`Server`** — must match *this machine's* instance name, not the original dev machine's. Confirm with `SELECT @@SERVERNAME` in SSMS if unsure.
- **`Trusted_Connection=True`** — uses Windows Authentication, tied to the logged-in Windows user. This means the connection string is not portable between machines/users without editing it — a known limitation, not a bug.
- **`TrustServerCertificate=True`** — required on most fresh Express installs. Without it, connections fail against the self-signed certificate with an unhelpful error, since newer `Microsoft.Data.SqlClient` defaults to `Encrypt=True`.

## 7. Build and run

Build the solution and run through each screen (Customers, Slips, Leases, Records) against the local database to confirm the connection works end to end.

## Troubleshooting

**"Unable to retrieve customers" / generic load errors**
The catch blocks currently swallow the real exception behind a canned message. Temporarily replace the catch body with `MessageBox.Show(ex.ToString())` to see the actual error before guessing. Common causes, in likely order:

1. Missing `TrustServerCertificate=True` (see step 6)
2. `Server` name doesn't match this machine's actual instance
3. `PopeyeMarinaDB` doesn't exist, or the schema script errored partway through — go re-check in SSMS
4. SQL Server (SQLEXPRESS) service isn't running — check **Services** (`services.msc`) for `SQL Server (SQLEXPRESS)`

**Windows Authentication and portability**
Because the connection string uses `Trusted_Connection=True`, this project will only run for whichever Windows user has access to the SQL Server instance on that machine. Handing the project to someone else, or running it on a lab/grading machine, requires either granting that user's Windows login access in SQL Server, or switching to SQL Authentication with a username/password.

## What this setup does *not* do

Cloning the repo and running this guide keeps the **schema** in sync across machines. It does **not** keep **data** in sync — each machine has its own independent local database. Adding a customer on one machine will not appear on the other unless you manually re-run the "Schema and Data" export from step 5.
