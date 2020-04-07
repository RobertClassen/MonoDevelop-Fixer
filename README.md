# CSProject Postprocessor for Unity
This package contains a small script that hooks into the creation of *.csproj files in Unity to overwrite properties by using the [undocumented "OnGeneratedCSProject" method in the "AssetPostprocessor" class](https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/AssetPostprocessor.cs#L154-L167).

## Motivation
Unity frequently rebuilds these 4 different *.csproj files (if they exist):
- Assembly-CSharp-firstpass.csproj
- Assembly-CSharp-Editor-firstpass.csproj
- Assembly-CSharp.csproj
- Assembly-CSharp-Editor.csproj

(For more info look here: https://docs.unity3d.com/Manual/ScriptCompileOrderFolders.html)

Doing this resets the "LangVersion" property within them to a value matching the newest version required by any script or *.dll in the project files.

In my case this caused problems with the MonoDevelop IDE which would stop working correctly if the value was set to an unsupported version (no more code-completion, no more code-highlighting, no more error checking, no more options like "Go to Reference" (F12), etc.) so I wrote this little package to fix that by automatically setting the property to a supported value again each time.

## Setup

### Installing from a Git URL (recommended)
See [here](https://docs.unity3d.com/Manual/upm-ui-giturl.html) for how to install packages from a Git URL by using the Unity Package Manager.  
See [here](https://docs.unity3d.com/Manual/upm-git.html) for how to do so manually by editing the "manifest.json" file in <your project folder>/Packages/.

### Installing a local package (alternative)
See [here](https://docs.unity3d.com/Manual/upm-ui-local.html) for how to install packages from a local folder using the Unity Package Manager.  
See [here](https://docs.unity3d.com/Manual/upm-localpath.html) for how to do so manually by editing the "manifest.json" file in <your project folder>/Packages/.

## Usage
1. In the Unity menu bar at the top select "Edit/Preferences..." to open the [Preferences window](https://docs.unity3d.com/Manual/Preferences.html)
2. Select the "CSProject" menu on the left side
3. On the right side make sure the "LangVersion" property is set to "Overwrite".
4. In the dropdown next to it you can select the desired value to overwrite existing values with.

Unity will automatically update the relevant files after certain events (recompiling scripts, restarting Unity, etc.).  
If Unity does not do this automatically at some point (e.g. when there are compilation errors) you can trigger this manually via the "Tools/Postprocessors/Update *csproj files" menu item.

## Note
The current implementation only allows to overwrite or ignore the values of existing properties.  
Renaming property keys or adding new ones is currently not possible.