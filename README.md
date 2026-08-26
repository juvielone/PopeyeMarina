# Popeye Marina — Setup Guide (App A + App B)

Setup guide for getting **both** applications running on a new computer, sharing one database.

- **App A** — `PopeyeMarina` (WinForms, the original desktop app)
- **App B** — `PopeyeMarina.Web` (ASP.NET Core MVC, the web version)
- **PopeyeMarina.Core** — shared class library both apps reference (Data/Models/Config). You don't run this directly.

Both apps point at the *same* `PopeyeMarinaDB` database. Changes made in one are immediately visible in the other, as long as they're both configured to point at the same SQL Server instance.

## 1. Prerequisites

| Software | Notes |
|---|---|
| [Visual Studio 2022](https://visualstudio.microsoft.com/) | Community edition is fine. During install, select **both** the **.NET desktop development** workload (for App A) and the **ASP.NET and web development** workload (for App B). |
| [SQL Server 2019/2022 Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) | The database engine. Free edition is sufficient. |
| [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) | Installable directly from the SQL Server installer's completion screen via "Install SSMS". |
| Git | For cloning the repo. |

## 2. Clone the repo

```
git clone https://github.com/<your-username>/popeye-marina.git
cd popeye-marina
```

Open `PopeyeMarina.sln` in Visual Studio. You should see three projects: `PopeyeMarina`, `PopeyeMarina.Core`, `PopeyeMarina.Web`.

## 3. Restore NuGet packages

The repo doesn't commit `/bin`, `/obj`, or `/packages`. Visual Studio restores automatically on first build; if it doesn't:

- Right-click the solution → **Restore NuGet Packages**

Confirm `PopeyeMarina` lands on **ReaLTaiizor 3.8.2.1** specifically — a newer version can shift the API and break the custom `RoundedPanel`/`RoundedButton` controls.

## 4. Install SQL Server Express

Run the installer, choose **Basic** setup (or Custom to name the instance yourself). Note the values on the completion screen:

- **Instance name** (default is `SQLEXPRESS`)
- **Connection string** shown on that screen

Click **Install SSMS** on that same screen if you haven't installed it separately.

## 5. Build the database from PopeyeMarina_Schema.sql

1. Open SSMS.
2. **Connect to Server** dialog:
   - Server Name: `localhost\SQLEXPRESS` (or `.\SQLEXPRESS`, or your instance name)
   - Authentication: **Windows Authentication**
3. Open `PopeyeMarina_Schema.sql` from the repo root (**File → Open → File**). This is the current source of truth — it includes `RentalBoat` and `BoatHire`, which older exports of this schema were missing.
4. Run it (F5). This creates `PopeyeMarinaDB` with all 12 tables and sample rows for each feature area (customer, boat, slip, lease, rental boat, boat hire).
5. Expand **Databases → PopeyeMarinaDB → Tables** and confirm all 12 tables exist before moving on. Don't assume the script ran clean — check.

### If you want existing data too, not just the schema

`PopeyeMarina_Schema.sql` only gives you structure plus sample rows. To carry over real data from another machine:

1. On the **original** machine, right-click `PopeyeMarinaDB` in SSMS → **Tasks → Generate Scripts**
2. Choose **Schema and Data** as the scripting option
3. Run the generated script against the **new** machine's SSMS

## 6. Configure appsettings.json for both apps

Neither app hardcodes its connection string anymore — both read from `appsettings.json`, which is gitignored (only `appsettings.json.example` is committed). You need to create the real file in **both** projects.

### App A (PopeyeMarina)

Create `PopeyeMarina/appsettings.json`, copying the shape of `appsettings.json.example`:

```json
{
  "ConnectionStrings": {
    "PopeyeMarinaDB": "Server=localhost\\SQLEXPRESS;Database=PopeyeMarinaDB;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

### App B (PopeyeMarina.Web)

Create/edit `PopeyeMarina.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "PopeyeMarinaDB": "Server=localhost\\SQLEXPRESS;Database=PopeyeMarinaDB;Integrated Security=True;TrustServerCertificate=True;"
  },
  "Admin": {
    "Username": "manager",
    "PasswordHash": "<paste your generated BCrypt hash here>"
  }
}
```

**Both connection strings must point at the same server/instance** — this is what makes the two apps share data. If they differ even slightly (different instance name, different auth mode), each app will happily run against its own separate database with no error, and you'll spend a while confused about why changes aren't showing up in the other app.

Notes on each connection string piece:

- **`Server`** — must match *this machine's* instance name, not the original dev machine's. Confirm with `SELECT @@SERVERNAME` in SSMS if unsure.
- **`Integrated Security=True`** — Windows Authentication, tied to the logged-in Windows user. Not portable between machines/users without editing — see the Windows Auth section below.
- **`TrustServerCertificate=True`** — required on most fresh Express installs, since `Microsoft.Data.SqlClient` defaults to `Encrypt=True` and a fresh Express instance's self-signed certificate isn't trusted otherwise.

### Generating the admin password hash

App B's login isn't in the database — it's a single hardcoded admin checked against a BCrypt hash in config. Generate the hash once via a throwaway `Program.cs`/console snippet (or a scratch `.csx`):

```csharp
Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("your-chosen-password"));
```

Paste the output into `PasswordHash` above. Never put the plaintext password in `appsettings.json`.

## 7. Build and run both apps

To verify the shared-database setup properly, run both apps at once:

- Right-click the solution → **Set Startup Projects** → **Multiple startup projects** → set both `PopeyeMarina` and `PopeyeMarina.Web` to **Start**.
- Press F5. Both the WinForms window and a browser tab (App B's login page) should open.

Log into App B with the admin credentials you set in step 6, and go through both apps' screens:

- **App A**: Dashboard, Leases, Slips, Customers, Records, Boat Hire
- **App B**: Dashboard, Leases, Customers, Records (Docks/Slips), Boats, Boat Hire

Then confirm sync in both directions:

1. Add a customer in App A → refresh App B's Customers page → confirm it appears.
2. Add a boat in App B → check App A's Records → Boats tab (or SSMS) → confirm it's there.

If both show the same data without restarting either app, the shared-database setup is working correctly.

## Troubleshooting

**"Unable to retrieve customers" / generic load errors (either app)**
Common causes, in likely order:

1. `appsettings.json` is missing entirely, or wasn't copied to the output folder — for App A specifically, check the `.csproj` has the `CopyToOutputDirectory` entry for `appsettings.json`; a missing file throws immediately on startup, not silently.
2. Missing `TrustServerCertificate=True`.
3. `Server` name in one or both `appsettings.json` files doesn't match this machine's actual instance.
4. The two apps' connection strings point at *different* instances/databases — each app works individually but they don't sync. Re-check both files side by side.
5. `PopeyeMarinaDB` doesn't exist, or the schema script errored partway through — re-check in SSMS.
6. SQL Server (SQLEXPRESS) service isn't running — check **Services** (`services.msc`) for `SQL Server (SQLEXPRESS)`.

**App B specifically: can't log in / "Invalid credentials" even with the right password**
The `PasswordHash` in `appsettings.json` wasn't generated correctly, or the plaintext password used to generate it doesn't match what you're typing at login. Regenerate the hash and re-paste it — there's no recovery/reset flow by design, since this is a single hardcoded admin account, not a real user store.

**Windows Authentication and portability**
Because both connection strings use `Integrated Security=True` / `Trusted_Connection=True`, both apps will only run for whichever Windows user has access to the SQL Server instance on that machine. Handing the project to someone else, or running it on a lab/grading machine, requires either granting that user's Windows login access in SQL Server, or switching both apps' connection strings to SQL Authentication (username/password) — the connection string syntax changes but no C# code needs to change, since both `DatabaseHelper` implementations just pass through whatever string is in config.

## What this setup does *not* do

Cloning the repo and running this guide keeps the **schema** in sync across machines. It does **not** keep **data** in sync — each machine has its own independent local database unless you deliberately point multiple machines' `appsettings.json` at one shared SQL Server instance over the network (not just `localhost`), which is a different, more involved setup than what's covered here. The live-sync behavior described in step 7 only applies to **App A and App B running on the same machine, against the same local instance** — that's the actual assessment requirement, not multi-machine deployment.
