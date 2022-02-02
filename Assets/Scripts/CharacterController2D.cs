using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	private enum ThrowState
    {
		Throw,
		Teleport
    }

	public InventoryUI inventoryUI;
	
	[SerializeField] private float jumpForce;
	[SerializeField] public float speed;	
	[SerializeField] private bool airControl = false;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform fallCheck;
	[SerializeField] private Transform throwPoint;
	[SerializeField] private Animator anim;
	[SerializeField] private GameObject teleporterObject;

	private ThrowState throwState;
	private GameObject teleporter;
	private Rigidbody2D rb;
	private float horizontal;
	private const float groundedRadius = .05f; 
	private const float fallRadius = .05f;
	private bool grounded;
	private bool isDestroyed = true;
	private bool facingRight = true;
	private bool jump = false;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		throwState = ThrowState.Throw;
	}

	private void Update() 
	{
		horizontal = Input.GetAxisRaw("Horizontal") * speed;
		anim.SetFloat("speed", Mathf.Abs(horizontal));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			anim.SetBool("jump", true);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			if(throwState == ThrowState.Throw)
            {
				if(isDestroyed)
				{ 
					isDestroyed = false;
					anim.SetTrigger("throw");
				}
			}
			if(throwState == ThrowState.Teleport)
            {
				if (!isDestroyed)
				{
					this.gameObject.transform.position = teleporter.transform.position;
					destroyTeleporter();
				}
				else
				{
					throwState = ThrowState.Throw;
					isDestroyed = true;
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Q))
        {
			if(throwState == ThrowState.Teleport)
            {
				destroyTeleporter();
			}
        }
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasGrounded)
				{
					anim.SetBool("jump", false);
					anim.ResetTrigger("land");
				}
			}
		}
        Collider2D[] fallcollider = Physics2D.OverlapCircleAll(fallCheck.position, fallRadius, whatIsGround);
        for (int i = 0; i < fallcollider.Length; i++)
        {
            if (fallcollider[i].gameObject != gameObject)
            {
                anim.SetBool("fall", false);
                anim.SetTrigger("land");
            }
        }
        Move(horizontal * Time.fixedDeltaTime, jump);
		jump = false;
	}


	public void Move(float move, bool jump)
	{
		if (grounded || airControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			rb.velocity = targetVelocity;

			if (move > 0 && !facingRight)
			{
				Flip();
			}
			else if (move < 0 && facingRight)
			{
				Flip();
			}
		}
		if (grounded && jump)
		{
			grounded = false;
			rb.AddForce(new Vector2(0f, jumpForce * 100f));
		}
	}
	private void Flip()
	{
		facingRight = !facingRight;
		transform.Rotate(0, 180, 0);
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
		isDestroyed = true;
		throwState = ThrowState.Throw;
		Destroy(teleporter);
	}
}