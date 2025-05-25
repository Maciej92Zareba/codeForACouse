using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class Character : BaseBoardObject
{
	public event Action OnArrivedAtDestination = delegate {};
	public event Action OnAttackFinished = delegate {};
	
	[SerializeField] protected NavMeshAgent boundAgent;
	[SerializeField] protected Animator boundAnimator;
	[SerializeField] protected string startWalkingCharacterBool;
	[field: SerializeField] public ActionDataSO BoundActionData { get; protected set; }
	[SerializeField] protected AudioClip onHitAudioClip;
	[SerializeField] protected AudioClip onDeathAudioClip;
	[SerializeField] protected AudioSource boundAudioSource;

	private Coroutine cachedDestinationCheckCoroutine;
	private WaitUntil playerAtDestinationCondition;
	private int cachedIsWalkingID;
	private int currentHealth;

	[ShowInInspector, ReadOnly] public GridPosition CharacterGridPosition { get; private set; } = new(0, 0);

	private const float VELOCITY_THRESHOLD = 0.01f;

	public void SetCharacterDestination (Vector3 target, int row, int column)
	{
		CharacterGridPosition.SetGridPosition(row, column);
		SetAgentDestination(target);
	}

	public void Attack (Vector3 attackedPosition)
	{
		transform.LookAt(attackedPosition);
		boundAnimator.SetTrigger(BoundActionData.attackData.AttackAnimationName);
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

	protected void Awake ()
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

	private void OnAttackAnimationFinished ()
	{
		OnAttackFinished();
	}

	protected override void ReactOnGettingAttacked (int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			boundAudioSource.clip = onDeathAudioClip;
			//Die
		}
		else
		{
			boundAudioSource.clip = onHitAudioClip;
		}
		
		boundAudioSource.Play();
	}
}
