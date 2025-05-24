using UnityEngine;

public class BoardCreator : MonoBehaviour
{
	[SerializeField] private EnemiesManager boundEnemiesManager;
	[SerializeField] private GridController boundGridController;
	[SerializeField] Vector2Int enemiesRange = new Vector2Int(2, 5);
	[SerializeField] private Character player;

	private void Awake ()
	{
		PrepareBoard();
	}

	private void PrepareBoard ()
	{
		PreparePlayer();
		PrepareEnemies();
	}

	private void PreparePlayer ()
	{
		GridTarget gridTarget = boundGridController.GridTargets2dArray[0, 0];
		player.SetCharacterDestination(gridTarget.PlacedObjectParent.position, 0, 0);
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
