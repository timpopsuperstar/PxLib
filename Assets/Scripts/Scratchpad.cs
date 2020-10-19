using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PxMath;
using System.Security.Policy;

public class Scratchpad : MonoBehaviour
{
    [SerializeField] private InputActions _inputActions;

    public static Scratchpad instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {        
        
    }
}
