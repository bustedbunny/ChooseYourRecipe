using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace ChooseYourRecipe
{

    [HarmonyPatch(typeof(StaticConstructorOnStartupUtility), "CallAll")]
    class StaticConstructorOnStartupUtilityPatch
    {
        public static void Postfix()
        {
            try
            {
                LongEventHandler.QueueLongEvent(RunConstructor, "NoRecipes_LongEvent_RecipeRead", false, null);
            }
            catch (Exception ex)
            {
                Log.Error(string.Concat("Error in static constructor of ", typeof(OriginalRecipesUtility), ": ", ex));
            }
        }

        static void RunConstructor()
        {
            RuntimeHelpers.RunClassConstructor(typeof(OriginalRecipesUtility).TypeHandle);
        }
    }

}
