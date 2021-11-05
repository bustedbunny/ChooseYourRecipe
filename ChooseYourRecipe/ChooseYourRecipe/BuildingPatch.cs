using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.Sound;

namespace ChooseYourRecipe
{
    [HarmonyPatch(typeof(Building), "GetGizmos")]
    class BuildingPatch
    {
        public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> __result, Building __instance)
        {
            foreach (Gizmo item in __result)
            {
                yield return item;
            }
            if (__instance is Building_WorkTable)
            {

                if (OriginalRecipesUtility.OriginalRecipes().ContainsKey(__instance.def))
                {

                    Command_Action action = new Command_Action
                    {
                        defaultLabel = "EditRecipesLabelChooseYourRecipe".Translate(),
                        defaultDesc = "EditRecipesDescChooseYourRecipe".Translate(),
                        icon = TexMod.GizmoIcon,
                        action = delegate
                        {
                            SoundDefOf.Tick_Tiny.PlayOneShotOnCamera();
                            Find.WindowStack.Add(new EditRecipesWindow(__instance.def));
                        }
                    };
                    yield return action;
                }
            }
        }
    }
}
