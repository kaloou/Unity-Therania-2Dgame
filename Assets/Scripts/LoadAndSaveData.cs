using UnityEngine;
public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }
        instance = this;
    }
    void Start()
    {
        Inventory.instance.coinsCount = PlayerPrefs.GetInt("coinsCount", 0);
        Inventory.instance.UpdateTextUI();
       
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("coinsCount", Inventory.instance.coinsCount);
    }
}