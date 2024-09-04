using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputActions InputActions
    {
        get => _inputActions;
    }
    public static GameManager instance;

    [SerializeField] private InputActions _inputActions;
    [SerializeField] private UIManager _uiManager;

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

    private void InitUI()
    {

    }
}
