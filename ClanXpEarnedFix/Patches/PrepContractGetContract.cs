using BattleTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;

namespace ClanXpEarnedFix.Patches
{

    /// <summary>
    /// Gets the contract for the contract value patch.
    /// </summary>
    [HarmonyPatch(typeof(SimGameState), nameof(SimGameState.PrepContract))]
    public static class PrepContractGetContract
    {
        public static bool Prepare()
        {
            return Core.ModSettings.ChangePayout;
        }

        /// <summary>
        /// the contract that was las
        /// </summary>
        public static Contract Contract{ get; set; }

        public static void Prefix(Contract contract)
        {
            Contract = contract;
        }
    }
}
