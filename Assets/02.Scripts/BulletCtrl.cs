using UnityEngine;
using System.Collections;

public class BulletCtrl : MonoBehaviour {

    public HeroCtrl.Side side = HeroCtrl.Side.RIGHT;
    public float speed = 10.0f;

    private SpriteRenderer sp;
    private Transform tr;

	void Start () {
        sp = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        sp.flipX = (side == HeroCtrl.Side.LEFT);
	}
	
	// Update is called once per frame
	void Update () {
        tr.Translate(Vector2.right * (int)side * Time.deltaTime * speed);
	}
}
