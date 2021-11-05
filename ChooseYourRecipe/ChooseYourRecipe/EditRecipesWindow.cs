using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace ChooseYourRecipe
{
    public class EditRecipesWindow : Window
    {
        const float Width = 700;
        const float Height = 700;
        public override Vector2 InitialSize => new Vector2(Width, Height);
        private Vector2 scrollPosition = Vector2.zero;
        public ThingDef workbench;
        public ListRecipeDef disabledRecipes;

        public EditRecipesWindow(ThingDef def)
        {
            resizeable = false;
            draggable = false;
            doCloseX = true;
            closeOnClickedOutside = true;
            forcePause = true;
            workbench = def;
            disabledRecipes = Current.Game.GetComponent<DisabledRecipesComponent>().RecipeLists(def);
        }

        public override QuickSearchWidget CommonSearchWidget
        {
            get
            {
                return StatsReportUtility.QuickSearchWidget;
            }
        }
        public override void DoWindowContents(Rect inRect)
        {
            List<RecipeDef> list = new List<RecipeDef>();
            if (CommonSearchWidget.filter.Active)
            {
                foreach (RecipeDef item in OriginalRecipesUtility.OriginalRecipes(workbench))
                {
                    if (CommonSearchWidget.filter.Matches(item.label))
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                list = OriginalRecipesUtility.OriginalRecipes(workbench);
            }
            Rect scrollRect = new Rect(0, 0, Width - 2 * Margin, Height - 4 * Margin - 40);
            Rect scrollVertRect = new Rect(0, 0, scrollRect.x, list.Count * 21 + 20);
            Widgets.BeginScrollView(scrollRect, ref scrollPosition, scrollVertRect);
            int scrollYOffset = 10;
            foreach (var recipe in list)
            {
                Rect icon = new Rect(0, scrollYOffset, 20f, 20f);
                GUI.DrawTexture(icon, (recipe?.UIIconThing?.uiIcon ?? TexMod.GizmoIcon));
                Rect label = new Rect(icon.width + 5, scrollYOffset, Text.CalcSize(recipe.label).x, 20f);
                Widgets.Label(label, recipe.label);
                TooltipHandler.TipRegion(label, recipe.description);
                Rect infoCardButton = new Rect(label.x + label.width + 5, scrollYOffset, 20f, 20f);
                Widgets.InfoCardButton(infoCardButton, recipe);
                Rect checkbox = new Rect(scrollRect.width - 3 * Margin, scrollYOffset, 20f, 20f);
                bool cachebool = !disabledRecipes.list.Contains(recipe);
                Widgets.Checkbox(scrollRect.width -3 * Margin,scrollYOffset, ref cachebool,paintable: true);
                if (!cachebool)
                {
                    if (!disabledRecipes.list.Contains(recipe))
                    {
                        disabledRecipes.list.Add(recipe);
                    }
                }
                else
                {
                    if (disabledRecipes.list.Contains(recipe))
                    {
                        disabledRecipes.list.Remove(recipe);
                    }
                }
                scrollYOffset += 21;
            }
            Widgets.EndScrollView();
            Rect resetButton = new Rect(scrollRect.width / 2, scrollRect.height + 21f, 100f, 50f);
            if (Widgets.ButtonText(resetButton, "ResetRecipesChooseYourRecipe".Translate()))
            {
                disabledRecipes.list.Clear();
            }
            Rect resetAllButton = new Rect(scrollRect.width / 2 + 150f, scrollRect.height + 21f, 100f, 50f);
            if (Widgets.ButtonText(resetAllButton, "ResetAllRecipesChooseYourRecipe".Translate()))
            {
                disabledRecipes.list.Clear();
                Current.Game.GetComponent<DisabledRecipesComponent>().ResetAllRecipes();
            }
        }

        public override void PostClose()
        {
            foreach (RecipeDef recipe in OriginalRecipesUtility.OriginalRecipes(workbench))
            {
                if (!workbench.allRecipesCached.Contains(recipe))
                {
                    workbench.allRecipesCached.Add(recipe);
                }
            }
            foreach (RecipeDef item in disabledRecipes.list)
            {
                workbench.allRecipesCached.Remove(item);
            }
            if (disabledRecipes.list.Count > 0)
            {
                Current.Game.GetComponent<DisabledRecipesComponent>().disabledRecipes.SetOrAdd(workbench, disabledRecipes);
                //   SavedSettings.disabledRecipes.SetOrAdd(workbench, disabledRecipes);
            }
            else
            {
                Current.Game.GetComponent<DisabledRecipesComponent>().disabledRecipes.Remove(workbench);
                //    SavedSettings.disabledRecipes.Remove(workbench);
            }
            //  NoRecipesMod.settings.Write();

        }
    }
}
