using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugResources : MonoBehaviour
{
    public Sprite panelSprite;

    public static DebugResources instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
