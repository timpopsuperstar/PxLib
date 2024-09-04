using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.UIElements;
using UnityEditorInternal;

public class BattlerExporterWindow : EditorWindow
{
    public const string _rootPath = "Assets/ScriptableObjects/Chef/Anims/Battlers";
    [SerializeField] Sprite[] _sprites;
    [SerializeField] string _exportName = "newAnim";

    [MenuItem("Window/Chef/Battle Exporter")]
    public static void ShowWindow()
    {
        GetWindow<BattlerExporterWindow>("Battler Exporter");
    }

    public void OnEnable()
    {
        Selection.selectionChanged += SetSpriteList;
    }
    public void OnDisable()
    {
        Selection.selectionChanged -= SetSpriteList;
    }

    private void SetSpriteList()
    {
        _sprites = Selection.GetFiltered<Sprite>(SelectionMode.Unfiltered);
        _sprites = _sprites.OrderBy(sprite => sprite.name).ToArray();
    }
    private void OnGUI()
    {
        _exportName = EditorGUILayout.TextField("Export Name", _exportName);

        if (GUILayout.Button("Generate Anim"))
        {
            if (_sprites.Length == 8)
            {
                CreateDirectories();
                CreateAnimSet();
            }
            else
            {
                Debug.Log("Select 8 sprites");
            }
        }
    }
    private void CreateDirectories()
    {
        string folderPath = _rootPath + "/" + _exportName;
        if (!System.IO.Directory.Exists(folderPath))
        {
            Debug.Log("doesn't exist");
            AssetDatabase.CreateFolder(_rootPath, _exportName);
        }
    }
    private void CreateAnimSet()
    {
        List<Sprite> idleSprites = new List<Sprite>()
        {
            _sprites[0],
            _sprites[1]
        };
        List<Sprite> damagedSprites = new List<Sprite>()
        {
            _sprites[2],
            _sprites[3]
        };
        List<Sprite> deadSprites = new List<Sprite>()
        {
            _sprites[4]
        };
        List<Sprite> dodgeVerticalSprites = new List<Sprite>()
        {
            _sprites[5]
        };
        List<Sprite> dodgeLeftSprites = new List<Sprite>()
        {
            _sprites[6]
        };
        List<Sprite> dodgeRightSprites = new List<Sprite>()
        {
            _sprites[7]
        };
        string exportPath = _rootPath + "/" + _exportName + "/";
        Anim idleAnim = CreateAnimAsset(exportPath, "Idle", idleSprites);
        idleAnim.Loop = true;
        idleAnim.FrameRate = 4f;
        Anim damagedAnim = CreateAnimAsset(exportPath, "Damaged", damagedSprites);
        Anim deadAnim = CreateAnimAsset(exportPath, "Dead", deadSprites);
        Anim dodgeVertAnim = CreateAnimAsset(exportPath, "DodgeVertical", dodgeVerticalSprites);
        Anim dodgeLeftAnim = CreateAnimAsset(exportPath, "DodgeLeft", dodgeLeftSprites);
        Anim dodgeRightAnim = CreateAnimAsset(exportPath, "DodgeRight", dodgeRightSprites);

        BattleActorDefaultAnimSet animSet = ScriptableObject.CreateInstance("BattleActorDefaultAnimSet") as BattleActorDefaultAnimSet;
        animSet.idle = idleAnim;
        animSet.damaged = damagedAnim;
        animSet.dead = deadAnim;
        animSet.dodgeVertical = dodgeVertAnim;
        animSet.dodgeLeft = dodgeLeftAnim;
        animSet.dodgeRight = dodgeRightAnim;
        AssetDatabase.CreateAsset(animSet, exportPath + _exportName + "DefaultAnimSet.asset");
    }
    private Anim CreateAnimAsset(string exportPath, string animName, List<Sprite> sprites, float framerate = 12f)
    {
        Anim anim = ScriptableObject.CreateInstance<Anim>();
        anim.Frames = sprites;
        anim.FrameRate = framerate;
        anim.name = animName;
        AssetDatabase.CreateAsset(anim, exportPath + _exportName + animName + ".asset");
        AssetDatabase.SaveAssets();
        return anim;
    }
}