using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    // スコア表示用Text
    private GameObject scoreText;

    // WKトータル
    private int total = 0;

	void Start () {
        //シーン中のScoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");
    }

    // スコア計算メソッド
    public void addScore(string hitTag)
    {
        switch (hitTag)
        {
            case "LargeCloudTag":
                total += 30;
                break;
            case "SmallCloudTag":
                total += 10;
                break;
            case "LargeStarTag":
                total += 100;
                break;
            case "SmallStarTag":
                total += 50; 
                break;
            default:
                break;
        }
        // Scoreテキストを更新
        this.scoreText.GetComponent<Text>().text = "Score:" + total.ToString();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
