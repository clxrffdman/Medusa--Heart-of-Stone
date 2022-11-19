using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DevDeleteSaveData : MonoBehaviour
{
    

    public void DeleteDevSaveData()
    {
        string path = Application.persistentDataPath + "/saves/save0";
        if (Directory.Exists(path)) { Directory.Delete(path, true); }
        Directory.CreateDirectory(path);
        
    }
}
