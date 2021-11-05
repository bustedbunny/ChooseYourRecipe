using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using RimWorld;
using HarmonyLib;
using System.Runtime.CompilerServices;

namespace ChooseYourRecipe
{
    public class ChooseYourRecipe : Mod
    {
    //    public static SavedSettings settings;

        public ChooseYourRecipe(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("ChooseYourRecipe.patch");
            harmony.PatchAll();
            //    LongEventHandler.ExecuteWhenFinished(() => settings = GetSettings<SavedSettings>());
        }
    }
}
