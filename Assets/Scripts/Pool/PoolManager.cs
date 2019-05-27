using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PoolManager : MonoBehaviour
{
	private Dictionary<GameObject, Pool> _pools = new Dictionary<GameObject, Pool>();

	public GameObject Spawn(GameObject prefab)
	{
		return Spawn(prefab, Vector3.zero, Quaternion.identity);
	}

	public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		Pool pool;

		if (_pools.TryGetValue(prefab, out pool))
		{
			var poolObject = pool.Get();
			poolObject.transform.position = position;
			poolObject.transform.rotation = rotation;
			poolObject.gameObject.SetActive(true);
			return poolObject.gameObject;
		}

		return GameObject.Instantiate(prefab, position, rotation);
	}

	public void Prespawn(GameObject prefab, int count)
	{
		Pool pool;
		if(!_pools.TryGetValue(prefab, out pool))
		{
			pool = new Pool(prefab, transform);
			_pools.Add(prefab, pool);
		}
		count = 0;
		for(int i = 0; i < count; ++i)
			pool.Add();
	}
}
