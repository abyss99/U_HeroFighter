using UnityEngine;
using System.Collections;

public class HeroCtrl : MonoBehaviour {
    public enum Side
    {
        LEFT = -1,
        RIGHT = 1
    }

    private Transform tr;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;

    public float moveForce = 200.0f;
    public float jumpForce = 500.0f;
    public float maxSpeed = 5.0f;
    public Side side = Side.RIGHT;
    public GameObject bullet;

    private float h;
    private bool isJump = false;

	void Start () {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
        #if UNITY_EDITOR
        //h = Input.GetAxisRaw("Horizontal");
        #endif

        if (h == 0.0f)
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        else
            side = (Side)h;

        sp.flipX = (side == Side.LEFT) ? true : false;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }

        if (!MouseHover.isHover && Input.GetMouseButtonDown(0))
        {
            Fire();
        }
            
    }

    void Fire()
    {
        Vector2 firePos = new Vector2(tr.position.x + ((int)side * 1.0f), tr.position.y + 0.8f); 
        GameObject _bullet = Instantiate(bullet, firePos, Quaternion.identity) as GameObject;
        _bullet.GetComponent<BulletCtrl>().side = this.side;
    }

    void FixedUpdate()
    {
        //Debug.Log(rb.velocity.x);
        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveForce);
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            float xSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed); 
            rb.velocity = new Vector2(xSpeed, rb.velocity.y);
        }

        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = false;
        }
    }

    public void Run(int _side)
    {
        this.side = (Side)_side;
        h = _side;
    }

    public void Stop()
    {
        h = 0.0f;
    }

    public void Jump()
    {
        isJump = true;
    }
}
