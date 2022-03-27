using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private float bounce;
    private Animator anim;
    private int count = 0;
    private float bounce1 = 18f;
    private float bounce2 = 24f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<CharacterController2D>() != null)
        {
            count++;
            if(count == 1)
            {
                bounce = bounce1;
            }
            else if(count == 2)
            {
                count = 0;
                bounce = bounce2;
            }
            anim.SetTrigger("bounce");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
