using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public int score;
    public float spawnX, spawnY;
    public float move;

    void Start()
    {
        spawnX = transform.position.x;
        spawnY = transform.position.y;
    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        move = Input.GetAxis("Horizontal");
    }

    void Update()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "fire")
        {
            Destroy(col.gameObject);
            score++;   
        }
           
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "saw" || col.gameObject.name =="dieFall") transform.position = new Vector3(spawnX, spawnY, transform.position.z);
        if (col.gameObject.name == "endLvl") Application.LoadLevel("scene2");
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 100), "Score=" + score);
    }
}