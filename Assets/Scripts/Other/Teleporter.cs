using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    [SerializeField] private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("door") || other.gameObject.CompareTag("mushroom"))
        {
            speed = 0f;
        }
    }
}
