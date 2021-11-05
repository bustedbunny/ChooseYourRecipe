using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ChooseYourRecipe
{
    public class ListRecipeDef : IExposable
    {
        public List<RecipeDef> list = new List<RecipeDef>();

        public void ExposeData()
        {
            Scribe_Collections.Look(ref list, "list");
        }
    }
}
