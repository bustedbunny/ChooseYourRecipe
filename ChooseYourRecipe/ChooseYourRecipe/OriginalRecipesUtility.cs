using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ChooseYourRecipe
{
    public static class OriginalRecipesUtility
    {
        private static readonly Dictionary<ThingDef, List<RecipeDef>> originalRecipes;
        public static List<RecipeDef> OriginalRecipes(ThingDef def)
        {
            return originalRecipes.TryGetValue(def);
        }

        public static Dictionary<ThingDef, List<RecipeDef>> OriginalRecipes()
        {
            return originalRecipes;
        }

        static OriginalRecipesUtility()
        {
            originalRecipes = new Dictionary<ThingDef, List<RecipeDef>>();
            foreach (ThingDef item in DefDatabase<ThingDef>.AllDefs)
            {
                if (typeof(Building_WorkTable).IsAssignableFrom(item.thingClass))
                {
                    originalRecipes.SetOrAdd(item, new List<RecipeDef>(item.AllRecipes));
                }
            }
        }
    }
}
