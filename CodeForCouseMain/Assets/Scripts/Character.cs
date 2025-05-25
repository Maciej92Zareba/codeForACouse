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
	[SerializeField] protected int characterStartHealthPoints = 1;
	[SerializeField] protected string deadAnimation;
	[field: SerializeField] public ActionDataSO BoundActionData { get; protected set; }
	
	[SerializeField] protected AudioClip onHitAudioClip;
	[SerializeField] protected AudioClip onDeathAudioClip;
	[SerializeField] protected AudioSource boundAudioSource;

	private Coroutine cachedDestinationCheckCoroutine;
	private WaitUntil playerAtDestinationCondition;
	private int cachedIsWalkingID;
	private int currentHealth;
	
	private BaseBoardObject cachedAttackedTarget;

	private const float VELOCITY_THRESHOLD = 0.01f;

	public void SetCharacterDestination (GridTarget gridTarget)
	{
		ResetPlacedObject();
		
		PlacedOnGrid = gridTarget;
		PlacedOnGrid.IsObstructed = true;
		SetAgentDestination(PlacedOnGrid.PlacedObjectParent.position);
	}

	public void Attack (GridTarget attackedGridTarget)
	{
		transform.LookAt(attackedGridTarget.PlacedObjectParent.position, Vector3.up);
		cachedAttackedTarget = attackedGridTarget.PlaceObjectOnGrid;
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
		currentHealth = characterStartHealthPoints;
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
		if (cachedAttackedTarget != null)
		{
			cachedAttackedTarget.ReactOnGettingAttacked(BoundActionData.attackData.AttackDamage);
		}
		
		OnAttackFinished();
	}

	public override void ReactOnGettingAttacked (int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0)
		{
			//boundAudioSource.clip = onDeathAudioClip;
			OnDie();
		}
		else
		{
			//boundAudioSource.clip = onHitAudioClip;
		}
		
		//boundAudioSource.Play();
	}
}
