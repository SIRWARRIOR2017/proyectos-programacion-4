using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public static class FindMissingScriptsEditor
{
    [MenuItem("Tools/Find Missing Scripts in Scene and Prefabs")]
    public static void FindMissingScripts()
    {
        int totalMissing = 0;
        Debug.Log("FindMissingScripts: buscando en escena activa...");

        var scene = EditorSceneManager.GetActiveScene();
        var roots = scene.GetRootGameObjects();
        foreach (var root in roots)
        {
            totalMissing += FindInGameObject(root);
        }

        Debug.Log("FindMissingScripts: buscando en prefabs del proyecto...");
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;
            totalMissing += FindInPrefab(prefab, path);
        }

        Debug.Log($"FindMissingScripts: búsqueda completada. Total missing components encontrados: {totalMissing}");
        if (totalMissing == 0) Debug.Log("No se encontraron componentes missing.");
    }

    private static int FindInPrefab(GameObject prefab, string path)
    {
        int found = 0;
        var gos = prefab.GetComponentsInChildren<Transform>(true);
        foreach (var t in gos)
        {
            var go = t.gameObject;
            var components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    found++;
                    Debug.Log($"Missing script in Prefab '{path}' -> GameObject '{GetGameObjectPath(go)}' (index {i})");
                }
            }
        }
        return found;
    }

    private static int FindInGameObject(GameObject go)
    {
        int found = 0;
        var transforms = go.GetComponentsInChildren<Transform>(true);
        foreach (var t in transforms)
        {
            var g = t.gameObject;
            var components = g.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    found++;
                    Debug.Log($"Missing script in Scene -> GameObject '{GetGameObjectPath(g)}' (index {i})");
                }
            }
        }
        return found;
    }

    private static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        Transform current = go.transform.parent;
        while (current != null)
        {
            path = current.name + "/" + path;
            current = current.parent;
        }
        return path;
    }
}
