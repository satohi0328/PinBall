using UnityEngine;
using System.Collections;

public class FripperController : MonoBehaviour {
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;
    //タップした時の指ID
    private int _fingerId = -1;

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
                Debug.Log(i);
                // タップされた時の挙動メソッドを呼び出し
                TapScreen(Input.GetTouch(i));
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
        // 画面右側がタップされた場合
        if (Input.mousePosition.x >= Screen.width / 2 && tag == "RightFripperTag") {
            // タップされた場合
            if (touch.phase == TouchPhase.Began) {
                // フリッパーを弾く角度に動かす
                SetAngle(this.flickAngle);
            }
            // タップされたポイントが離された場合
            if (touch.phase == TouchPhase.Ended) {
                // フリッパーをデフォルトの角度に戻す
                SetAngle(this.defaultAngle);
            }
        }
        //画面左がタップされた場合
        if (Input.mousePosition.x < Screen.width / 2 && tag == "LeftFripperTag") {

            // タップされた場合
            if (touch.phase == TouchPhase.Began) {
                // フリッパーを弾く角度に動かす
                SetAngle(this.flickAngle);
            }
            // タップされたポイントが離された場合
            if (touch.phase == TouchPhase.Ended) {
                // フリッパーをデフォルトの角度に戻す
                SetAngle(this.defaultAngle);
            }
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