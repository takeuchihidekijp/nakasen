﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //味方の捕虜リスト
   // private List<GameObject> charapows = new List<GameObject>();
    public List<GameObject> charapows = new List<GameObject>();
    public GameObject EnemyPowPrefab;

    //敵の捕虜リスト
   // private List<GameObject> enemypows = new List<GameObject>();
    public List<GameObject> enemypows = new List<GameObject>();
    public GameObject CharacterPowPrefab;

    //仮2018
    public bool Character_PowFLG = false;
    public bool Enemy_PowFLG = false;
    //仮2018


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RedeemPowCharacter()
    {
        Debug.Log(enemypows.Count);

        //捕虜解放したので表示を０に(プレイヤーが解放したので敵の保持している捕虜の数を０にする)
        GameData.EnemyPowNumber = 0;
        //解放した分の使えるキャラクター増加
        GameData.NUMBER_OF_CHARACTERS += enemypows.Count;

        //捕虜解放したので得点追加
        GameData.CharacterScore = GameData.CharacterScore + enemypows.Count * 5;

        //捕虜解放したのでFLGをOffにする
        Enemy_PowFLG = false;

        var clones = GameObject.FindGameObjectsWithTag("Pow");

        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        enemypows.RemoveAll(enemypows => enemypows);
        Debug.Log("RedeemPowCharacter");
        Debug.Log(enemypows.Count);

    }

    public void RedeemPowEnemy()
    {
        //捕虜解放したので表示を０に（敵が解放したので味方の保持している捕虜の数を０にする）
        GameData.CharacterPowNumber = 0;
        //解放した分の使えるキャラクター増加
        GameData.NUMBER_OF_ENEMYS += charapows.Count;

        //捕虜解放したので得点追加
        GameData.EnemyScore = GameData.EnemyScore + charapows.Count * 5;

        //捕虜解放したのでFLGをOffにする
        Character_PowFLG = false;

        var clones = GameObject.FindGameObjectsWithTag("EnemyPow");

        foreach (var clone in clones)
        {
            Destroy(clone);
        }

        charapows.RemoveAll(charapows => charapows);
    }

    public void AddPow_Character()
    {
        //ここでの-10はGameData.CharacterAreaZと同じ意味。変えたら変える
        //var ins = Instantiate(EnemyPowPrefab, new Vector3(-4, 2, charapows.Count - 10), Quaternion.identity);
        var ins = Instantiate(EnemyPowPrefab, new Vector3(-4, 2, charapows.Count - 13), Quaternion.identity);

        charapows.Add(ins);

        GameData.CharacterPowNumber += 1;

        //捕まえたら得点追加
        GameData.CharacterScore += 5;

        //捕虜になったので使えるキャラクターを減らす
        //すでに生成したときに減らしているので減らすロジック削除
        //GameData.NUMBER_OF_ENEMYS -= 1;

        //仮2018
        if (charapows.Count > 1)
        {
            Character_PowFLG = true;
        }
        //仮2018
    }

    public void AddPow_Enemy()
    {

        //    UnityEngine.Debug.LogError(enemypows.Count);
        //    var ins = Instantiate(CharacterPowPrefab, new Vector3(4, 2, enemypows.Count - 10), transform.rotation);
        //   enemypows.Add(ins);

        // （※）
        // var ins = Instantiate(CharacterPowPrefab, new Vector3(4, 2, enemypows.Count + 10), transform.rotation);
        // ins.transform.LookAt(Vector3.zero);    // （※）
        // enemypows.Add(ins);
        // （※）
        //var ins = Instantiate(CharacterPowPrefab, new Vector3(4, 2, 10 - enemypows.Count), CharacterPowPrefab.transform.rotation);
        var ins = Instantiate(CharacterPowPrefab, new Vector3(4, 2, GameData.EnemyAreaZ - enemypows.Count), CharacterPowPrefab.transform.rotation);
        enemypows.Add(ins);

        GameData.EnemyPowNumber += 1;

        //捕まえたら得点追加
        GameData.EnemyScore += 5;

        //捕虜になったので使えるキャラクターを減らす
        //すでに生成したときに減らしているので減らすロジック削除
        //GameData.NUMBER_OF_CHARACTERS -= 1;

        //仮2018
        if (enemypows.Count > 1)
        {
            Enemy_PowFLG = true;
        }

        /*
（※）たまたま原点がフィールドの中心になっているのでこのようにしてみたが、Stage オブジェクトの Position を設定してもよいし、「こちらを向く」と指定したいオブジェクトを設定してもよい。後者の場合は Character と Enemy で違うオブジェクトを設定し、それぞれの方向を見るようにしてもよい。
*/

    }
}
