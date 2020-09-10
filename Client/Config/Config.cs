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
        public Vector3 PlayerSpawn = new Vector3( 456.9026f, -991.0045f, 30.6895f );
        public int ZombieDamage = 20;
        public float DistanceZombieTargetToPlayer = 25.0f;
        public bool ZombieCanRun = false;
        public int PercentageVehiclesUndriveable = 90;
        public bool Debug = false;
    }
}
