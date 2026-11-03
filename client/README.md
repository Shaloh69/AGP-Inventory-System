# Client – C# Desktop App (.NET)

The frontend the user sees. It talks **only** to the Express API over HTTPS.

> Not built yet. Follow the steps below (and **Phase 4–5** of
> [../docs/Beginners-Guide.pdf](../docs/Beginners-Guide.pdf)).

## Requirements

- Windows 10/11
- Visual Studio 2022 or newer (Community edition is free) with the
  **".NET desktop development"** workload
- .NET SDK (current LTS – check with `dotnet --version`)

## Create the project (once, by one team member)

**Option A – Visual Studio:** *Create a new project → Windows Forms App (or WPF Application)
→ Name: `AppClient` → Location: this `client` folder → uncheck "Place solution and project
in the same directory"*.

**Option B – terminal:**

```bash
cd client
dotnet new sln -n AppClient
dotnet new winforms -n AppClient      # or: dotnet new wpf -n AppClient
dotnet sln add AppClient/AppClient.csproj
```

The existing sub-folders (`Forms`, `Models`, …) will be kept.

## Folder purpose

| Folder | What goes inside | Example |
|--------|------------------|---------|
| `AppClient/Forms/` | Windows / screens (WPF: call it `Views/`) | `LoginForm.cs`, `MainForm.cs` |
| `AppClient/Models/` | Classes matching the JSON from the API | `TaskItem.cs`, `User.cs` |
| `AppClient/Services/` | All HTTP calls in one place | `ApiClient.cs` |
| `AppClient/Helpers/` | Reusable utilities | `Validator.cs`, `AppConfig.cs` |
| `AppClient/Assets/` | Images, icons, logos | `logo.png` |

**Rule:** forms never use `HttpClient` directly – they call methods in `Services/ApiClient.cs`.

## Install a UI library (NuGet)

*Right-click the project → Manage NuGet Packages → Browse*, or:

```bash
cd client/AppClient
dotnet add package MaterialSkin.2          # WinForms example
dotnet add package MaterialDesignThemes    # WPF example
```

See [../docs/UI-LIBRARIES.md](../docs/UI-LIBRARIES.md) for the full list and recommendations.

## API base URL

Keep the server address in **one** place (e.g. `Helpers/AppConfig.cs`):

| Environment | Base URL |
|-------------|----------|
| Local testing | `http://localhost:3000/` |
| Production | `https://agp-api.onrender.com/` |

The URL must end with `/`. Use an `HttpClient` timeout of about 90 seconds because
Render's free plan can take time to wake up.

## Naming conventions

- Classes, methods, properties: `PascalCase` (`LoadTasksAsync`)
- Local variables, parameters: `camelCase` (`taskTitle`)
- Controls: prefix by type – `btnSave`, `txtUsername`, `lblStatus`, `dgvTasks`
- Async methods end with `Async`
