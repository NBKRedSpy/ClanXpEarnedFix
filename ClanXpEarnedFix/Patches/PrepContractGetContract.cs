using BattleTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using Newtonsoft.Json;
using BattleTech.Framework;

namespace ClanXpEarnedFix.Patches
{

    /// <summary>
    /// Gets the contract for the contract value patch.
    /// </summary>
    [HarmonyPatch(typeof(SimGameState), nameof(SimGameState.PrepContract))]
    public static class PrepContractGetContract
    {
        //public static bool Prepare()
        //{

        //    if (Core.ModSettings.Debug) Logger.Log("PrepContractGetContract prepare");
        //    return Core.ModSettings.ChangePayout;
        //}

        /// <summary>
        /// the contract that was las
        /// </summary>
        public static Contract Contract{ get; set; }
        public static FactionValue TargetFaction { get; set;  }



        public static void Postfix(SimGameState __instance, Contract contract, FactionValue target, int presetSeed)
        {

            try
            {
                if (Core.ModSettings.Debug) Logger.Log("PrepContractGetContract start");


                if (target.IsClan == false) return;



                //Recompute the reward to a 5 skull difficulty for clan missions.
                //If the override is set, still use that.
                int num2;
                if (contract.Override.contractRewardOverride >= 0)
                {
                    Logger.Log($"contract.Override.contractRewardOverride {contract.Override.contractRewardOverride} ");
                    num2 = contract.Override.contractRewardOverride;
                }
                else
                {
                    num2 = __instance.CalculateContractValueByContractType(contract.ContractTypeValue, 10, (float)__instance.Constants.Finances.ContractPricePerDifficulty, __instance.Constants.Finances.ContractPriceVariance, presetSeed);
                }
                num2 = SimGameState.RoundTo((float)num2, 1000);
                contract.SetInitialReward(num2);

                if (Core.ModSettings.Debug) Logger.Log("PrepContractGetContract end");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }
    }
}
