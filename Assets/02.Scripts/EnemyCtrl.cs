using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour {

    public HeroCtrl.Side side = HeroCtrl.Side.RIGHT;

    private float hp = 100.0f;
    private Animator anim;
    private SpriteRenderer sp;
    private Transform tr;
    public bool isFire = false;
    public bool isDie = false;

    public GameObject bullet;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        sp = GetComponent<SpriteRenderer>();
        sp.flipX = (side == HeroCtrl.Side.LEFT);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isFire && !isDie)
        {
            tr.Translate(Vector2.right * (int)side * Time.deltaTime * 3.0f);
        }

        RaycastHit2D hit = Physics2D.Raycast(tr.position + (Vector3.up * 0.5f), (int)side * Vector2.right, 5.0f, 1 << 8);

        Debug.DrawRay(tr.position + (Vector3.up * 0.5f), (int)side * Vector2.right * 5.0f, Color.green);

        if (hit.collider != null)
        {
            isFire = true;
        }
        else
        {
            isFire = false;
        }

        if (isFire && Time.time >= nextFire)
        {
            Fire();
            nextFire = Time.time + fireRate;
        }

	}

    void Fire()
    {
        GameObject _bullet = Instantiate(bullet, tr.position + (Vector3.up * 0.8f), Quaternion.identity) as GameObject;
        _bullet.GetComponent<BulletCtrl>().side = this.side;
    }


    void OnTriggerEnter2D ( Collider2D coll )
    {
        if (coll.tag == "BULLET_HERO")
        {
            anim.SetTrigger("hit");
            hp -= 10.0f;
            Destroy(coll.gameObject);
            if (hp <= 0.0f)
            {
                EnemyDie();
            }
        }
    }

    void EnemyDie()
    {
        isDie = true;

        GameMgr.instance.IncKillCount();

        anim.SetTrigger("die");
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        //GetComponent<Rigidbody2D>().isKinematic = true;
        Invoke("RemoveEnemy", 3.0f);
    }

    void RemoveEnemy()
    {
        Destroy(this.gameObject);
    }

}
