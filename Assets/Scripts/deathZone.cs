using UnityEngine;
using System.Collections;
public class deathZone : MonoBehaviour
{
    private Transform playerSpawn;
    private Animator fadeSystem;
    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("playerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ReplacePlayer(collision));
        }
    }
    private IEnumerator ReplacePlayer(Collider2D collision)
    {
        fadeSystem.SetTrigger("fadein");
        yield return new WaitForSeconds(1f);
        collision.transform.position = playerSpawn.position;
    }
}