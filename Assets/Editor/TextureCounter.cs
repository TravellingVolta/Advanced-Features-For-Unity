using UnityEngine;
using System.Collections;
using UnityEditor;

public class TextureCounter : EditorWindow
{

    private int selectedSizeIndex = 0;

    [MenuItem("Window/Texture Counter")]
    public static void Init()
    {
        var window = EditorWindow.GetWindow<TextureCounter>("Texture Counter");

        // Stops this window from being unloaded when a
        // new scene is loaded
        DontDestroyOnLoad(window);
    }

    private void OnGUI()
    {
        var sizes = new string[] { "small", "medium", "large" };

        // Editor GUI goes here
        EditorGUILayout.LabelField("Current selected size is "+ sizes[selectedSizeIndex]);
    }
    }
