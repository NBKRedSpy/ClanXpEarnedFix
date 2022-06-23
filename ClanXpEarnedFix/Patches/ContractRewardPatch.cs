using BattleTech;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanXpEarnedFix.Patches
{


    [HarmonyPatch(typeof(SimGameState), nameof(SimGameState.CalculateContractValueByContractType))]

    public static class ContractRewardPatch
    {

        //public static bool Prepare()
        //{
        //    if (Core.ModSettings.Debug) Logger.Log("ContractRewardPatch prepare");
        //    return Core.ModSettings.ChangePayout;
        //}

        public static void Prefix(ref int diff)
        {
            try
            {
                if (Core.ModSettings.Debug) Logger.Log("ContractRewardPatch start");
                Contract contract = PrepContractGetContract.Contract;

                if (contract == null) return;
                if (!contract.Override.targetTeam.FactionValue.IsClan) return;

                diff = 10;

                if (Core.ModSettings.Debug) Logger.Log("ContractRewardPatch end");

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }
    }
}
