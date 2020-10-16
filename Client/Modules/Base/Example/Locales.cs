using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak.Base
{
    public partial class Example
    {
        public void Translations()
        {
            Translate.Locale["EN"] = new Dictionary<string, string>(){
	            {"TEST1", "1 english"},
	            {"TEST2", "2 english"},
	            {"TEST3", "3 english"}
            };

            Translate.Locale["ES"] = new Dictionary<string, string>(){
                {"TEST1", "1 spanish"},
                {"TEST2", "2 spanish"},
                {"TEST3", "3 spanish"}
            };
        }
    }
}