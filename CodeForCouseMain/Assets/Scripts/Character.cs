using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
	[SerializeField] private NavMeshAgent boundAgent;
	[SerializeField] private Animator boundAnimator;
	[SerializeField] private string startWalkingCharacterBool;

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
		Debug.Log("Started moving");
		boundAnimator.SetBool(cachedIsWalkingID, true);
		yield return playerAtDestinationCondition;
		boundAnimator.SetBool(cachedIsWalkingID, false);
		Debug.Log("Arrived");
	}

	private bool IsPlayerAtDestination ()
	{
		return boundAgent.hasPath == false && boundAgent.velocity.sqrMagnitude <= VELOCITY_THRESHOLD;
	}
}
