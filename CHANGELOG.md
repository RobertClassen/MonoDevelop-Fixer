# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [3.0.2] - 2020-10-14
### Fixed
- Fixed build errors by changing asmdef to only include "Editor" platform.

## [3.0.1] - 2020-10-01
### Changed
- Made buttons slightly wider to fit text better when using Unity versions with new Editor style.

## [3.0.0] - 2020-05-26
### Changed
- Changed Postprocessor to read *.csproj files as XML files and compare their elements to replace defined values instead of using string.Replace based on indices of tags.

## [2.0.0] - 2020-04-07
### Added
- Added LICENSE (MIT)
- Added README
- Added CHANGELOG

### Changed
- Turned project from plugin into package for easier distribution and implementation into other projects.
- Improved layout of menu in Edit/Preferences for better usability.

### Removed
- Removed obsolete files of Unity project which acted as a wrapper for the plugin before.

## [1.0.0] - 2020-03-03
### Added
- Added menu in the "Edit/Preferences..." window to be able to set Property settings more easily.
- Added "Tools/Postprocessors/Update *.csproj files" menu item to update files manually if Unity ever fails to do so automatically (e.g. when there are compilation errors).

### Changed
- Made Postprocessor more generic to be able to use it for different Properties other than "LangVersion" as well if needed.

### Removed
- Removed obsolete Settings class because Properties are now self-contained.

## [0.1.0] - 2019-01-31
### Added
- Implemented basic proof-of-concept version which can be worked with although selecting a Property to overwrite requires doing so in a ScriptableObject which needs to be found in the Assets folder.