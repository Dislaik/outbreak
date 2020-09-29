using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Outbreak
{
    public class Config
    {
        public static dynamic PlayerSpawn = new { X = 456.9026f, Y = -991.0045f, Z = 29.6895f, Heading = 90.0f };
        public static dynamic PlayerDeathRespawn = new { X = 427.5055f, Y = -974.7889f, Z = 29.7099f, Heading = 90.0f };
        public static int ZombieHealth = 250;
        public static int ZombieDamage = 20;
        public static bool ZombieInstantDeathByHeadshot = false;
        public static bool ZombieCanRagdollByShots = false;
        public static float ZombieDistanceTargetToPlayer = 25.0f;
        public static bool ZombieCanRun = false;
        public static int PercentageVehiclesUndriveable = 90;
        public static bool Debug = false;
    }
}
