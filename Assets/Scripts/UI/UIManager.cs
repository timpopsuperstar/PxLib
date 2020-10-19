using PxMath;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NaughtyAttributes;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIFrame MainCanvas
    {
        get { return _mainCanvas; }
    }

    [SerializeField] UIModule _testModule;

    private UIMainFrame _mainCanvas;
    private List<UIComposition> _loadedCompositions;
    private InputActions _inputActions;

    //Monobehaviour methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("More than one instance of UIManager");
            Destroy(this.gameObject);
        }        
    }
    private void Start()
    {
        InitializeReferences();
        InstantiateCanvas();
    }

    //Public Methods
    public void LoadComposition(UIComposition composition)
    {
        _loadedCompositions.Add(composition);
    }

    //Private Methods
    private void InitializeReferences()
    {
        _inputActions = GameManager.instance.InputActions;
    }
    private void InstantiateCanvas()
    {
        _mainCanvas = UIMainFrame.Instantiate();
    }
}
