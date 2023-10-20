#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ScenesWindow : EditorWindow
{
    private void OnGUI()
    {
        var scenes = EditorBuildSettings.scenes;
        for (int i = 0; i < scenes.Length; i++)
        {
            string path = scenes[i].path;
            string sceneName = Path.GetFileNameWithoutExtension(path);
            if (GUILayout.Button(sceneName))
            {
                OpenScene(i);
            }
        }
    }

    private void OpenScene(int buildIndex)
    {
        var scenes = EditorBuildSettings.scenes;
        if (buildIndex < scenes.Length)
        {
            string path = scenes[buildIndex].path;
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(path);
        }
    }
}
#endif