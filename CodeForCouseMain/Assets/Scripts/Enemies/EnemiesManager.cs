using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform enemiesParent;

    public List<Enemy> enemies;

    public void SpawnEnemies (GridTarget [] gridTargets)
    {
        for (int i = 0; i < gridTargets.Length; i++)
        {
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Quaternion spawnRot = Quaternion.identity;
            Instantiate(enemyToSpawn, gridTargets[i].PlacedObjectParent.position, spawnRot, enemiesParent.transform);
            enemies.Add(enemyToSpawn.GetComponent<Enemy>());
        }
    }
}
