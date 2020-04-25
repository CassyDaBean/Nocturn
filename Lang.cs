using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;


namespace Nocturn
{
    class Lang
    {
        public static string BossCheck(string Boss)
        {
            switch (Boss)
            {
                case "UseAn":
                return "Use an ";
                case "UseA":
                return "Use a ";
                case "Aquafuros1":
                return "Aquafuros";
                case "AquaInfo1":
                    return "Aquafuros leaves to practice once again. ";
            }
            return "";
        }

    }
}
