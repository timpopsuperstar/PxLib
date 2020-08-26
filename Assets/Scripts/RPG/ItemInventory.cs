using PxMath;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ItemInventory 
{
    Transform container;
    Grid<ItemGridObject> grid;

    public ItemInventory(List<Item> items)
    {
        this.grid = new Grid<ItemGridObject>(new Dimensions(8,8),ItemGridObject.cellSize, Vector2.zero) ;
        InstantiateGridObjects(items);
    }

    public void InstantiateGridObjects(List<Item> items)
    {
        container = Scratchpad.InstantiateEmpty("Inventory");
        //container.SetParent(UICanvas.instance.transform);
        var itemsToAdd = new List<Item>(items);
        for (int y = 0; y < grid.Dimensions.height; y++)
        {
            for (int x = 0; x < grid.Dimensions.width; x++)
            {
                var obj = Scratchpad.InstantiateGridObject(GameResources.instance.itemGridObjectPrefab);
                grid.SetGridObject(x, y, obj);
                obj.transform.position = grid.OriginPosition + new Vector2(x*grid.CellSize.width, -y*grid.CellSize.height);
                obj.transform.SetParent(container);
                if(itemsToAdd.Count > 0)
                {
                    obj.Item = itemsToAdd[0];
                    itemsToAdd.Remove(obj.Item);
                }
            }
        }
    }
}
