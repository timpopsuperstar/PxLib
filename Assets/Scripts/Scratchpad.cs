using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PxMath;
using System.Security.Policy;

public class Scratchpad : MonoBehaviour
{

    [SerializeField] private UIScene _questionnaireScene;
    [SerializeField] public InputActions _inputActions;

    private UIScene _questionnaireSceneInstance;


    public static Scratchpad instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _questionnaireSceneInstance = Instantiate(_questionnaireScene);
        _questionnaireSceneInstance.Load(_inputActions);
        var a = new Vector2(0, 0);
        var b = new Vector2(10, 10);
        var c = new Vector2(30, 30);
        var d = new Vector2(60, 60);

    }
}
