using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite[] mapSprites; // Tableaux des sprites de la carte
    public Image panelImage; // Composant Image du panneau

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        // Mettre à jour l'interactivité des boutons
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
        Debug.Log("Niveau atteint start : " + levelReached);


        // Mettre à jour l'image du panneau en fonction du niveau atteint
        if (levelReached - 1 < mapSprites.Length)
        {
            panelImage.sprite = mapSprites[levelReached - 1];
        }
        else
        {
            Debug.LogWarning("Le niveau atteint dépasse le nombre de sprites disponibles.");
        }
    }

    public void LoadLevelPassed(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
