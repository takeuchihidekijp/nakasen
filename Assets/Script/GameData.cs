using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviourはClassの特性上取る
public class GameData {

    //敵の数の上限
    public static int NUMBER_OF_ENEMYS = 15;

    //Characterの数の上限
    public static int NUMBER_OF_CHARACTERS = 15;

    //生成された順番
    public static int NUMBER_OF_GENERATE = 0;

    //ゲーム時間
    public static float TotalTime = 2 * 60;

    //Characterの得点
    public static int CharacterScore = 0;

    //Enemyの得点
    public static int EnemyScore = 0;

    //Characterの保持している捕虜の数
    public static int CharacterPowNumber = 0;

    //Enemyの保持している捕虜の数
    public static int EnemyPowNumber = 0;

    //Characterの追いかける限界位置(Z軸)
    public static float CharacterChaseLimitZ = 0;

    //Enemyの追いかける限界位置(Z軸)
    public static float EnemyChaseLimitZ = 0;

}
