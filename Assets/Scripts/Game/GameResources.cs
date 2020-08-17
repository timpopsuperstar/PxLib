using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    public ItemGridObject itemGridObjectPrefab;



    //Singleton
    public static GameResources instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("More than one instance of GameResources");
            Destroy(this.gameObject);            
        }
    }
}
