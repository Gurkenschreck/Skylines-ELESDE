using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELESDE
{
    public class ELESDEMod : IUserMod
    {
        //Necessary properties
        public string Description
        {
            get { return "A mod for visual highlights."; }
        }

        public string Name
        {
            get { return "ELESDE Mod"; }
        }

        private static bool isDebug = true;
        public static bool IsDebug
        {
            get { return isDebug; }
            set { isDebug = value; }
        }
    }
}
