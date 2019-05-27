using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject 
{
	[System.Serializable]
	public struct PrefabData
	{
		public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
	}
    public NavMeshData NavMeshData;

	public PrefabData[] Objects;
}
