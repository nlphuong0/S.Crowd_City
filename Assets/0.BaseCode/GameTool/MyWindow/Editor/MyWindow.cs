using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MyWindow : EditorWindow
{
    [MenuItem("SNAPE/Open Scenes Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ScenesWindow), false, "Scenes Window");
    }
}