using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	public event Action OnArrivedAtDestination = delegate {};
	
	[SerializeField] private NavMeshAgent boundAgent;
	[SerializeField] private Animator boundAnimator;
	[SerializeField] private string startWalkingCharacterBool;
	[SerializeField] public CharacterDataSO boundCharacterData;

	private Coroutine cachedDestinationCheckCoroutine;
	private WaitUntil playerAtDestinationCondition;
	private int cachedIsWalkingID;

	[ShowInInspector, ReadOnly] public GridPosition CharacterGridPosition { get; private set; } = new(0, 0);

	private const float VELOCITY_THRESHOLD = 0.01f;

	public void SetCharacterDestination (Vector3 target, int row, int column)
	{
		CharacterGridPosition.SetGridPosition(row, column);
		SetAgentDestination(target);
	}

	[Button]
	private void SetAgentDestination (Vector3 target)
	{
		if (cachedDestinationCheckCoroutine != null)
		{
			StopCoroutine(cachedDestinationCheckCoroutine);
		}

		boundAgent.SetDestination(target);

		cachedDestinationCheckCoroutine = StartCoroutine(PlayerArrivedAtDestination());
	}

	private void Awake ()
	{
		playerAtDestinationCondition = new WaitUntil(IsPlayerAtDestination);
		cachedIsWalkingID = Animator.StringToHash(startWalkingCharacterBool);
	}

	private IEnumerator PlayerArrivedAtDestination ()
	{
		yield return null;
		boundAnimator.SetBool(cachedIsWalkingID, true);
		yield return playerAtDestinationCondition;
		boundAnimator.SetBool(cachedIsWalkingID, false);
		OnArrivedAtDestination();
	}

	private bool IsPlayerAtDestination ()
	{
		return boundAgent.hasPath == false && boundAgent.velocity.sqrMagnitude <= VELOCITY_THRESHOLD;
	}
}
