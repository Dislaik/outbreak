using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak.User
{
    public partial class Example
    {
        private void Locales()
        {
            Translate.Locale["EN"] = new Dictionary<string, string>(){
	            {"Example:Test", "This is a Example"}
            };

            Translate.Locale["ES"] = new Dictionary<string, string>(){
                {"Example:Test", "Esto es un ejemplo"}
            };
        }
    }
}