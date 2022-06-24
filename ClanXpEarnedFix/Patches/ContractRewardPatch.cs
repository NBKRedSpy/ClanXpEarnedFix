using BattleTech;
using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClanXpEarnedFix.Patches
{


    [HarmonyPatch(typeof(SimGameState), nameof(SimGameState.CalculateContractValueByContractType), new Type[] {
        typeof(ContractTypeValue),
        typeof(int),
        typeof(float ),
        typeof(float),
        typeof(int)}
        )]

    public static class ContractRewardPatch
    {

        public static bool Prepare()
        {
            Logger.Log("simgame state prepare called");
            return true;
            //if (Core.ModSettings.Debug) Logger.Log("ContractRewardPatch prepare");
            //return Core.ModSettings.ChangePayout;
        }

        public static void Prefix(ref int diff)
        {
            try
            {
                if (Core.ModSettings.Debug) Logger.Log("ContractRewardPatch start");
                Logger.Log("29");
                Contract contract = PrepContractGetContract.Contract;

                Logger.Log("32");
                if (contract == null) return;
                Logger.Log("34");
                if (!contract.Override.targetTeam.FactionValue.IsClan) return;

                Logger.Log("37");
                diff = 10;

                Logger.Log("40");
                if (Core.ModSettings.Debug) Logger.Log("ContractRewardPatch end");

            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }
    }
}
