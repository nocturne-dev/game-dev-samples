using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManagement : MonoBehaviour {
    
    public static DataManagement data_management;
    public int highScore;

    void Awake() {
        //if data_management does not exist
        if(data_management == null)
        {
            DontDestroyOnLoad(gameObject);
            data_management = this;
        }
        //removes duplicate data_management
        else if (data_management != this) {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        //Data is saved
        BinaryFormatter BinForm = new BinaryFormatter(); // creates a binary formatter
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat"); // creates file
        gameData data = new gameData(); // creates container for data
        data.highscore = highScore;
        BinForm.Serialize(file, data); //serializes
        file.Close(); //closes file
    }

    public void LoadData()
    {
        //Data is loaded
        if(File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {
            BinaryFormatter BinForm = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);
            gameData data = (gameData)BinForm.Deserialize(file);
            file.Close();
            highScore = data.highscore;
        }
    }
}

[Serializable]
class gameData {
    public int highscore;
}
