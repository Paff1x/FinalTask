using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

[ExecuteInEditMode]
public class LevelEditor : MonoBehaviour 
{
#if UNITY_EDITOR
    public LevelConfig Level;
    public NavMeshData NavMeshData;

	public void Save()
	{
		if (Level == null)
		{
			Level = ScriptableObject.CreateInstance<LevelConfig>();
			AssetDatabase.CreateAsset(Level, "Assets/Resources/Levels/" + SceneManager.GetActiveScene().name + ".asset");
        }

		Level.Objects = new LevelConfig.PrefabData[transform.childCount];
		for (int i = 0; i < transform.childCount; ++i)
		{
			var obj = transform.GetChild(i);
            Level.Objects[i] = new LevelConfig.PrefabData { Prefab = PrefabUtility.GetCorrespondingObjectFromSource(obj.gameObject), Position = obj.transform.position, Rotation = obj.transform.rotation,Scale = obj.transform.localScale };
		}
        Level.NavMeshData = NavMeshData;
        EditorUtility.SetDirty(Level);
    }
#endif
}