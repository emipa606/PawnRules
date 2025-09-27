# GitHub Copilot Instructions for Pawn Rules (Continued)

## Mod Overview and Purpose

**Pawn Rules (Continued)** is a mod for RimWorld that allows granular control over individual colonists, animals, guests, and prisoners by assigning custom rules. These rules can be used to customize behavior, dietary preferences, relationship restrictions, and construction permissions to better manage your colony without micromanaging every aspect.

Originally created by Jaxes and now updated to support RimWorld’s newest features, including slaves, the mod also provides options to exclude animals from certain rule applications. The aim is to give you more control over your colony's social, dietary, and work dynamics.

## Key Features and Systems

- **Rule Customization:** Assign individual rules to any pawn in your colony.
- **Flexible Rules:** Disallow pawns from eating certain foods, bonding with specific animals, starting new romances, or constructing items that require a quality level.
- **Ease of Use:** Easily access the rules dialog via the pawn’s HUD.
- **No Game Breaks:** The mod can be added or removed at any time without negatively affecting saved games.
- **Presets and Defaults:** Import and export rule presets between games, and apply default settings to new pawns or newborn animals.
- **Localization Support:** Encourages community-driven translation submods rather than embedding multiple languages within the mod.

## Coding Patterns and Conventions

- **Naming Conventions:** Classes are named using PascalCase, while methods use camelCase.
- **Abstract and Generic Classes:** Used extensively for highly customizable and extendable systems (e.g., `OptionHandle<T>`).
- **Static Helpers:** Static classes like `Lang` and `ScribePlus` handle utility functions and data serialization.

## XML Integration

- XML integration is minimal but supports RimWorld’s default modding structure. XML files are typically used for defining new Defs that interact with the mod’s C# backend.
- Modders can introduce new rule options via XML files that integrate with the existing `OptionHandle`.

## Harmony Patching

Harmony is used for non-invasive patches that modulate game behavior:

- **Food Restrictions:** Patches around food restriction logic to allow specific control over what pawns can or cannot consume (`RimWorld_FoodRestriction_Allows_ByThing` and variants).
- **Behavior Overrides:** Modify pawn behaviors such as romance attempts, construction abilities, and animal interactions.
- **Gizmo Integration:** The rules button is added dynamically to the pawn UI using Harmony patches.

## Suggestions for Copilot

1. **Auto-Generate Stubs:** For new rule options or dialog windows, generate initial stubs with necessary methods like `ExposeData()` for serialization.
2. **XML and C# Cohesion:** Suggest methods or class modifications to ensure seamless XML integration when new def types are introduced.
3. **Harmony Tips:** Provide common patterns for Harmony patches that involve state checks or conditional alterations without creating bugs in the game logic.
4. **Localization Support:** Offer translation key suggestions and their implementations within the code to enable easy translation by third-party localizers.
5. **Property Handling:** Recommend methods for handling dynamic properties in classes such as `OptionHandle` that dictate how rules apply to specific pawns.

This file aims to guide developers in maintaining and expanding the functionality of the "Pawn Rules" mod. Detailed understanding of RimWorld's mod structure and the Harmony library would optimize usage of these suggestions.
