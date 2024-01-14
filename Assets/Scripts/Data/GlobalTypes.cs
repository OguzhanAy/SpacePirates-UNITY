using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalTypes 
{
   public enum ShipClass
   {
      Interceptor,
      BattleShip,
      Carrier,
      Explorer,
      Frigate,
      ScienceVessel,
      StealthCruiser,
      Transporter
   };
   public enum ShipLookDirection
   {
      ToPlayer, ToNextWayPoint, ToBottom
   };
}
