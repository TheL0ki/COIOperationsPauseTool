# Operation Pause Tool

A [Captain of Industry](https://www.captain-of-industry.com/) mod that adds a dedicated toolbar cursor tool for pausing **building operation only**. Construction keeps running.

## Disclaimer
As I do not have any C++ or Unity knowledge, this modification is entirely vibe coded with "Cursor". Expect unchecked slop and such things. This mod was created to fill a personal gap for me and me alone. I am providing this mod if anyone else feels the need for such a tool too. Please don't come at me with comments about the quality of the code and use this mod **at your own risk**.

## Why this mod exists

Captain of Industry ships a **Pause** tool that, by default, pauses both construction and operation on selected buildings. The vanilla tool has a secondary mode (“pause more”) that limits the effect to operation only, but it is tied to a modifier inside that same tool.

**Operation Pause Tool** exposes that behavior as its own toolbar button: one click to select it, then left-click or drag to pause operation without stopping construction. That is useful when you want to halt production on a building (or a group of buildings) while trucks, builders, or materials still finish the build.

## How it works

When the mod loads, it registers `OperationPauseToolController` through the game’s dependency injection system. The controller extends the same base class as the vanilla pause tool (`BaseEntityCursorInputController`) and adds a toolbar button next to the stock Pause tool (toolbar order 1061 vs 1060).

On selection, the tool calls `SetPaused(true/false)` on eligible entities instead of issuing `ToggleEnabledGroupCmd`. That is the same operation-only pause path the vanilla “pause more” mode uses, but always active—no modifier to hold.

### What can be paused

- Fully built buildings that support pausing (`CanBePaused`)
- Buildings still under construction (operation pauses; construction does not)
- Partial transport segments (belts, pipes) when **Ctrl** is held during selection

### What is excluded

- Destroyed entities
- Transport and train track pillars
- Buildings in an invalid construction state
- Full transport entities during area selection (use Ctrl for segment selection instead)

Right-click targets only entities that are already paused (`IsPaused`), so you unpause deliberately rather than toggling everything in a selection.

The tool unlocks with the same **Tools** research as the vanilla Pause tool (`IdsCore.Technology.PauseTool`).

## Controls

| Action | Effect |
|--------|--------|
| Left click / drag | Pause operation on selected buildings |
| Right click / drag | Unpause operation on paused buildings |
| Hold **Ctrl** | Include transports (belts, pipes) in the selection |

There is no dedicated keyboard shortcut. Activate the tool from the toolbar (PauseOnly icon, next to the vanilla Pause tool). The vanilla Pause tool still uses **P** (`TogglePauseTool`).

## Installation

### Pre-built release

Copy the mod folder into your mods directory:

```
%APPDATA%\Captain of Industry\Mods\OperationPauseTool\
```

The folder must contain at least `manifest.json` and `OperationPauseTool.dll`.

### Build from source

**Requirements**

- .NET SDK (targets `net48`)
- Captain of Industry installed locally

**Steps**

1. Set `COI_ROOT` to your game install path. The project file defaults to a Steam path; override it when building:

   ```powershell
   dotnet build -c Release /p:COI_ROOT="C:\Path\To\Captain of Industry"
   ```

2. Build the project:

   ```powershell
   dotnet build src/OperationPauseTool/OperationPauseTool.csproj -c Release
   ```

3. With `DeployToModsFolder` enabled (default in the `.csproj`), the build copies output to `%APPDATA%\Captain of Industry\Mods\OperationPauseTool\` and creates a zip alongside it.

Enable the mod in the game’s mod manager. It can be added to or removed from existing saves (`can_add_to_saved_game` / `can_remove_from_saved_game` in the manifest).

## Compatibility

| | |
|---|---|
| Minimum game version | 0.8.3 |
| Verified through | 0.8.4b |
| Mod version | 0.1.0 |
| DLC required | None |

## Project layout

```
src/OperationPauseTool/
  OperationPauseToolMod.cs          # Mod entry point; registers DI dependencies
  Tools/
    OperationPauseToolController.cs # Toolbar tool implementation
  manifest.json                     # Mod metadata for the game loader
tools/decomp/                       # Decompiled game assemblies (reference only)
```

## License

GNU GPLv3
