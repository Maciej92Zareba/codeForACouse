using UnityEngine;

public class BoardCreator : MonoBehaviour
{
	[SerializeField] private EnemiesManager boundEnemiesManager;
	[SerializeField] Vector2Int enemiesRange = new (2, 5);
	[SerializeField] private ObstacleManager boundObstacleManager;
	[SerializeField] Vector2Int obstaclesRange = new (1, 3);
	[SerializeField] private GridController boundGridController;
	[SerializeField] private Player player;

	private void Awake ()
	{
		PrepareBoard();
	}

	private void PrepareBoard ()
	{
		PreparePlayer();
		PrepareEnemies();
		PrepareObstacles();
	}

	private void PreparePlayer ()
	{
		GridTarget gridTarget = boundGridController.GridTargets2dArray[0, 0];
		gridTarget.IsObstructed = true;
		player.transform.position = gridTarget.PlacedObjectParent.position;
	}

	private void PrepareEnemies ()
	{
		int numbersOfEnemies = Random.Range(enemiesRange.x, enemiesRange.y);
		GridTarget[] enemiesGridTargets = new GridTarget[numbersOfEnemies];

		for (int i = 0; i < enemiesGridTargets.Length; i++)
		{
			enemiesGridTargets[i] = FindRandomValidGridTarget();
		}

		boundEnemiesManager.SpawnEnemies(enemiesGridTargets);
	}

	private void PrepareObstacles ()
	{
		int numbersOfObstacles = Random.Range(obstaclesRange.x, obstaclesRange.y);
		GridTarget[] obstaclesGridTargets = new GridTarget[numbersOfObstacles];

		for (int i = 0; i < obstaclesGridTargets.Length; i++)
		{
			obstaclesGridTargets[i] = FindRandomValidGridTarget();
		}

		boundObstacleManager.SpawnObstacles(obstaclesGridTargets);
	}

	private GridTarget FindRandomValidGridTarget ()
	{
		int maxRow = boundGridController.GridTargets2dArray.GetLength(0);
		int maxColumn = boundGridController.GridTargets2dArray.GetLength(1);

		int maxNumberOfTrys = 100;

		for (int i = 0; i < maxNumberOfTrys; i++)
		{
			int randomRow = Random.Range(0, maxRow);
			int randomColumn = Random.Range(0, maxColumn);
			GridTarget gridTarget = boundGridController.GridTargets2dArray[randomRow, randomColumn];

			if (gridTarget.IsObstructed == false)
			{
				gridTarget.IsObstructed = true;
				return gridTarget;
			}
		}

		Debug.LogError("Failed to find valid grid target");
		return null;
	}
}
