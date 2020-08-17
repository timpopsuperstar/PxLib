using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PxMath;
using System.Security.Policy;

public class Scratchpad : MonoBehaviour
{
    [SerializeField] public InputActions inputActions;
    Grid<ItemGridObject> grid;
    public List<Item> items;
    public Sprite panelSprite;
    public Sprite pinkSquare;

    public static Scratchpad instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {        
        
    }

    public ItemInventory SpawnInventory()
    {
        return new ItemInventory(items);
    }

    public static ItemGridObject InstantiateGridObject(ItemGridObject obj)
    {
        return Instantiate(obj);
    }

    public static Transform InstantiateEmpty(string name)
    {
        return new GameObject(name).transform;
    }


}
