using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace ChooseYourRecipe
{
    [StaticConstructorOnStartup]
    public static class TexMod
    {
        public static readonly Texture2D GizmoIcon;


        static TexMod()
        {
            GizmoIcon = ContentFinder<Texture2D>.Get("NoRecipes/NoRecipesGizmo");
        }
    }
}
