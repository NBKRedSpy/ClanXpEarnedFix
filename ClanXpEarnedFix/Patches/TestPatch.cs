using BattleTech;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanXpEarnedFix.Patches
{


    //[HarmonyPatch(typeof(SimGameState), nameof(SimGameState.PrepContract))]
    //public static class TestPatch
    //{

    //    public static void Prefix(FactionValue target)
    //    {
    //        try
    //        {
    //            Logger.LogJson("PrepContract");
    //            Logger.LogJson(new
    //            {
    //                target.IsClan,
    //                target.Name,
    //                target.FriendlyName,
    //                FactionDefName = target.FactionDef?.Name,
    //            });
    //        }
    //        catch (Exception ex)
    //        {

    //            Logger.Log(ex);
    //        }
    //    }
    //}
}
