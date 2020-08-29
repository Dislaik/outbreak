using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Outbreak.Core
{
	class Zombie : BaseScript
	{
		Config Config = new Config();
		private string PlayerGroup = "PLAYER";
		private string ZombieGroup = "ZOMBIE";

		public Zombie()
		{
			uint GroupHandle = 2;
			AddRelationshipGroup(ZombieGroup, ref GroupHandle);
			SetRelationshipBetweenGroups(0, (uint)GetHashKey(ZombieGroup), (uint)GetHashKey(PlayerGroup));
			SetRelationshipBetweenGroups(5, (uint)GetHashKey(PlayerGroup), (uint)GetHashKey(ZombieGroup));

			Tick += OnTick;
		}

		private async Task OnTick()
		{
			int PedHandle = -1;
			bool success;
			int Handle = FindFirstPed(ref PedHandle);

			do
			{
				await Delay(10);

				if (IsPedHuman(PedHandle) & !IsPedAPlayer(PedHandle) & !IsPedDeadOrDying(PedHandle, true) & GetRelationshipBetweenPeds(PedHandle, PlayerPedId()) != 0)
				{
					SetPedRelationshipGroupHash(PedHandle, (uint)GetHashKey(ZombieGroup));

					PedAtributtes(PedHandle);

					if (Config.Debug)
					{
						Vector3 PedsCoords = GetEntityCoords(PedHandle, false);
						World.DrawMarker(MarkerType.VerticalCylinder, PedsCoords + new Vector3(0, 0, -1), new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(1f, 1f, 2f), Color.FromArgb(255, 255, 255, 255));
					}

				}
				success = FindNextPed(Handle, ref PedHandle);
			} while (success);

			EndFindPed(Handle);

			await Delay(100);
		}

		private async void PedAtributtes(int PedHandle)
		{
			RequestAnimSet("move_m@drunk@verydrunk");
			while (!HasAnimSetLoaded("move_m@drunk@verydrunk"))
			{
				await Delay(1);
			}
			SetPedMovementClipset(PedHandle, "move_m@drunk@verydrunk", 1.0f);

			ApplyPedDamagePack(PedHandle, "BigHitByVehicle", 0.0f, 9.0f);
		}

	}
}
