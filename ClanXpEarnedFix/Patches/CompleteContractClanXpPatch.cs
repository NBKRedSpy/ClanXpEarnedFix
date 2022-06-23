using BattleTech;
using BattleTech.UI;
using Harmony;
using HBS;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static BattleTech.Contract;

namespace ClanXpEarnedFix.Patches
{
    [HarmonyPatch(typeof(Contract), nameof(Contract.CompleteContract), new Type[] { typeof(MissionResult), typeof(bool)})]
    public static class CompleteContractClanXpPatch
	{


        private static PropertyInfo ExperienceEarnedInfo { get; set; }

		public static bool Prepare()
		{
			return Core.ModSettings.ChangeXp;
		}


		static CompleteContractClanXpPatch()
        {
			ExperienceEarnedInfo = Harmony.AccessTools.Property(typeof(Contract), nameof(Contract.ExperienceEarned));
		}

		public static void Postfix(Contract __instance)
        {
            try
            {
				if (Core.ModSettings.Debug) Logger.Log("CompleteContract start");

				if (Core.ModSettings.Debug) Logger.LogJson(new
				{
					__instance.State,
					__instance.Override.targetTeam.FactionValue.IsClan,
					__instance.SimGameContract,
				});
				

				//Early exit checks				
				if (!__instance.Override.targetTeam.FactionValue.IsClan) return;
				if (!__instance.SimGameContract) return;

				//Filter out the state as modified by the original function.
				switch(__instance.State)
                {
					case Contract.ContractState.Failed:
					case Contract.ContractState.Retreated:
					case Contract.ContractState.Complete:
						break;
					default:
						return;
				}

				SimGameState simulation = SceneSingletonBehavior<UnityGameInstance>.Instance.Game.Simulation;

				int experienceEarned = 0;


				//Change clan missions to always give 5 skull equivalent XP
				//	---Note: This is the only difference from the base BT function
				experienceEarned = Mathf.FloorToInt(10f *
					simulation.Constants.Pilot.BaseXPGainPerMission);

				Contract.ContractState state2 = __instance.State;
				if (state2 != Contract.ContractState.Retreated)
				{
					if (state2 == Contract.ContractState.Failed)
					{
						experienceEarned = Mathf.FloorToInt((float) experienceEarned * 
							simulation.Constants.Story.XPFailureMod);
					}
				}
				else
				{
					float num2;
					if (__instance.IsGoodFaithEffort)
					{
						num2 = simulation.Constants.Story.XPGoodFaithMod;
					}
					else
					{
						num2 = simulation.Constants.Story.XPBadFaithMod;
					}
					experienceEarned = Mathf.FloorToInt(experienceEarned * num2);
				}

				ExperienceEarnedInfo.SetValue(__instance, experienceEarned);

				if (Core.ModSettings.Debug) Logger.LogJson(new {
					experienceEarned
				});

				if (Core.ModSettings.Debug) Logger.Log("CompleteContract end");

			}
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

    }
}
