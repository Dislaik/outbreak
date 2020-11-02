using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.User
{
    public partial class Example : BaseScript
    {
        Translation Translate { get; } = new Translation(Config.Lang);
        public Example()
        {Events(); Translations();

            //Console.Debug(Translate._("Example:Test"));
        }
    }
}
