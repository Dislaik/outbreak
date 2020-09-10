using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
    class Players : BaseScript
    {
        Config Config = new Config();
        public Players()
        {
            Exports["spawnmanager"].spawnPlayer(SpawnPosition());
            Exports["spawnmanager"].setAutoSpawn(false);
        }

        public dynamic SpawnPosition()
        {
            dynamic obj = new ExpandoObject();
            obj.x = Config.PlayerSpawn.X;
            obj.y = Config.PlayerSpawn.Y;
            obj.z = Config.PlayerSpawn.Z;
            obj.heading = 0f;
            obj.model = "s_m_y_marine_01";
            return obj;
        }
    }
}
