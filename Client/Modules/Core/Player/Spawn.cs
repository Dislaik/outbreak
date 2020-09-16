using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core.Player
{
    class Spawn : BaseScript
    {
        public Spawn()
        {
            Exports["spawnmanager"].spawnPlayer(SpawnPosition());
        }

        public dynamic SpawnPosition()
        {
            dynamic obj = new ExpandoObject();
            obj.x = Config.PlayerSpawn.X;
            obj.y = Config.PlayerSpawn.Y;
            obj.z = Config.PlayerSpawn.Z;
            obj.heading = Config.PlayerSpawn.Heading;
            obj.model = "mp_m_freemode_01";
            return obj;
        }
    }
}
