using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputActions))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public InputActions InputActions => _inputActions? _inputActions : (_inputActions = GetComponent<InputActions>());
    InputActions _inputActions;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
