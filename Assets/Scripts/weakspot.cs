using UnityEngine;
public class weakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;

     public AudioClip killSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(killSound, transform.position);
            Destroy(objectToDestroy);
        }
    }
}