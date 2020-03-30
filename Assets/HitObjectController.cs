using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectController : MonoBehaviour {
    //ScoreControllerコンポーネント
    private ScoreController scoreComp;

    void Start()
    {
        //シーン内にあるScoreコンポーネントを確保
        scoreComp = GameObject.Find("Ball").GetComponent<ScoreController>();
    }

    //衝突時処理：Scoreコンポーネントの衝突時メソッドにタグを渡す
    void OnCollisionEnter(Collision collision)
    {
        scoreComp.addScore(collision.gameObject.tag);
    }
}
