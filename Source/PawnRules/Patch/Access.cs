﻿using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace PawnRules.Patch;

internal static class Access
{
    private static readonly MethodInfo MethodRimWorldFoodUtilityGetMaxRegionsToScan =
        AccessTools.Method(typeof(FoodUtility), "GetMaxRegionsToScan");

    private static readonly MethodInfo MethodRimWorldFoodUtilityIsFoodSourceOnMapSociallyProper =
        AccessTools.Method(typeof(FoodUtility), "IsFoodSourceOnMapSociallyProper");

    private static readonly MethodInfo MethodRimWorldFoodUtilitySpawnedFoodSearchInnerScan =
        AccessTools.Method(typeof(FoodUtility), "SpawnedFoodSearchInnerScan");

    private static readonly FieldInfo FieldRimWorldFoodUtilityFiltered =
        AccessTools.Field(typeof(FoodUtility), "filtered");

    private static readonly FieldInfo FieldVerseLoadedModManagerRunningMods =
        AccessTools.Field(typeof(LoadedModManager), "runningMods");

    public static int Method_RimWorld_FoodUtility_GetMaxRegionsToScan_Call(Pawn getter, bool forceScanWholeMap)
    {
        return (int)MethodRimWorldFoodUtilityGetMaxRegionsToScan.Invoke(null,
            [getter, forceScanWholeMap]);
    }

    public static bool Method_RimWorld_FoodUtility_IsFoodSourceOnMapSociallyProper_Call(Thing thing, Pawn getter,
        Pawn eater, bool allowSociallyImproper)
    {
        return (bool)MethodRimWorldFoodUtilityIsFoodSourceOnMapSociallyProper.Invoke(null,
            [thing, getter, eater, allowSociallyImproper]);
    }

    public static Thing Method_RimWorld_FoodUtility_SpawnedFoodSearchInnerScan_Call(Pawn eater, IntVec3 root,
        List<Thing> searchSet, PathEndMode peMode, TraverseParms traverseParams, float maxDistance = 9999f,
        Predicate<Thing> validator = null)
    {
        return (Thing)MethodRimWorldFoodUtilitySpawnedFoodSearchInnerScan.Invoke(null,
            [eater, root, searchSet, peMode, traverseParams, maxDistance, validator]);
    }

    public static HashSet<Thing> Field_RimWorld_FoodUtility_Filtered_Get()
    {
        return (HashSet<Thing>)FieldRimWorldFoodUtilityFiltered.GetValue(null);
    }

    public static List<ModContentPack> Field_Verse_LoadedModManager_RunningMods_Get()
    {
        return (List<ModContentPack>)FieldVerseLoadedModManagerRunningMods.GetValue(null);
    }
}