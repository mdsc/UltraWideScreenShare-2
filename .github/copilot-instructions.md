# GitHub Copilot Instructions

## Project Overview

**UltraWideScreenShare 2** is a Windows desktop utility that renders a live, magnified view of the screen inside a borderless, always-on-top floating window. It uses the native [Windows Magnification API](https://learn.microsoft.com/en-us/windows/win32/winauto/magapi/entry-magapi-sdk) to capture and display screen content at a configurable zoom level.

Primary use case: mirror a region of the screen (e.g. for presentation or ultra-wide monitor workflows).

---

## Solution Structure

```
UltraWideScreenShare2.sln
├── UltraWideScreenShare.WinForms/   # Main WinForms application (.NET 6)
└── UltraWideScreenShare.Package/    # Windows App Package (WAP / MSIX)
```

### UltraWideScreenShare.WinForms

The runnable application. Targets `net6.0-windows` and is built as self-contained single-file executables for `win-x86` and `win-x64`.

### UltraWideScreenShare.Package

A WAP project that wraps the WinForms app into an MSIX package for Store / side-load distribution. Does not contain application logic.

---

## Key Classes

| Class | File | Responsibility |
|---|---|---|
| `Program` | `Program.cs` | Entry point. Configures per-monitor DPI awareness (`PerMonitorV2`) before launching `MainWindow`. |
| `MainWindow` | `MainWindow.cs` | Primary `Form`. Owns the magnifier panel, custom title bar, transparent click-through logic, and the timer-driven update loop. |
| `Magnifier` | `Magnifier.cs` | Thin wrapper around the Windows Magnifier API. Calls `MagInitialize`, creates the host magnifier `HWND` via `CreateWindowEx`, and exposes `SetSource(RECT)` to update the captured region each frame. |
| `WindowExtension` | `WindowExtension.cs` | Extension methods on `Form` for: transparency/click-through toggling, edge/corner drag-to-resize, title-bar dragging, and DPI helpers. |
| `Settings` | `Settings.cs` | Static class. Persists window bounds and state to `HKCU\Software\UltraWideScreenShare` via the registry. Validates saved bounds against current screen geometry before restoring. |

---

## Architecture & Patterns

### Timer-Driven Magnifier Loop
`MainWindow` uses a `System.Windows.Forms.Timer` with a **2 ms interval** (~30 fps) to call `Magnifier.SetSource()` on every tick, keeping the magnified view in sync with the current screen content.

### Borderless / Transparent Window
- `FormBorderStyle = None`, always on top.
- `TransparencyKey = Color.Magenta` — magenta areas punch through to the desktop.
- When the cursor enters the magnified panel, `WindowExtension.SetTransparency()` makes the window semi-transparent and click-through so the user can interact with windows underneath.

### Extension Methods on Form
Prefer adding window-management behaviour via extension methods in `WindowExtension` rather than putting logic directly in `MainWindow`. This keeps `MainWindow` focused on orchestration.

### Win32 Interop via CsWin32
All Windows API calls (magnifier init, `SetWindowPos`, `GetWindowRect`, etc.) use the **[CsWin32](https://github.com/microsoft/CsWin32)** source generator. Add new API bindings by adding the function name to `NativeMethods.txt` — do **not** write manual `[DllImport]` declarations.

### Registry Settings
`Settings` is a plain static class (no DI). Read settings at startup; write on window move/resize/close. Always validate persisted screen coordinates against `Screen.AllScreens` before applying.

---

## Coding Conventions

- **C# 10 / .NET 6** — use file-scoped namespaces, target-typed `new()`, and `record` types where appropriate.
- **Nullable reference types** are enabled (`<Nullable>enable</Nullable>`). Annotate all public APIs.
- **No MVVM / data-binding** — this is a direct WinForms app; keep UI logic in the form/extension layer.
- **P/Invoke**: always go through CsWin32. Add entries to `NativeMethods.txt` for any new Win32 functions needed.
- **Resources**: embedded resources (icons, images) live in the `Resources/` folder and are referenced via `Properties.Resources`.
- **Minimum window size**: 928 × 427 px. Enforce this in any resize logic.

---

## Build & Packaging

```bash
# Build self-contained single-file executables
dotnet publish UltraWideScreenShare.WinForms -r win-x64 -c Release
dotnet publish UltraWideScreenShare.WinForms -r win-x86 -c Release
```

MSIX packaging is handled by the `.Package` project and GitHub Actions CI (triggered on tagged releases).

---

## Dependencies

| Package | Purpose |
|---|---|
| `Microsoft.Windows.CsWin32` | Source-generated safe P/Invoke wrappers for Win32 APIs |

No other third-party runtime dependencies.
