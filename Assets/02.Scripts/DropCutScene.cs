using UnityEngine;
using System.Collections;

public class DropCutScene : MonoBehaviour {

    public void Drop()
    {
        StartCoroutine(GameMgr.instance.ShowStar());
    }
}
