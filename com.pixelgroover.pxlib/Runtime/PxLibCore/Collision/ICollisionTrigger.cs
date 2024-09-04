using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionTrigger 
{
    void OnEnter(GameObject obj);
    void OnCollision(GameObject obj);
    void OnExit(GameObject obj);
    void OnMove(GameObject obj);
}
