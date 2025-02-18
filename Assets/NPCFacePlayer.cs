using UnityEngine;

public class NPCFacePlayer : MonoBehaviour
{
    public Transform player; // Référence au transform du joueur
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // Si le joueur est à droite du NPC
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // Le NPC fait face à droite
            }
            // Si le joueur est à gauche du NPC
            else if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true; // Le NPC fait face à gauche
            }
        }
    }
}
