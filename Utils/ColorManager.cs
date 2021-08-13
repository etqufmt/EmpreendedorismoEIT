using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Utils
{
    public static class ColorManager
    {
        public static string CorHTML(int cor)
        {
            try
            {
                return String.Format("{0:X6}", cor).Insert(0, "#");
            }
            catch
            {
                return null;
            }
        }
    }
}
