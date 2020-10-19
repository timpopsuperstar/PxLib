using System.Collections;
using UnityEngine;
using TMPro;
using PxUISettings;
using NaughtyAttributes;
using System.Runtime.Remoting.Messaging;
using UnityEditorInternal;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteInEditMode]
public class UIText : UIFrame
{
    public enum PxFont
    {
        Softsquare
    }
    //Public properties
    public TextMeshPro Text => _text;

    //Editor fields
    [SerializeField] private PxFont _fontStyle;

    //Private fields    
    private TextMeshPro _text;
    private MeshRenderer _renderer;
    private RectTransform _transform;

    //Monobehaviour Methods
    private void Awake()
    {
        GetComponentReferences();
    }
    private void Update()
    {
        InitializeText();
    }

    //Public methods
    public void SizeBoundsToText()
    {
        Vector2 v = _text.GetRenderedValues().ToInt();
        Bounds = new Bounds(Bounds.center, v);
    }
    public void PrintText(string s, float printSpeed = UISettings.defaultPrintSpeed)
    {
        StartCoroutine(IEPrintText(s, printSpeed));
    }

    //Private methods
    private void GetComponentReferences()
    {
        _text = GetComponent<TextMeshPro>();
        _renderer = GetComponent<MeshRenderer>();
        _transform = GetComponent<RectTransform>();
    }
    private IEnumerator IEPrintText(string s, float printSpeed = UISettings.defaultPrintSpeed)
    {
        yield return new WaitForSeconds(printSpeed);
        string toPrint;
        for (int i = 0; i <= s.Length; i++)
        {
            toPrint = s.Substring(0, i) + "<color=#00000000>" + s.Substring(i);
            Text.text = toPrint;
            yield return new WaitForSeconds(printSpeed);
        }
    }
    private void InitializeText()
    {
        SetSortingOrder();
        SetFrameAlignment();
        switch (_fontStyle)
        {
            case PxFont.Softsquare:
                InitSoftsquareFont(_text);
                break;
        }            
    }

    //Protected methods
    protected void SetSortingOrder() => _renderer.sortingOrder = transform.hierarchyCount;

    //Callbacks
    public override void OnSizeChanged()
    {
        _transform.sizeDelta = Bounds.size;
    }

    //Static Methods
    public static void InitSoftsquareFont(TextMeshPro t)
    {        
        t.fontSize = 9;
        t.margin = new Vector4(7, 7, 7, 7);
        t.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        t.sortingOrder = t.transform.hierarchyCount;
    }

    //Editor Methods
    

}
