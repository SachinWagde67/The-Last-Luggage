using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum ThrowState
    {
        Throw,
        Teleport
    }

    private ThrowState throwState;
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject teleporter;
    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;
    private bool isTeleporterDestroyed = true;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform landCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject teleporterObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        throwState = ThrowState.Throw;
    }

    private void Update()
    {
        playerMove();
        playerJump();
        playerFlip();
        playerThrow();
    }

    private void playerThrow()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (throwState == ThrowState.Throw)
            {
                if (isTeleporterDestroyed)
                {
                    isTeleporterDestroyed = false;
                    anim.SetTrigger("throw");
                }
            }
            if (throwState == ThrowState.Teleport)
            {
                if (!isTeleporterDestroyed)
                {
                    this.gameObject.transform.position = teleporter.transform.position;
                    destroyTeleporter();
                }
                else
                {
                    throwState = ThrowState.Throw;
                    isTeleporterDestroyed = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (throwState == ThrowState.Teleport)
            {
                destroyTeleporter();
            }
        }
    }

    private void playerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("jump");
        }
        if(isGrounded)
        {
            anim.SetBool("fall", false);
        }
    }

    private void playerMove()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(moveInput));
    }

    private void playerFlip()
    {
        if (moveInput > 0)
        {
            facingRight = true;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (moveInput < 0)
        {
            facingRight = false;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void Fall()
    {
        anim.SetBool("fall", true);
    }

    private async void throwTeleporter()
    {
        if (facingRight)
        {
            teleporter = (GameObject)Instantiate(teleporterObject, throwPoint.position, Quaternion.Euler(0f, 0f, 0f));
            teleporter.GetComponent<Teleporter>().Initialize(Vector2.right);
        }
        else
        {
            teleporter = (GameObject)Instantiate(teleporterObject, throwPoint.position, Quaternion.Euler(0f, 0f, 180f));
            teleporter.GetComponent<Teleporter>().Initialize(Vector2.left);
        }
        await new WaitForSeconds(0.5f);
        throwState = ThrowState.Teleport;
    }

    public void destroyTeleporter()
    {
        isTeleporterDestroyed = true;
        throwState = ThrowState.Throw;
        Destroy(teleporter);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("complete"))
        {
            moveSpeed = 0;
        }
    }
}
