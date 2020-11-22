using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AnimExporterWindow : EditorWindow
{    
    public const string _rootPath = "Assets/ScriptableObjects/Anims/";
    const float _space = 20f;
    [SerializeField] Sprite[] _sprites;
    string _exportName = "newAnim";
    string _exportPath = "newAnim";
    bool groupEnabled;
    bool _toggle = true;
    
    [MenuItem("Window/Anim Exporter")]
    public static void ShowWindow()
    {
        GetWindow<AnimExporterWindow>("Anim Exporter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);        
        GUILayout.Space(_space);
        //_exportName = EditorGUILayout.TextField("Export Name", _exportName);
        //_exportPath = EditorGUILayout.TextField("Export Path", _exportPath);

        if (GUILayout.Button("Generate Anim"))
        {
            Sprite[] sprites = Selection.GetFiltered<Sprite>(SelectionMode.Unfiltered);
            var orderedSprites = sprites.OrderBy(sprite => sprite.name);
            foreach (Sprite spr in orderedSprites)
            {
                Debug.Log(spr);

            }
            _sprites = orderedSprites.ToArray();
            CreateDirectories(_exportPath);
            CreateAnim();
        }
    }

    private static void CreateDirectories(string exportPath)
    {
        string folderPath = _rootPath + exportPath;
        if (!System.IO.Directory.Exists(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/ScriptableObjects/Anims", exportPath);
        }
    }
    private void CreateAnim()
    {
        int animFrameCount = _sprites.Length;
        List<Sprite> animFrames = new List<Sprite>();

        for (int i = 0; i < animFrameCount; i++)
        {
            Sprite spr = _sprites[i];
            animFrames.Add(spr);
        }
        string animName = _exportName;
        Anim anim = ScriptableObject.CreateInstance<Anim>();
        anim.Frames = animFrames;
        anim.name = animName;
        AssetDatabase.CreateAsset(anim, _rootPath + anim.name + ".asset");
        AssetDatabase.SaveAssets();
    }
}