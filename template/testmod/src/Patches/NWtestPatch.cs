// // Token: 0x02000476 RID: 1142
// [global::UnityEngine.Scripting.Preserve]
// public class EntityVehicle : EntityAlive, ILockable
// {
// // Token: 0x0600257A RID: 9594 RVA: 0x000F24A0 File Offset: 0x000F06A0
// 	[PublicizedFrom(EAccessModifier.Private)]
// 	public void ApplyAccumulatedDamage()
// 	{
// 		if (this.damageAccumulator >= 1f)
// 		{
// 			int num = (int)this.damageAccumulator;
// 			this.damageAccumulator -= (float)num;
// 			this.ApplyDamage(num);
// 		}
// 	}
// }

using System;
// using DMT;
// using Harmony;
using HarmonyLib;
using UnityEngine;

// public class ItemActionsChange : IPatcherMod

// {

//    public bool Patch(ModuleDefinition module)
//     {
//         var harmony = new HarmonyLib.Harmony("com.example.patch");
//         harmony.PatchAll();
//         return true;
//     }

namespace testmod.Patches
{

    [HarmonyPatch(typeof(EntityVehicle))]   
    // [HarmonyPatch("ApplyAccumulatedDamage")]
    [HarmonyPatch("ApplyDamage")]
    // public class NWtestPatch : IHarmony
    internal static class NWtestPatch
    {
        // public void Start()
        // {
        //     Debug.Log(" Loading Patch: " + this.GetType().ToString());
        //     // var harmony = HarmonyInstance.Create(GetType().ToString());
        //     // harmony.PatchAll(Assembly.GetExecutingAssembly());
        // }

        static bool Prefix(EntityVehicle __instance)
        {
            // if (__instance.damageAccumulator >= 1f)
            // {
            // 	int num = (int)__instance.damageAccumulator;
            // 	__instance.damageAccumulator -= (float)num;
            // 	// __instance.ApplyDamage(num);
            // }
            return false; // Skip the original method
        }
    }
}