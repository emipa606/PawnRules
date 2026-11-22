# GitHub Copilot Instructions for Pawn Rules (Continued)

## Mod Overview and Purpose

Pawn Rules (Continued) is a RimWorld mod that allows custom rules to be assigned individually to your colonists, animals, guests, and prisoners. The mod is a continuation of Jaxe's original Pawn Rules mod and has been updated to include support for Slaves and an option to not use the rules for animals. The mod also allows default rules to be set for new pawns, including newborn and hatched animals.

## Key Features and Systems

- Custom rules that can be applied individually to pawns, including policy selection on the Animals tab
- Policy names can include spaces and special characters
- Default rules that can be set for new pawns
- Import and export of rules presets and defaults between games
- Compatibility with other mods
- Easy creation of new rule options by other modders

## Coding Patterns and Conventions

- The mod follows the standard RimWorld coding conventions.
- XML files are used for defining game objects and their properties.
- Harmony patching is used to modify the game's existing code.
- The mod uses a modular architecture, with separate files for different features and systems.
- C# files are used for implementing the mod's logic.

## XML Integration

The mod uses XML files to define game objects and their properties. The XML files are located in the "Defs" folder and include definitions for WorldObjectDefs and other game objects.

## Harmony Patching

Harmony patching is used to modify the game's existing code. The mod uses Harmony to patch methods and add custom behavior.

## Suggestions for Copilot

- When working on new rule options, consider using the existing code as a reference for implementing the logic and UI.
- When creating new XML definitions, refer to the existing XML files for examples of how to define game objects and their properties.
- When using Harmony patching, make sure to test the patches thoroughly to ensure they work as expected.
- When contributing to the mod, make sure to follow the coding conventions and patterns used in the existing code.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

