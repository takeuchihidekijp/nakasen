using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScreenEvent : MonoBehaviour {

    //Character用スコアを表示するテキスト
    private GameObject chara_scoreText;

    //Character用使えるメンバを表示するテキスト
    private GameObject chara_memberText;

    //Characterの保持している捕虜の数を表示するテキスト
    private GameObject chara_powText;


    //Game終了時に表示するテキスト（You Win or You Lose）
    private GameObject stateText;

    //GameのTimerを表示するテキスト
    private GameObject timerText;


    //enemy用スコアを表示するテキスト
    private GameObject enemy_scoreText;

    //enemy用使えるメンバを表示するテキスト
    private GameObject enemy_memberText;

    //enemyの保持している捕虜の数を表示するテキスト
    private GameObject enemy_powText;

    private bool isGameOver = false;


    // Use this for initialization
    void Start () {

        this.chara_scoreText = GameObject.Find("CharaScoreText");

        this.chara_memberText = GameObject.Find("CharaMemberText");

        this.chara_powText = GameObject.Find("CharaPowText");


        this.stateText = GameObject.Find("GameResultText");

        this.timerText = GameObject.Find("TimeText");


        this.enemy_scoreText = GameObject.Find("EnemyScoreText");

        this.enemy_memberText = GameObject.Find("EnemyMemberText");

        this.enemy_powText = GameObject.Find("EnemyPowText");

        //初期化 Start
        isGameOver = false;

        GameData.TotalTime = 2 * 60;

        GameData.NUMBER_OF_CHARACTERS = 10;

        GameData.NUMBER_OF_ENEMYS = 10;

        GameData.NUMBER_OF_GENERATE = 0;

        GameData.CharacterScore = 0;

        GameData.EnemyScore = 0;

        GameData.CharacterPowNumber = 0;

        GameData.EnemyPowNumber = 0;
        //初期化 End
    }

    // Update is called once per frame
    void Update () {

        //Timerを減らす
        GameData.TotalTime -= Time.deltaTime;

        //Timerを表示
        this.timerText.GetComponent<Text>().text = "Time" + GameData.TotalTime + "s";

        //CharacterScoreを表示
        this.chara_scoreText.GetComponent<Text>().text = "Score:" + GameData.CharacterScore;

        //Characterの保持している捕虜の数表示
        this.chara_powText.GetComponent<Text>().text = "CharaPow:" + GameData.CharacterPowNumber;


        //EnemyScoreを表示
        this.enemy_scoreText.GetComponent<Text>().text = "Score:" + GameData.EnemyScore;

        //Enemyの保持している捕虜の数を表示
        this.enemy_powText.GetComponent<Text>().text = "EnemyPow:" + GameData.EnemyPowNumber;

        //CharacterMember数を表示
        this.chara_memberText.GetComponent<Text>().text = "CharaMem:" + GameData.NUMBER_OF_CHARACTERS;

        //EnemyMember数を表示
        this.enemy_memberText.GetComponent<Text>().text = "EnemyMem:" + GameData.NUMBER_OF_ENEMYS;

        if(GameData.TotalTime <= 0)
        {
            this.isGameOver = true;




                if(GameData.CharacterScore >= GameData.EnemyScore)
                {
                    this.stateText.GetComponent<Text>().text = "YOU WIN";
                }
                else
                {
                    this.stateText.GetComponent<Text>().text = "YOU LOSE";
                }
        }

        // ゲームオーバになった場合
        if (this.isGameOver)
        {
            //ゲームオーバになったら時間を止める。
            Time.timeScale = 0.0f;

            // クリックされたらシーンをロードする（追加）
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1.0f;

                //GameSceneを読み込む（追加）
                SceneManager.LoadScene("Title");
            }
        }

    }
}
