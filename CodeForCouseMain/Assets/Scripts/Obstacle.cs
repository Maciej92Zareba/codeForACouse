using UnityEngine;

public class Obstacle : BaseBoardObject
{
	[SerializeField] private AudioSource boundAudioSource;

	public override void ReactOnGettingAttacked (int damage)
	{
		boundAudioSource.Play();
	}
}
