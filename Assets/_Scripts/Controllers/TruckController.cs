using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : UnitController
{
   public Vector3 _targetPosition;

   private void Awake()
   {
      
   }

   public void Initialize(Vector3 targetPosition)
   {
      _targetPosition = targetPosition;

      //Figure out which spawn point to start from
      GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
      Vector2 closestSpawn = spawnPoints[0].transform.position;
      float currentDistance = Vector2.Distance(closestSpawn, targetPosition);

      foreach(GameObject go in spawnPoints)
      {
         float distance = Vector2.Distance(go.transform.position, _targetPosition);

         if (distance < currentDistance)
            closestSpawn = go.transform.position;
      }
      transform.position = closestSpawn;
   }

   private void Update()
   {
      //Vector3 translation = _targetPosition - transform.position;
      //Debug.Log(translation.normalized);


   }

}
