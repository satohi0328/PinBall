using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BallController : MonoBehaviour {

    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    //ゲームオーバを表示するテキスト
    private GameObject gameoverText;
    // スコア表示用Text
    private GameObject scoreText;

    // Score合計
    private int total = 0;

    // Use this for initialization
    void Start() {
        //シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");
        //シーン中のScore表示用Textオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");

    }

    // Update is called once per frame
    void Update() {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ) {
            //GameoverTextにゲームオーバを表示
            this.gameoverText.GetComponent<Text>().text = "Game Over";
        }
    }

    // 衝突時処理：Scoreコンポーネントの衝突時メソッドにタグを渡す
    void OnCollisionEnter(Collision collision) {
        // 衝突したタグによって点数を加算
        switch (collision.gameObject.tag) {
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

}