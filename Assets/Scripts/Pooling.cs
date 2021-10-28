using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Add this script to any GameObject that needs to instantiate several objects regularly
/// </summary>
public class Pooling : MonoBehaviour
{
	/**
	* Object to be copied
	*/
	[SerializeField]
	private GameObject _pooledObject = null;

	/**
	* Initial size of the pool
	*/
	[SerializeField]
	private int _initialPoolSize = 10;

	/**
	* Maximum size of the pool
	*/
	[SerializeField]
	private int _maxPoolSize = 50;

	/**
	* Can the pool grow if no inactive gameObjects is found?
	*/
	[SerializeField]
	private bool _canGrow = true;
	/**
	* List of gameObjects
	*/
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

		for (int i = 0; i < _initialPoolSize; i++) {
			InstantiateGameObject ();
		}
	}

	public GameObject GetPooledObject()
	{
		// Check for any disabled gameObject and returns it
		for (int i = 0; i < _pool.Count; i++) {
			if (!_pool [i].activeInHierarchy)
				return _pool [i];
		}

		// Otherwise, if there is no object available and the pool can grow, create a new one and add to the pool
		if (_canGrow && _pool.Count < _maxPoolSize) 
		{
			return InstantiateGameObject();
		}

		// In case the pool can no longer grow and there is no available gameObject, return null
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
