using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class UIHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject block;
    public GameObject Play;
    public GameObject Pause;       
    public GameObject MainMenu;
    public GameObject Death1;
    public GameObject Death2;
    public GameObject Complete;
    public int NextLevelNum;
    public Text ScoreText;
    public Text HiScoreText;
    public int score;
    public int highScore;
    public int level;

    
    
  

   


    void Start()
    {
        //RESETS BASIC SCORE
        //FINDS LEVEL NUMBER AND SETS THE LEVEL VARIABLE
        //CALLS LOAD HIGH SCORE
        score = 0;
        level = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(level);
        LoadHighScore();
       
    }

   
    void Update()
    {
        //KEEPS SCORE AND HIGH SCORE UPDATED 
        player = GameObject.Find("slimey");
        ScoreText.text = score.ToString();
        HiScoreText.text = highScore.ToString();
        if (Input.GetKey(KeyCode.P))
        {
            LoadHighScore();
        }
        if (Input.GetKey(KeyCode.O))
        {
            SaveHighScore();
        }


    }

    public void JumpButton()
    {
        //FUNCTION THAT MAKES THE PLAYER JUMP BY ADDING VERTICAL FORCE TO THE PLAYERS RIGIDBODY
        //THIS KEEPS THE PLAYERS MOMENTUM FOR MECHANICS LIKE SLIDING
        //ALSO DISABLES THE JUMP BUTTON
        if (player.GetComponent<PlayerMove>().jumpToggle == true) {           
            player.GetComponent<PlayerMove>().jumpToggle = false;
            block.SetActive(true);
            player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity + Vector2.up * 7;
        }
    }
    public void PauseGame()
    {
        //PAUSES THE GAME AND STOPS ALL OBJECTS FROM MOVING
        Time.timeScale = 0;
        Pause.SetActive(false);
        Play.SetActive(true);
        MainMenu.SetActive(true);
        player.GetComponent<SegmentedRopeHook>().canFire = false;
    }
    public void ContinueGame()
    {
        //UNPAUSES THE GAME
        Time.timeScale = 1;
        Pause.SetActive(true);
        Play.SetActive(false);
        MainMenu.SetActive(false);
        player.GetComponent<SegmentedRopeHook>().canFire = true;
    }
    public void RetryLevel()
    {
        //RELOADS CURRENT LEVEL

        Application.LoadLevel(Application.loadedLevel);
       
        Time.timeScale = 1;
        Pause.SetActive(true);       
        Play.SetActive(false);
        
    }
    public void FinishScreen()
    {
        //ENABLES THE FINSIH MENU ON UI 
        Complete.SetActive(true);

    }
    public void LevelComplete()
    {
        //LOADS NEXT LEVEL
        Application.LoadLevel(NextLevelNum);
    }
    public void MainMenuReturn()
    {
        //RETURNS PLAYER TO MENU
        Time.timeScale = 1;
        Application.LoadLevel(0);
        
    }
    public void LoadHighScore()
    {
        //LOADS HIGH SCORE FROM BINARY FILE BASED ON WHICH LEVEL IS LOADED
        if (File.Exists(Application.persistentDataPath + "/playerData.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerData.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
           Debug.Log("Started Loading");
            if (level == 1) 
            {
                Debug.Log("LoadedLevel1");
                highScore = data.Level1score;
                HiScoreText.text = highScore.ToString();
                Debug.Log("Score:");
                Debug.Log(highScore);
            }
            
            if (level == 2)
            {
                Debug.Log("LoadedLevel2");
                highScore = data.Level2score;
                HiScoreText.text = highScore.ToString();
                Debug.Log(highScore);
            }
            
        }else
        {
            Debug.Log("No File or Broken");
        }
        
    }

      public void SaveHighScore()
      {
        //COMPARES CURRENT SCORE TO HIGHSCORE FROM BINARY FILE AND SAVES OVER IT IF SCORE IS HIGHER
        FileStream stream;
        BinaryFormatter bf = new BinaryFormatter();
        stream = File.Create(Application.persistentDataPath + "/playerData.dat");
        PlayerData playerData = new PlayerData();
        Debug.Log("Saving");
        if (score > highScore)
        {
            if (level == 1)
            {
                Debug.Log("SavingLV1");
                playerData.Level1score = score;
                Debug.Log(playerData.Level1score);
            }
            if (level == 2)
            {
                Debug.Log("SavingLV2");
                playerData.Level2score = score;
                Debug.Log(playerData.Level2score);
            }
        }
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
