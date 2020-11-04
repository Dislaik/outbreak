using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak.Base
{
    public partial class Building
    {
        public void Locales()
        {
            Translate.Locale["EN"] = new Dictionary<string, string>(){
                {"Tent:Test", "This is a Example"}
            };

            Translate.Locale["ES"] = new Dictionary<string, string>(){
                {"Tent:Test", "Esto es un ejemplo"}
            };
        }
    }
}