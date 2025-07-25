# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This RimWorld mod aims to enhance the game's rule and restriction system. It provides custom behaviors for pawns, allowing developers to create rich, customizable rule sets governing various aspects of pawns' lives. The mod is constructed using C# and integrates with the game through the use of Harmony patches, alongside XML configuration files that define in-game content and behaviors.

## Key Features and Systems

- **Option Handles**: Implement different options or rules that can be applied to pawns. These allow dynamic interaction with pawn settings.
- **Pawns and Restrictions**: Custom restriction types and templates provide versatile rules, enabling players to enforce specific conditions on pawns.
- **Custom Dialog Interfaces**: A series of `Dialog_*` classes facilitate interaction with users, providing intuitive UI elements to manage rules and restrictions.
- **Addon Management**: Through classes like `AddonManager`, the mod provides a system to manage additional features or modifications.

## Coding Patterns and Conventions

- **Naming Conventions**: Use PascalCase for class names and method names, camelCase for local variables and method parameters, and ALL_CAPS for constants.
- **Accessibility**: Favor using `internal` for class and method access within the mod, indicating their consumption within the mod's assembly.
- **Class Design**: 
  - Use `abstract` classes for base class functionalities like OptionHandle, promoting inheritance for specific implementations.
  - Employ `static` classes such as `AddonManager` and `Patcher` for singleton-like behavior ensuring a single class instance.

## XML Integration

XML files are integral to this mod, used for defining things like options, rules, and restrictions that attach to pawns. These XML files follow RimWorld's mod structure, ensuring compatibility and ease of installation for players.

- Adhere to RimWorld's schema, ensuring all XML entries are valid and match the expected format.
- Implement classes like `Presetable` and `Binding` that use XML serialization, guided by the `IExposable` and `ILoadReferenceable` interfaces.

## Harmony Patching

Harmony is used extensively to modify and extend the base game functionality. Key areas of patching include:

- **Food Restrictions**: Multiple patches modify how food restrictions affect pawns, leveraging static classes such as `RimWorld_FoodRestriction_Allows_ByThing`.
- **Interaction Workers**: Custom adjustments on interactions between pawns, notably through classes like `RimWorld_InteractionWorker_RomanceAttempt_SuccessChance`.
  
Each patch should adhere to the following conventions:

- Clearly separate patches into individual static classes based on their function.
- Annotate methods using `[HarmonyPatch]` specifying the target method to patch.
- Maintain clarity in patch intent within method documentation or inline comments.

## Suggestions for Copilot

1. **Pattern Recognition**: Copilot can assist by recognizing patterns in harmony patches, suggesting appropriate method signatures and `[HarmonyPatch]` annotations.
2. **UI Code Assistance**: Generate boilerplate code for UI components, especially for complex dialogs that utilize RimWorld's UI framework.
3. **Consistency in Naming**: Ensure suggested names for variables and methods follow the established convention for coherence and readability.
4. **XML Configuration**: Propose structures for new XML files, following established XML templates, and offer suggestions for serialized field names that match their C# counterparts.
5. **Error Handling**: Suggest robust error handling patterns in harmony patches and custom C# methods to ensure stability and debugging ease.

By following these organized instructions, you can effectively utilize GitHub Copilot to enhance development efficiency for RimWorld mods.
