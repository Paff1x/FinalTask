using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions 
{

	public static void DestroyOrMoveToPool(this GameObject gameObject)
	{
		var poolObject = gameObject.GetComponent<PoolObject>();
		if(poolObject == null || poolObject.Pool == null)
		{
            GameObject.Destroy(gameObject);

		}
		else
		{
			poolObject.Pool.Despawn(poolObject);
		}
	}
}
