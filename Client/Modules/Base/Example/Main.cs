using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak.Base
{
    public partial class Example : BaseScript
    {
        Translation Translate = new Translation(Config.Lang);
        public Example()
        {
            Events();
            Translations();

            //Debug.WriteLine($"{Translate._("TEST1")}");
        }
    }
}
