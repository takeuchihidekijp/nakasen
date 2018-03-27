using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviourはClassの特性上取る
public class GameData {

    //敵の数の上限
    public static int NUMBER_OF_ENEMYS = 10;

    //Characterの数の上限
    public static int NUMBER_OF_CHARACTERS = 10;

    //生成された順番
    public static int NUMBER_OF_GENERATE = 0;

    //ゲーム時間
    public static float TotalTime = 1 * 60;

    //Characterの得点
    public static int CharacterScore = 0;

    //Enemyの得点
    public static int EnemyScore = 0;

    //Characterの陣地のZ軸
    public static int CharacterAreaZ = -13;

    //Enemyの陣地のZ軸
    //Characterと値が違うのはゲームのバランスを検討中
    public static int EnemyAreaZ = 12;

    //Characterの保持している捕虜の数
    public static int CharacterPowNumber = 0;

    //Enemyの保持している捕虜の数
    public static int EnemyPowNumber = 0;

    //Characterの追いかける限界位置(Z軸)
    public static float CharacterChaseLimitZ = 9;

    //Enemyの追いかける限界位置(Z軸)
    public static float EnemyChaseLimitZ = -9;

}
