using System;
using UnityEngine;

public class PlayerController : UnitController
{
   public override void TakeDamage(float damage)
   {
      base.TakeDamage(damage);
      EventManager.FireEvent("PlayerTakeDamage");
   }

   public void Jump()
   {
      throw new NotImplementedException();
   }

   public void Attack()
   {
      throw new NotImplementedException();
   }

   public void Ultimate()
   {
      throw new NotImplementedException();
   }
}
