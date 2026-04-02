# Copilot Instructions For This Repository

## Project Shape

- This repository contains a single ASP.NET Core MVC application in `Romulus.Web` targeting `net10.0`.
- Use the existing feature-folder structure under `Romulus.Web/Features` instead of creating traditional `Controllers` and `Views` folders.
- Keep startup and hosting code in `Romulus.Web/Program.cs` minimal and explicit.

## Code Style

- Follow `.editorconfig` exactly.
- Use file-scoped namespaces in C# files.
- Use 4 spaces for C# indentation and 2 spaces for XML-based project files.
- Prefer `var` where the type is obvious, matching the repository style.
- Preserve nullable annotations and do not introduce null-forgiving operators unless they are clearly justified.
- Keep existing naming and formatting conventions; avoid broad refactors when making targeted changes.

## Analyzer Expectations

- The project treats many warnings as errors. Assume analyzer cleanliness is required for every change.
- Pay special attention to globalization and string-comparison rules such as `CA1309` and `CA1310`.
- Prefer explicit `StringComparison` for string comparisons and replacements when applicable.
- Avoid banned APIs and patterns that would trigger Roslynator, Meziantou, SecurityCodeScan, or IDisposable analyzer violations.

## Application Structure

- MVC controllers live under `Romulus.Web/Features/<FeatureName>`.
- Views are resolved through the custom feature-folder conventions in `Romulus.Web/Infrastructure/FeatureConvention.cs` and `Romulus.Web/Infrastructure/FeatureViewLocationExpander.cs`.
- New controllers should be public unless there is a verified reason controller discovery does not require it.
- Shared infrastructure belongs under `Romulus.Web/Infrastructure`.
- Mail transport abstractions live under `Romulus.Web/Services`.

## Startup And Middleware

- Treat middleware ordering in `Romulus.Web/Program.cs` as behavior-sensitive.
- `UseForwardedHeaders` must stay early because the app runs behind a same-host reverse proxy.
- If you change compression, caching, static files, security headers, logging, or exception handling, review the full request pipeline rather than editing a single line in isolation.
- Static-file, compression, and security-header behavior should be considered together because ordering affects which responses receive headers and compression.

## Configuration And Operations

- The app is expected to run behind a reverse proxy on the same host.
- Do not assume Azure App Configuration is active; the registration is currently commented out in startup.
- Prefer configuration-driven behavior over hardcoded environment switches when adding new operational settings.
- Avoid committing secrets, API keys, or connection strings.

## Performance And Reliability

- Prefer source-generated or allocation-conscious patterns already used in the repo, such as `System.Text.Json` source generation.
- Avoid introducing unnecessary LINQ or per-request allocations on hot paths.
- Do not add blocking I/O to request paths when a background or deferred approach is more appropriate.
- Be cautious with network calls in controller actions or mediator handlers; request latency matters for this site.

## When Making Changes

- Keep edits focused on the user request.
- Update related infrastructure when needed, not just the call site.
- If changing routes, feature folders, headers, proxy handling, or transports, explain the runtime impact in the final response.
- Prefer concrete verification steps such as `dotnet build MadhonMVC4.sln` when validation is appropriate.