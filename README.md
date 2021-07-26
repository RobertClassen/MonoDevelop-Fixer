# MonoDevelop Fixer for Unity
This package contains a small script that hooks into the creation of *.csproj files in Unity to overwrite properties by using the [undocumented "OnGeneratedCSProject" method in the "AssetPostprocessor" class](https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/AssetPostprocessor.cs#L154-L167).

## Motivation
Unity frequently rebuilds these 4 different *.csproj files (if they exist):
- Assembly-CSharp-firstpass.csproj
- Assembly-CSharp-Editor-firstpass.csproj
- Assembly-CSharp.csproj
- Assembly-CSharp-Editor.csproj

(For more info look here: https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html)

Doing this resets the "LangVersion" property within them to a value matching the newest version required by any script or *.dll in the project files.

In my case this caused problems with the MonoDevelop IDE which would stop working correctly if the value was set to an unsupported version (no more code-completion, no more syntax-highlighting, no more error checking, no more options like "Go to Reference" (F12), etc.) so I wrote this little tool to fix that by automatically setting the property to a supported value again each time.

## Setup

### Installing from a Git URL (recommended)
See [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html) for how to install packages from a Git URL by using the Unity Package Manager.  
See [here](https://docs.unity3d.com/Manual/upm-git.html) for how to do so manually by editing the "manifest.json" file in <your project folder>/Packages/.

### Installing a local package (alternative)
See [here](https://docs.unity3d.com/Manual/upm-ui-local.html) for how to install packages from a local folder using the Unity Package Manager.  
See [here](https://docs.unity3d.com/Manual/upm-localpath.html) for how to do so manually by editing the "manifest.json" file in <your project folder>/Packages/.

## Usage
After the installation the following steps must be performed once to set up the tool:

1. In the Unity menu bar at the top select "Tools/MonoDevelop Fixer/Open Preferences" to open the [Preferences window](https://docs.unity3d.com/Manual/Preferences.html)
2. Press the bottom "Find" button to update the list of ElementDefinitions which can be applied.
3. Press the "Update all *csproj files" button.
4. Switch to the MonoDevelop window and wait a few seconds for it to reload.

Afterwards the tool will automatically update the relevant *.csproj files after certain events (restarting Unity, recompiling scripts, double-clicking Console entries, etc.).  
If this does not happen automatically at some point (e.g. when there are compilation errors) you can trigger this manually via the "Tools/MonoDevelop Fixer/Update all *csproj files" menu item.

## Note
The current implementation only allows to overwrite or ignore the values of existing XML Elements.  
Renaming keys or adding new ones is currently not possible.