using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 250;
    public float climbSpeed;
    public float jumpForce = 300;
    
    private bool isJumping;
    private bool isGrounded;
    [HideInInspector]
    public bool isClimbing;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;
    private float verticalMovement;

    public static MovePlayer instance;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
        verticalMovement = Input.GetAxis("Vertical") * climbSpeed * Time.fixedDeltaTime;

        // 🔹 Détection du saut
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        // 🔹 Mise à jour des animations
        Flip(rb.linearVelocity.x);
        float characterVelocity = Mathf.Abs(rb.linearVelocity.x);
        
        animator.SetFloat("speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {
        // Vérifier si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        animator.SetBool("isGrounded", isGrounded);

        MoveCharacter(horizontalMovement * Time.fixedDeltaTime, verticalMovement);
    }

    void MoveCharacter(float _horizontalMovement, float _verticalMovement)
    {
        if (!isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.linearVelocity.y);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

            // 🔹 Appliquer le saut
            if (isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false; 
            }
        }
        else
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);
        }

        // 🔹 Arrêter l'animation de saut quand le joueur atterrit
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
            spriteRenderer.flipX = false;
        else if (_velocity < -0.1f)
            spriteRenderer.flipX = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
