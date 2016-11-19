using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

    public static GameMgr instance = null;

    public GameObject enemy;
    public Transform[] points;
    public bool isGameOver = false;
    public int killCount = 0;

    public GameObject cutScene;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public int count = 2;

    void Awake()
    {
        instance = this;
    }

	void Start () {
        StartCoroutine(this.CreateEnemy());
	}
	
    IEnumerator CreateEnemy()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(3.0f);
            int idx = Random.Range(0, points.Length);
            GameObject _enemy = Instantiate(enemy, points[idx].position, Quaternion.identity) as GameObject;
            _enemy.GetComponent<EnemyCtrl>().side = (idx == 0) ? HeroCtrl.Side.RIGHT : HeroCtrl.Side.LEFT;
        }
    }

    public void IncKillCount()
    {
        if (++killCount >= 3)
        {
            isGameOver = true;

            //

            StartCoroutine(this.ShowCutScene());
        }
    }

    //1 : Star1 / 2 / 3
    IEnumerator ShowCutScene()
    {
        cutScene.GetComponent<Animator>().SetTrigger("drop");
        yield return null;
    }

    public IEnumerator ShowStar()
    {
        if (count >= 1)
        {
            star1.SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);

        if (count >= 2)
        {
            star2.SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);

        if (count >= 3)
        {
            star3.SetActive(true);
        }       
    }
}
