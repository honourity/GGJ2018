using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : UnitController
{
   public RadioTowerController _radioTower;
   [SerializeField] float _desiredDistance;

   private float _initialScale;
   private Vector3 _diffVector;

   private void Awake()
   {
      _initialScale = transform.localScale.x;
   }

   public void Initialize(RadioTowerController radioTower)
   {
      _radioTower = radioTower;

      //Figure out which spawn point to start from
      GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
      Vector2 closestSpawn = spawnPoints[0].transform.position;
      float currentDistance = Vector2.Distance(closestSpawn, radioTower.transform.position);

      foreach (GameObject go in spawnPoints)
      {
         float distance = Vector2.Distance(go.transform.position, _radioTower.transform.position);

         if (distance < currentDistance)
            closestSpawn = go.transform.position;
      }
      transform.position = closestSpawn;

      _diffVector = (_radioTower.transform.position - transform.position).normalized;
   }

   private void Update()
   {
      if (Vector2.Distance(transform.position, _radioTower.transform.position) > _desiredDistance)
      {
         if (_diffVector.x <= 0)
            transform.localScale = new Vector3(-_initialScale, _initialScale);
         else
            transform.localScale = new Vector3(_initialScale, _initialScale);

         transform.Translate((_diffVector) * _speed * Time.deltaTime);

      }
      else
      {
         _radioTower.StartCoroutine(_radioTower.Repair());
         Destroy(gameObject);
      }



   }
}
