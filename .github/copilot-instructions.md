# Copilot Coding Agent Instructions

## Repository Overview

Portable .NET 6 console application demonstrating cross-platform resource loading (localized text and binary resources), .NET 6 console app with .NET Standard 2.0 library references, and Docker image creation.

**Size**: ~11,600 lines of C# | **Type**: Console App | **Frameworks**: .NET 6.0 (app), .NET Standard 2.0 (libs) | **Runtimes**: Windows/Linux

## Project Structure

3 projects: **App/DotNet.Docker.csproj** (.NET 6.0 console), **FirstDotNetStandard2Library** (.NET Standard 2.0), **SecondDotNetStandard2Library** (.NET Standard 2.0 → depends on FirstDotNetStandard2Library).

- `DotNetStandardConsole.sln` - Main solution (repository root)
- `App/Program.cs` - Entry point
- `FirstDotNetStandard2Library/ResourceFactory.cs` - Loads embedded resources from assemblies via reflection
- `FirstDotNetStandard2Library/resources/` - Embedded images (.bmp) and localized text (.resx: en-US, de, da)
- `DockerfileSln` - Primary Dockerfile

## Build Instructions

**IMPORTANT**: Targets .NET 6.0 (end-of-life). Builds with .NET SDK 8.0+ but needs .NET 6.0 runtime to run. **ALWAYS run from repository root.**

### Commands (run from repo root)

| Command | Time | Notes |
|---------|------|-------|
| `dotnet restore DotNetStandardConsole.sln` | 3-5s | Restores NuGet packages |
| `dotnet build DotNetStandardConsole.sln` | 3-10s | Builds 3 projects |
| `dotnet clean DotNetStandardConsole.sln` | 1-2s | Removes bin/obj |
| `dotnet run --project App/DotNet.Docker.csproj -- 1` | 5-10s | May fail without .NET 6 runtime |

**Expected Warnings (IGNORE - not errors):**
- `NETSDK1138`: .NET 6.0 out of support
- `CA1416`: Platform-specific Bitmap/Image.Save APIs (code is cross-platform via System.Drawing.Common)

## Testing

**No test projects exist.** `dotnet test` runs 0 tests. Validate by building. Don't add tests unless requested.

## Common Issues

1. **`dotnet run` fails** - .NET 6 runtime missing. Build validates code; use Docker to run.
2. **CA1416 warnings** - Expected; System.Drawing.Common is cross-platform in .NET 6.
3. **NETSDK1138 warning** - .NET 6 EOL notice. Ignore unless task is upgrading framework.

## Architecture

**Resource Loading**: Factory pattern - each library's `RegisterResourcesClass` registers with `ResourceFactory` (FirstDotNetStandard2Library) which loads embedded resources via reflection. Supports binary (BMP) and localized text (.resx: da, de, en-US).

**Key Dependencies**: System.Drawing.Common (8.0.6), System.Runtime.InteropServices.RuntimeInformation (4.3.0).

**Configuration**: No .editorconfig, linting config, or CI/CD. Standard .gitignore (ignores bin/, obj/, .vs/).

## Critical Rules

1. **Always build from repo root** with `DotNetStandardConsole.sln`
2. **Ignore NETSDK1138 and CA1416 warnings** - expected, not errors
3. **Don't remove build warnings** unless that's the task
4. **Don't add test projects** unless requested
5. **Build is fast** (3-10s) - always build after changes
6. **No CI/CD** - validate with `dotnet build` only
7. **Resource files (.resx, .bmp) are embedded** - rebuild after changes
8. **SecondDotNetStandard2Library depends on FirstDotNetStandard2Library** - maintain dependency order
9. **App saves to `c:\temp\`** - Windows-specific path is expected
10. **Trust these instructions** - search only if incomplete/incorrect

## Validation

✓ `dotnet restore DotNetStandardConsole.sln` succeeds (3-5s)
✓ `dotnet build DotNetStandardConsole.sln` succeeds with only NETSDK1138/CA1416 warnings (3-10s)
✓ No new errors introduced
