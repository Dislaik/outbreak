using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    public partial class Admin : BaseScript
    {
        Translation Translate { get; } = new Translation(Config.Lang);
        public Admin()
        {

        }
    }
}