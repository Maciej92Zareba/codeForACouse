using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesManager : MonoBehaviour
{
	[SerializeField] List<GameObject> enemyPrefabs;
	[SerializeField] Transform enemiesParent;

	private List<Enemy> enemies;
	private int currentEnemyIndex = 0;
	private Action cachedEnemyTurnFinishedAction;

	public void SpawnEnemies (GridTarget[] gridTargets)
	{
		for (int i = 0; i < gridTargets.Length; i++)
		{
			GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
			Quaternion spawnRot = Quaternion.identity;
			Instantiate(enemyToSpawn, gridTargets[i].PlacedObjectParent.position, spawnRot, enemiesParent.transform);
			enemies.Add(enemyToSpawn.GetComponent<Enemy>());
		}
	}

	private void Start ()
	{
		GlobalActions.Instance.OnTurnChange += HandleTurnChanged;
		cachedEnemyTurnFinishedAction = OnEnemyTurnEnded;
	}

	private void OnDestroy ()
	{
		GlobalActions.Instance.OnTurnChange -= HandleTurnChanged;
	}

	private void HandleTurnChanged (bool isPlayerTurn)
	{
		if (isPlayerTurn == false)
		{
			currentEnemyIndex = 0;
			TryPerformEnemyActions();
		}
	}

	private void TryPerformEnemyActions ()
	{
		if (currentEnemyIndex < enemies.Count)
		{
			enemies[currentEnemyIndex].OnFinishedTurn += cachedEnemyTurnFinishedAction;
			enemies[currentEnemyIndex].PerformTurn();
		}
		else
		{
			LastEnemyFinishedActions();
		}
	}

	private void OnEnemyTurnEnded ()
	{
		enemies[currentEnemyIndex].OnFinishedTurn -= cachedEnemyTurnFinishedAction;
		currentEnemyIndex++;
		TryPerformEnemyActions();
	}

	private void LastEnemyFinishedActions ()
	{
		GlobalActions.Instance.NotifyOnEnemiesFinishedAttack();
	}
}
