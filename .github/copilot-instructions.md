# Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

### Mod Name: Pawn Rules (Continued)
**Description**: An update of Jaxes' mod, this project adds advanced management features for pawns, supported new elements like slaves and animals. The mod allows users to assign customized rules to colonists, animals, guests, and prisoners to enhance gameplay and maintain control over various interactions and restrictions within the game.

**Purpose**: The mod aims to improve player control over pawns by enabling the assignment of specific behavioral rules. These rules prevent undesired actions such as feeding lavish meals to prisoners, bonding with livestock, or allowing certain colonists to enter new romantic relationships.

## Key Features and Systems

- **Customizable Rules**: Assign rules to pawns covering diet, animal bonding, romance, and construction activities.
- **Rule Management**: The ability to disable or hide rules from the GUI, import/export rule presets, ensure new born animals inherit rules, and establish default rules for new pawns.
- **Seamless Interface**: Integration with RimHUD to replace the food restriction button for easy rule management.

## Coding Patterns and Conventions

- **Class Design**: Follow inheritance where necessary, as seen in `OptionHandle<T>`, which extends `OptionHandle`.
- **Access Modifiers**: Use `internal` access for classes and methods not intended for public API exposure (e.g., `AddonManager`, `Binding`).
- **Method Naming**: Employ descriptive method names that reveal intent, such as `ExposeData`, `IsUsedBy`, and `InitVoids`.
- **Convention**: Use PascalCase for class and method names to maintain consistency with C# conventions.

## XML Integration

- **Data Management**: Ensure that XML data for rules and configurations is correctly managed through interfaces like `IExposable`.
- **Persistence**: Utilize the `ExposeData` method in classes such as `Binding` and `Presetable` for saving/loading data states.

## Harmony Patching

- **Patch Strategy**: Use Harmony to patch methods in RimWorld's base game classes to extend or alter functionality.
- Implement the concept of lightweight patching by utilizing specific patches like `RimWorld_FoodUtility_WillEat_ByThing`.
- **Organization**: Keep patches within dedicated static classes and follow the naming structure such as `RimWorld_<ClassName>_<MethodName>` for clarity.

## Suggestions for Copilot

- **Class Templates**: Provide templates and suggestions for commonly used classes such as rule management and dialog windows.
- **Harmony Setup**: Automate the generation of Harmony patches by providing structure and boilerplate code.
- **More Suggestive Context**: Recognize coding patterns to offer completions for method overrides, e.g., `ExposeData` for handling data serialization.
- **GUI Elements**: Suggest GUI components, ensuring they follow the game's user interface guidelines.
- **Translation Support**: Encourage and assist in the setup for localization by suggesting interfaces for language management.

## Final Notes

This mod is designed to not disrupt existing games, ensuring a smooth experience when adding or removing it. Contributors through translations are encouraged to manage language packs as separate submods. Technical support prioritizes Discord channels for real-time issue tracking and resolution.

For further documentation, refer to the project's wiki on GitHub, where detailed explanations and development guidelines are available for extension and contribution.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.
