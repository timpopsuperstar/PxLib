using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class Preloader : MonoBehaviour
{
    [Scene]public string sceneToLoad;

    private void Awake()
    {
       
    }

    private void Start()
    {
        LoadScene(sceneToLoad);
    }

    void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
