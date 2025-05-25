using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component
{
	public static T Instance {
		get { return instance; }
		protected set { instance = value; }
	}

	private static T instance;
	
	protected abstract void Initialize ();

	private void Awake ()
	{
		if (Instance == null)
		{
			Instance = this as T;
			Initialize();
		}
		else
		{
			Debug.LogError($"SingletonMonoBehaviour<T> was duplicated. Object name {gameObject.name} was removed");
			Destroy(gameObject);
		}
	}

	protected virtual void OnDestroy ()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}
}
