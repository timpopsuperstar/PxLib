using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PxJsonData
{
    public virtual void Save() => PxJson.SaveToJson(this);
    //public abstract string FilePath{ get; protected set; }
    public string FilePath => SaveFolder + "/" + FileName;
    public abstract string SaveFolder{ get; protected set; }
    public abstract string FileName { get; protected set; }
    //public string SaveFolder { get; }
    //public string FileName { get; }
}