using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    // Assignez l'élément UI (par exemple, une icône ou un indicateur) via l'inspecteur
    public GameObject speedUIElement;

    public void AddSpeed(int speedGiven, float speedDuration)
    {
        // Active l'élément UI pour indiquer que l'effet est en cours
        speedUIElement.SetActive(true);
        
        // Augmente la vitesse du joueur
        MovePlayer.instance.moveSpeed += speedGiven;
        
        // Démarre la coroutine pour retirer l'effet après la durée donnée
        StartCoroutine(RemoveSpeed(speedGiven, speedDuration));
    }

    private IEnumerator RemoveSpeed(int speedGiven, float speedDuration)
    {
        // Attend la durée de l'effet
        yield return new WaitForSeconds(speedDuration);
        
        // Réduit la vitesse du joueur pour revenir à la valeur d'origine
        MovePlayer.instance.moveSpeed -= speedGiven;
        
        // Désactive l'élément UI une fois l'effet terminé
        speedUIElement.SetActive(false);
    }
}
