using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour
{
    public Transform graphic; // L'objet graphique attaché au checkpoint
    public float moveDistance = 1f; // Distance du déplacement sur Y
    public float moveDuration = 0.5f; // Durée du déplacement

    private Transform playerSpawn;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("playerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            StartCoroutine(MoveGraphic()); // Lance l'animation de montée
            gameObject.GetComponent<BoxCollider2D>().enabled = false; // Désactive le trigger après activation
        }
    }

    private IEnumerator MoveGraphic()
    {
        Vector3 startPos = graphic.position;
        Vector3 endPos = startPos + new Vector3(0, moveDistance, 0);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            graphic.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        graphic.position = endPos; // Assurer la position finale exacte
    }
}
