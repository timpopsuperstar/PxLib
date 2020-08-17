using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class UIMessageBox : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    [ResizableTextArea] [SerializeField] private string debugMessageString;

    private void Awake()
    {
        StartCoroutine(PrintText(debugMessageString));
    }

    public IEnumerator PrintText(string s)
    {
        string toPrint;
        for(int i = 0; i < s.Length; i++)
        {
            toPrint = s.Substring(0, i);
            text.text = toPrint;
            yield return new WaitForSeconds(.02f);
        }        
    }
}
