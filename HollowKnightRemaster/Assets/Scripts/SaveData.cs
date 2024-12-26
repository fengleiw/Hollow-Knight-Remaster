using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.LookDev;
using UnityEngine.Rendering;


[System.Serializable]
public struct SaveData
{
    public static SaveData Instance;

    ////Map stuff
    //public HashSet<string> sceneNames;

//Bench stuff

    public string benchSceneName;
    public Vector2 benchPos;

    //Player stuff
    public int playerHealth;
    public float playerMana;
    public bool playerBreakMana;
    public Vector2 playerPosition;
    public string lastScene;

    //Enemies stuff
    //Shade
    public Vector2 shadePos;
    public string sceneWithShade;
    public Quaternion shadeRot;

    public void Initialize()
    {
        //For map
        //if (scenenames == null) scenenames = new hashset<string>();

        //Bench
        if (!File.Exists(Application.persistentDataPath + "/save.bench.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.bench.data"));
        }
        if (!File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.player.data"));
        }
        if (!File.Exists(Application.persistentDataPath + "/save.shade.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.shade.data"));
        }

    }

    


    public void SaveBench()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.bench.data")))
        {
            writer.Write(benchSceneName);
            writer.Write(benchPos.x);
            writer.Write(benchPos.y);
        }
    }

    public void LoadBench()
    {
        if(File.Exists(Application.persistentDataPath + "/save.bench.data"))
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.bench.data")))
            {
                benchSceneName = reader.ReadString();
                benchPos.x = reader.ReadSingle();
                benchPos.y = reader.ReadSingle();
            }
        }
    }

    public void SavePlayerData() //Work fine
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.player.data")))
        {
            playerHealth = PlayerController.Instance.Health;           
            writer.Write(playerHealth);

            playerMana = PlayerController.Instance.Mana;
            writer.Write(playerMana);

            playerBreakMana = PlayerController.Instance.breakMana;           
            writer.Write(playerBreakMana);

            playerPosition = PlayerController.Instance.transform.position;
            writer.Write(playerPosition.x);
            writer.Write(playerPosition.y);

            lastScene = SceneManager.GetActiveScene().name;
            writer.Write(lastScene);
            
        }
    }

    public void LoadPlayerData() //
    {
        if (File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.player.data")))
            {
                //Debug.Log("Player's data loading");
                playerHealth = reader.ReadInt32();
                playerMana = reader.ReadSingle();
                playerBreakMana = reader.ReadBoolean();
                playerPosition.x = reader.ReadSingle();
               // Debug.Log(playerPosition.x);
                playerPosition.y = reader.ReadSingle();
                //Debug.Log(playerPosition.y);
                lastScene = reader.ReadString();


                SceneManager.LoadScene(lastScene);
                PlayerController.Instance.transform.position = playerPosition;
           

                PlayerController.Instance.Health = playerHealth;
                PlayerController.Instance.Mana = playerMana;
                PlayerController.Instance.breakMana = playerBreakMana;
               // Debug.Log("Player's data loading");
            }
        }
        else
        {
            Debug.Log("File doesn't exist");
            PlayerController.Instance.Health = PlayerController.Instance.maxHealth;
            PlayerController.Instance.Mana = 0.5f;
        }
    }

    public void SaveShadeData()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.shade.data")))
        {
            sceneWithShade = SceneManager.GetActiveScene().name;
            shadePos = Shade.Instance.transform.position;
            shadeRot = Shade.Instance.transform.rotation;

            writer.Write(sceneWithShade);

            writer.Write(shadePos.x);
            writer.Write(shadePos.y);   

            writer.Write(shadeRot.x);
            writer.Write(shadeRot.y);
            writer.Write(shadeRot.z);
            writer.Write(shadeRot.w);

        }
    }
    
    public void LoadShadeData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.shade.data"))
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.shade.data")))
            {
                sceneWithShade = reader.ReadString();
                shadePos.x = reader.ReadSingle();
                shadePos.y = reader.ReadSingle();

                float rotationX = reader.ReadSingle();
                float rotationY = reader.ReadSingle();
                float rotationZ = reader.ReadSingle();
                float rotationW = reader.ReadSingle();
                shadeRot = new Quaternion(rotationX, rotationY, rotationZ, rotationW);
            }
        }
        else
        {
            Debug.Log("Shade doesn't exist");
        }
    }
}
