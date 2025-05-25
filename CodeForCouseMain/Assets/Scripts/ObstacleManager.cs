using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> obstaclesPrefabs;
    
    public void SpawnObstacles (GridTarget [] gridTargets)
    {
        for (int i = 0; i < gridTargets.Length; i++)
        {
            GameObject enemyToSpawn = obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Count)];
            Quaternion spawnRot = Quaternion.identity;
            GameObject spawnedObject =  Instantiate(enemyToSpawn, gridTargets[i].PlacedObjectParent.position, spawnRot, transform);
            Obstacle obstacle = spawnedObject.GetComponent<Obstacle>();
            obstacle.InitializeBoardObject(gridTargets[i]);
        }
    }
}
