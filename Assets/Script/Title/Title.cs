using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Input.backButtonLeavesApp = true;

        //Titleクラスで初期化処理実行
        GameData.NUMBER_OF_GENERATE = 0;
        GameData.TotalTime = 120;
        GameData.CharacterScore = 0;
        GameData.EnemyScore = 0;
        GameData.CharacterPowNumber = 0;
        GameData.EnemyPowNumber = 0;

#if UNITY_ANDROID
        //解像度をスクリプトから変更
        Screen.SetResolution(1280, 600, true);
#endif
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
