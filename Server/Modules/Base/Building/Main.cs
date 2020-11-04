using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using MySqlConnector;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Base
{
    public partial class Building : BaseScript
    {
        public Building()
        {
            Events();
            Database.Initialize();
            Database.ExecuteMigrationQuery("Base", "Building", "buildings");

        }
    }
}
