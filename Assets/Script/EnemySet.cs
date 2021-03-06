﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySet : MonoBehaviour {

    public GameObject prefab;

    public GameObject PowRedeemprefab;

    /// <summary>敵のカウンター。敵のオブジェクト名を unique にするために使う</summary>
    int enemyCounter;

    //30
    [SerializeField] float m_interval = 10.0f;
    float m_timer;


    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0f;
            CreateEnemy();
        }


    }


    void CreateEnemy()
    {
        const int RetryMax = 3;
        //仮
     //   int EnemyCount = 2;

 //       for (int i = 0; i < EnemyCount; i++)
 //       {
            for (int j = 0; j < RetryMax; j++)
            {
                Ray ray = new Ray(new Vector3(Random.Range(-10, 10), 20, Random.Range(10, 20)),Vector3.down);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.tag == "Floor")
                    {

                        // フローアにいる全キャラクターを取得（重いが）
                        Enemy[] Enemys = FindObjectsOfType<Enemy>();

                        bool putable = true;

                        //全キャラクター分ループ
                        foreach (var enemy in Enemys)
                        {
                            //各キャラクターとの距離を測る
                            Vector3 dist = enemy.transform.position - hit.point;

                            //近い場合キャラクターの作成フラグをオフ
                            if (dist.magnitude < 0.8f)
                            {
                                putable = false;
                            }

                        }

                        if(putable == true && GameData.NUMBER_OF_ENEMYS > 0)
                        {
                         //   Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(10.0f, 20.0f));
                         //相手の捕虜がいる場合、確率で捕虜救出用のキャラを出撃させる。５から９の数値より大きくかつ相手の得点が自分のより高い場合
                         if(GameData.CharacterPowNumber > Random.Range(5,9) && GameData.CharacterScore > GameData.EnemyScore)
                        {
                            Instantiate(PowRedeemprefab, hit.point + Vector3.up * 0.6f, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(prefab, hit.point + Vector3.up * 0.6f, Quaternion.identity);
                        }
                         //   Instantiate(prefab, hit.point + Vector3.up * 0.6f, Quaternion.identity);

                        GameData.NUMBER_OF_ENEMYS -= 1;
                        }



                        break;

                    }

                }
            }
   //     }

            //    Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(10.0f, 20.0f));

            //   Instantiate(prefab, pos, Quaternion.identity);

        //    Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(10.0f, 20.0f));

        // Instantiate(prefab, pos, Quaternion.identity);
    //    GameObject go = Instantiate(prefab, pos, Quaternion.identity);
    //    go.name = prefab.name + enemyCounter;   // 敵のオブジェクト名は prefab 名 + 数字とする
    //    enemyCounter++;
    }

}
