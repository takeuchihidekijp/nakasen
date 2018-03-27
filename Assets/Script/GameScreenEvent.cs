using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    }
}
