using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    //LOADS LEVEL BASED ON WHAT BUTTON CLICKED
    FileStream stream;
    public void LevelOne()
    {
        Application.LoadLevel(1);

    }
    public void Level2()
    {
        Application.LoadLevel(2);

    }

    //LETS THE PLAYER CLEAR THEIR HIGH SCORES
    public void wipeScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        stream = File.Create(Application.persistentDataPath + "/playerData.dat");
        PlayerData playerData = new PlayerData();             
        playerData.Level1score = 0;        
        playerData.Level2score = 0;
        Debug.Log(playerData.Level1score);
        Debug.Log(playerData.Level2score);
        bf.Serialize(stream, playerData);
        stream.Close();

    }
    [System.Serializable]
    public struct PlayerData
    {
        public int Level1score;
        public int Level2score;

    }

}
