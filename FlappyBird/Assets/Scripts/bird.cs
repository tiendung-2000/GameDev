using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    Rigidbody2D rb;
    public float speed;
    //private Animator anim;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flyClip,pingClip,diedClip;

    private bool isAlive;
    private bool didFlap;

    int angle;
    int maxAngle = 20;
    int minAngle = -90;



    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

    }

    void Update()
    {
        if(isAlive)
        {
            if(didFlap)
            {
                didFlap = false;
                rb.velocity = Vector2.zero;
                rb.velocity = new Vector2(rb.velocity.x, speed);
                audioSource.PlayOneShot(flyClip);
            }
        }

        BirdRotation();
    }

    void BirdRotation()
    {
        if (rb.velocity.y > 0)
        {

            if (angle <= maxAngle)
            {
                angle = angle + 4;
            }
        }
        else if (rb.velocity.y < 0)
        {

            if (rb.velocity.y < -1.3f)
            {
                if (angle >= minAngle)
                {
                    angle = angle - 3;
                }
            }
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void FlapButton()
    {
        didFlap = true;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "PipeHolder")
        {
            audioSource.PlayOneShot(pingClip);
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag =="Pipe" || target.gameObject.tag == "Ground")
        {
            audioSource.PlayOneShot(diedClip);
        }
        gameManager.GameOver();
    }
}
