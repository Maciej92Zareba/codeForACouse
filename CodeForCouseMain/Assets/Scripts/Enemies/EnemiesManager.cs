using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform enemiesParent;
    [SerializeField] GridController gridController;

    public List<Enemy> enemies;

    private void Awake()
    {
        SpawnEnemies(2, 5);
    }

    public void SpawnEnemies(int minNumberOfEnemies, int maxNumberOfEnemies)
    {
        int numberOfEnemiesToSpawn = Random.Range(minNumberOfEnemies, maxNumberOfEnemies);
        
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Quaternion spawnRot = Quaternion.identity;
            Instantiate(enemyToSpawn, CalculateSpawnPosition(), spawnRot, enemiesParent.transform);

            enemies.Add(enemyToSpawn.GetComponent<Enemy>());
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        int maxRow = gridController.GridTargets2dArray.GetLength(0);
        int maxColumn = gridController.GridTargets2dArray.GetLength(1);

        int randomRow = Random.Range(0, maxRow);
        int randomColumn = Random.Range(0, maxColumn);

        int numOfTries = 0;
        GridTarget gridTarget = gridController.GridTargets2dArray[randomRow, randomColumn];
        while (gridTarget.IsObstructed || numOfTries >= 10)
        {
            gridTarget = gridController.GridTargets2dArray[randomRow, randomColumn];
            numOfTries++;
        }
        
        gridTarget.IsObstructed = true;
        
        Vector3 spawnPos = gridTarget.transform.position;
        
        return spawnPos;
    }
}
