using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Base
{
    public partial class Building : BaseScript
    {
        Translation Translate { get; } = new Translation(Config.Lang);
        public Building()
        {
            Events();
            Locales();

        }
    }
}
