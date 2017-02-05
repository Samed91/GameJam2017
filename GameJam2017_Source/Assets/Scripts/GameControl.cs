using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<Item> inventory = new List<Item>();
}

public class GameControl : MonoBehaviour
{
    public GameData gameData;
    public Inventory inventory;

    void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            gameData = Load<GameData>(Application.persistentDataPath + "/gameData.dat");
            inventory.items = gameData.inventory;
            inventory.RefreshOneOfEach();
        }
        else
        {
            gameData = new GameData();
            Save<GameData>(Application.persistentDataPath + "/gameData.dat", gameData);
        }
    }

    public static T Load<T>(string filename) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                using (Stream stream = File.OpenRead(filename))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as T;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        return default(T);
    }

    public static void Save<T>(string filename, T data) where T : class
    {
        using (Stream stream = File.OpenWrite(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    void OnDisable()
    {
        gameData.inventory = inventory.items;
        Save<GameData>(Application.persistentDataPath + "/gameData.dat", gameData);
        Debug.Log("<color=green> Saved to: " + Application.persistentDataPath + "/gameData.dat" + "</color>");
    }


}
