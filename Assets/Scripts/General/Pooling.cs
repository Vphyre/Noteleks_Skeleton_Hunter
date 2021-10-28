using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Add this script to any GameObject that needs to instantiate several objects regularly
/// </summary>
public class Pooling : MonoBehaviour
{
	[SerializeField] private GameObject _pooledObject = null;
	[SerializeField] private int _initialPoolSize = 10;
	[SerializeField] private int _maxPoolSize = 50;
	[SerializeField] private bool _canGrow = true;
	private List<GameObject> _pool;

	public List<GameObject> pool
	{ 
		get { return this.pool; }
	}
	public GameObject pooledObject
    {
		get { return this._pooledObject; }
	}

	void Start()
	{
		//Initialing the pool
		_pool = new List<GameObject> ();

		for (int i = 0; i < _initialPoolSize; i++)
		{
			InstantiateGameObject ();
		}
	}
	/// <summary>
	/// Returns a disabled gameObject in the pool and create a new gameObject if is necessary.
	/// </summary>
	/// <returns></returns>
	public GameObject GetPooledObject()
	{
		for (int i = 0; i < _pool.Count; i++)
		{
			if (!_pool [i].activeInHierarchy)
				return _pool [i];
		}
		if (_canGrow && _pool.Count < _maxPoolSize) 
		{
			return InstantiateGameObject();
		}
		return null;
	}
	private GameObject InstantiateGameObject()
	{
		GameObject instance = (GameObject)Instantiate (_pooledObject, transform.position, Quaternion.identity, transform);

		instance.SetActive (false);
		_pool.Add (instance);
		return instance;
	}
}
