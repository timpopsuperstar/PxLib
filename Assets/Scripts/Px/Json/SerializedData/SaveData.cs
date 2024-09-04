using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SaveSlots { Debug, Slot1, Slot2, Slot3};

[System.Serializable]
public class SaveData : PxJsonData
{
    //public PlayerInventoryData playerInventoryData;
    public PxEventFlags eventFlags;
    public SaveSlots SaveSlot { get; protected set; }
    public override string SaveFolder { get; protected set; }
    public override string FileName { get; protected set; }

    //public new static string SaveFolder => "Player";
    //public new static string FileName => "SaveData";
    //public override string filePath => FilePath;
    //public override string saveFolder => SaveFolder;
    //public override string fileName => FileName;

    public SaveData(SaveSlots saveSlot)
    {
        SaveSlot = saveSlot;
        SaveFolder = saveSlot.ToString(); 
        FileName = saveSlot.ToString() + "SaveData";
    }
}
