using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ChooseYourRecipe
{
    public class DisabledRecipesComponent : GameComponent
    {
        public Dictionary<ThingDef, ListRecipeDef> disabledRecipes = new Dictionary<ThingDef, ListRecipeDef>();

        public DisabledRecipesComponent(Game game)
        {
        }
        public Dictionary<ThingDef, ListRecipeDef> DisabledRecipes
        {
            get
            {
                if (disabledRecipes != null)
                {
                    return disabledRecipes;
                }
                disabledRecipes = new Dictionary<ThingDef, ListRecipeDef>();
                return disabledRecipes;
            }
        }
        public ListRecipeDef RecipeLists(ThingDef def)
        {
            if (disabledRecipes.ContainsKey(def))
            {
                return disabledRecipes.TryGetValue(def);
            }
            return new ListRecipeDef();
        }

        public override void LoadedGame()
        {
            foreach (ThingDef item in OriginalRecipesUtility.OriginalRecipes().Keys)
            {
                item.allRecipesCached = new List<RecipeDef>(OriginalRecipesUtility.OriginalRecipes().TryGetValue(item));
                if (DisabledRecipes.ContainsKey(item))
                {
                    foreach (RecipeDef recipe in DisabledRecipes.TryGetValue(item).list)
                    {
                        item.allRecipesCached.Remove(recipe);
                    }
                }
            }
        }
        public void ResetAllRecipes()
        {
            foreach (ListRecipeDef item in DisabledRecipes.Values)
            {
                item.list.Clear();
            }
            foreach (ThingDef item in OriginalRecipesUtility.OriginalRecipes().Keys)
            {
                item.allRecipesCached = new List<RecipeDef>(OriginalRecipesUtility.OriginalRecipes().TryGetValue(item));
            }
        }
        public override void StartedNewGame()
        {
            ResetAllRecipes();
        }

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref disabledRecipes, "disabledRecipes", LookMode.Def, LookMode.Deep);
            base.ExposeData();
        }
    }
}
