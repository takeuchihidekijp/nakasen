using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviourはClassの特性上取る
//GameScreenEventとTitleでも初期化しているので注意
public class GameData {

    //敵の数の上限(生成の際のロジックに利用されるので他のコードから更新される。可変)
    public static int NUMBER_OF_ENEMYS = 10;
    //敵の数の上限(ゲームの終了条件とかに利用されるため他のコードから変更しない。不変。言うまでもないが上記と数値を合わせること)
    public static int NUMBER_OF_ENEMYS_STATIC = 10;

    //Characterの数の上限(生成の際のロジックに利用されるので他のコードから更新される。可変)
    public static int NUMBER_OF_CHARACTERS = 10;
    //Characterの数の上限(ゲームの終了条件とかに利用されるため他のコードから変更しない。不変。言うまでもないが上記と数値を合わせること)
    public static int NUMBER_OF_CHARACTERS_STATIC = 10;

    //生成された順番
    public static int NUMBER_OF_GENERATE = 0;

    //ゲーム時間
    public static float TotalTime = 120;

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
