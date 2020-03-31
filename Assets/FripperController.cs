using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    // 最大タップ数
    private int maxTapCount = 5;
    //指IDごとに初回タップ時に左右のどちらを押したか保持
    private string[] arrayFstTapSide = new string[5];

    // Use this for initialization
    void Start() {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update() {
        // 矢印キーが押された時の挙動メソッド呼び出し
        EnterCursolKey();

        // タップされた場合
        if (Input.touchCount > 0) {
            // タップされている指分だけループ
            for (int i = 0; i < Input.touchCount; i++) {
                // タップされた指が最大タップ数以下の場合
                if (i < maxTapCount) {
                    // タップされた時の挙動メソッドを呼び出し
                    TapScreen(Input.GetTouch(i));
                } else {
                    // タップされた指が最大タップ数を超えた場合は処理しない
                }
            }
        }
    }

    /** 矢印キーが押された時の挙動メソッド*/
    private void EnterCursolKey() {
        //左矢印キーを押した時左フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag") {
            SetAngle(this.flickAngle);
        }
        //右矢印キーを押した時右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag") {
            SetAngle(this.flickAngle);
        }
        //矢印キー離された時フリッパーを元に戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag") {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag") {
            SetAngle(this.defaultAngle);
        }
        return;
    }

    /** タップされた時の挙動メソッド*/
    private void TapScreen(Touch touch) {
        // 画面右側がタップされている場合
        if (touch.position.x >= Screen.width / 2 && tag == "RightFripperTag") {
            // タップされた場合
            if (touch.phase == TouchPhase.Began) {
                // フリッパーを弾く角度に動かす
                SetAngle(this.flickAngle);

                // タップ保持用配列[fingerID]に右で押されたことを保持
                arrayFstTapSide[touch.fingerId] = "Right";
            }
            // タップされたポイントが離された場合
            if (touch.phase == TouchPhase.Ended) {
                // フリッパーをデフォルトの角度に戻す
                SetAngle(this.defaultAngle);
            }
        }
        // 右側をタップした指が左側に移動した場合
        if (touch.position.x < Screen.width / 2 && tag == "RightFripperTag" && arrayFstTapSide[touch.fingerId] == "Right") {
            // タップ保持用配列[fingerID]を初期化
            arrayFstTapSide[touch.fingerId] = "";
            // フリッパーをデフォルトの角度に戻す
            SetAngle(this.defaultAngle);
        }


        //画面左がタップされている場合
        if (touch.position.x < Screen.width / 2 && tag == "LeftFripperTag") {
            // タップされた場合
            if (touch.phase == TouchPhase.Began) {
                // タップ保持用配列[fingerID]に右で押されたことを保持
                arrayFstTapSide[touch.fingerId] = "Left";
                // フリッパーを弾く角度に動かす
                SetAngle(this.flickAngle);
            }
            // タップされたポイントが離された場合
            if (touch.phase == TouchPhase.Ended) {
                // フリッパーをデフォルトの角度に戻す
                SetAngle(this.defaultAngle);
            }
        }
        // 左側をタップした指が右側に移動した場合
        if (touch.position.x >= Screen.width / 2 && tag == "LeftFripperTag" && arrayFstTapSide[touch.fingerId] == "Left") {
            // タップ保持用配列[fingerID]を初期化
            arrayFstTapSide[touch.fingerId] = "";
            // フリッパーをデフォルトの角度に戻す
            SetAngle(this.defaultAngle);
        }


    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle) {
        JointSpring jointSpr = this.myHingeJoint.spring;
        if (tag == "LeftFripperTag") {
            jointSpr.targetPosition = angle;

        } else if (tag == "RightFripperTag") {
            jointSpr.targetPosition = angle;
        }
        this.myHingeJoint.spring = jointSpr;
    }
}