using UnityEngine;

public class enemySkull : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision = 20;
    public SpriteRenderer graphics;

    public Transform head; // Référence à la tête
    public Vector3 headOffsetLeft = new Vector3(-0.6f, 0, 0);  // Décalage réduit quand retourné
    private Vector3 headOriginalPosition; // Position d'origine de la tête

    private Transform target;
    private int destPoint = 0;

    void Start()
    {
        target = waypoints[0];
        headOriginalPosition = head.localPosition; // Sauvegarde de la position initiale

    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Si l'ennemi est quasiment arrivé à sa destination
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];

            // Flip du sprite
            graphics.flipX = !graphics.flipX;

            // Ajuster la position de la tête SEULEMENT si flipX est activé
            if (graphics.flipX)
                head.localPosition = headOriginalPosition + headOffsetLeft; // Décalé à gauche
            else
                head.localPosition = headOriginalPosition; // Retour à la position normale
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }
}
