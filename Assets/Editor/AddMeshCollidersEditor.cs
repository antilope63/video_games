using UnityEngine;
using UnityEditor;

public class AddMeshCollidersEditor : EditorWindow
{
    [MenuItem("Tools/Add Mesh Colliders")]
    public static void ShowWindow()
    {
        GetWindow<AddMeshCollidersEditor>("Add Mesh Colliders");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Add Mesh Colliders to Selected Prefabs"))
        {
            AddMeshColliders();
        }
    }

    private void AddMeshColliders()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (PrefabUtility.IsPartOfPrefabAsset(obj))
            {
                string prefabPath = AssetDatabase.GetAssetPath(obj);
                GameObject prefabRoot = PrefabUtility.LoadPrefabContents(prefabPath);

                AddCollidersToChildren(prefabRoot);

                PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabPath);
                PrefabUtility.UnloadPrefabContents(prefabRoot);
            }
            else
            {
                Debug.LogWarning(obj.name + " is not a prefab.");
            }
        }
    }

    private void AddCollidersToChildren(GameObject root)
    {
        foreach (Transform child in root.transform)
        {
            if (child.GetComponent<MeshFilter>() != null && child.GetComponent<MeshCollider>() == null)
            {
                child.gameObject.AddComponent<MeshCollider>();
            }
            // Recursive call to handle nested children
            AddCollidersToChildren(child.gameObject);
        }
    }
}
