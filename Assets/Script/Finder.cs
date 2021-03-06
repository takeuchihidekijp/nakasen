﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour {
    //仮
    //追いかけるフラグ
    public bool chargeflg = false;
    //逃げるフラグ
    public bool escapeflg = false;

    //つかまったフラグ
    public bool chachedflg = false;

    //逃げる用のフラグ
    public bool escape_down_flg = false;

    public bool escape_up_flg = false;

    public bool escape_right_flg = false;

    public bool escape_left_flg = false;


    //見つけた相手の GameObject を保持する変数
    public GameObject target;


    //味方の捕虜リスト
    private List<GameObject> charapows = new List<GameObject>();
    public GameObject EnemyPowPrefab;

    //敵の捕虜リスト
    private List<GameObject> enemypows = new List<GameObject>();
    public GameObject CharacterPowPrefab;


    // トリガーに入ってきた瞬間
    public void OnTriggerEnter(Collider other)
    {
        CheckSight(other);
    }

    // トリガーにとどまった間呼ばれ続ける
    public void OnTriggerStay(Collider other)
    {
        CheckSight(other);
    }

    public Vector3 CheckSight1(Collider other1)
    {

        return other1.transform.position;
    }

    public void CheckSight(Collider other)
    {
        if (other.gameObject.tag == "Character" && this.transform.parent.gameObject.layer != other.gameObject.layer)
        {


            // 自分の向いてる方向
            Vector3 dir = this.transform.rotation * Vector3.forward;
            Debug.DrawLine(this.transform.position, this.transform.position + dir, Color.blue, 0.1f, false);

            // 敵の方向
            Vector3 enemyDir = other.transform.position - this.transform.position;

            // 敵方向ベクトルの正規化
            enemyDir.Normalize();
            Debug.DrawLine(this.transform.position, this.transform.position + enemyDir, Color.red, 0.1f, false);

            Debug.Log("dir");
            Debug.Log(dir);

            Debug.Log("enemyDir");
            Debug.Log(enemyDir);

            Debug.Log("Vector3.Dot(dir, enemyDir)");
            Debug.Log(Vector3.Dot(dir, enemyDir));

            // 視界の方向に相手がいるか
            // 前方４５度（cos45 の値より大きい）なら視野の中にいる
            if (Vector3.Dot(dir, enemyDir) > 0.5253f)
            {

                // 見つけた処理
                Debug.Log("Found");
                Debug.Log(this.transform.parent.GetComponent<GenerateManager>().MyNumber);

                Debug.Log(other.gameObject.GetComponent<GenerateManager>().MyNumber);


                if (this.transform.parent.GetComponent<GenerateManager>().MyNumber > other.gameObject.GetComponent<GenerateManager>().MyNumber)
                {
                    //追いかける
                    Debug.Log("Found: Charge");
                    chargeflg = true;
                    //いったんオフにするのを外してみる。デフォルトでFalseだから。
                    //   escapeflg = false;

                    // 追いかける相手を変数に渡す
                    target = other.gameObject;

                    //相手との距離が近い場合は捕まえる
                    float dist = Vector3.Distance(this.transform.parent.position,other.transform.position);
                    Debug.Log("Dist");
                    Debug.Log(dist);

                    if(dist < 1.0f && chachedflg == false)
                    {
                        Debug.Log("Catched!");
                        Debug.Log(dist);
                        //捕まえたので追いかけるフラグをオフにして捕まえたフラグをオンにする
                        chargeflg = false;
                        chachedflg = true;

                        //！！なぜ2回呼ぶのか別途確認！！ ->いったん削除して様子見20180318
                     //   Destroy(other.gameObject);

                        if (this.transform.parent.gameObject.name == "Character(Clone)")
                        {
                            //仮
                            //`gameObject` はスクリプトがアタッチされたゲームオブジェクトを指すので、ここではFinderがアタッチされているSearchColliderを指します。
                            // つまり `gameObject.GetComponent<GameManager>()` はSearchColliderからGameManagerのコンポーネントを取ってくるという意味になります。
                           //SearchColliderはGameManagerを持っていないので結果がNullとなり、エラーになります。
                            //GameManagerを持っているのはPowCreateなのでそちらからGetComponentする必要があります。
                            //
                            GameObject powcreate = GameObject.Find("PowCreate");

                            powcreate.GetComponent<GameManager>().AddPow_Character();
                            //！！なぜ2回呼ぶのか別途確認！！
                            Destroy(other.gameObject);
                            Debug.Log("CharacterPow!");
                            Debug.Log(this.transform.parent.gameObject.name);
                        }

                        else if (this.transform.parent.gameObject.name == "Enemys(Clone)")
                        {
                            //仮
                            GameObject enemypowcreate = GameObject.Find("PowCreate");

                            enemypowcreate.GetComponent<GameManager>().AddPow_Enemy();
                            //！！なぜ2回呼ぶのか別途確認！！
                            Destroy(other.gameObject);
                            Debug.Log("EnemyPow!");
                            Debug.Log(this.transform.parent.gameObject.name);
                        }
                        else if(this.transform.parent.gameObject)
                        {
                            Debug.Log("何かがおかしい!");
                            Debug.Log(this.transform.parent.gameObject.name);
                        }
                        

                    }



                }
                else
                {

                    Debug.Log("Found: Escape");
                    chargeflg = false;
                    escapeflg = true;

                    // 追いかけられている相手を変数に渡す
                    target = other.gameObject;

                    //otherの方が強いのでotherの位置から自分の位置をひく
                    Vector3 dist_escape = other.transform.position - this.transform.parent.position;

                    if (Mathf.Abs(dist_escape.x) < Mathf.Abs(dist_escape.z))
                    {
                        if(dist_escape.z > 0)
                        {
                            //下に逃げる
                            escape_down_flg = true;
                            escape_up_flg = false;
                            escape_right_flg = false;
                            escape_left_flg = false;
                            Debug.Log("Found: Escape 下に逃げる");

                        }
                        else
                        {
                            //上に逃げる
                            escape_down_flg = false;
                            escape_up_flg = true;
                            escape_right_flg = false;
                            escape_left_flg = false;
                            Debug.Log("Found: Escape 上に逃げる");

                        }
                    }
                    else
                    {
                        // X軸の距離がZ軸の距離より大きい
                        if(dist_escape.x > 0)
                        {
                            // 左に逃げる
                            escape_down_flg = false;
                            escape_up_flg = false;
                            escape_right_flg = false;
                            escape_left_flg = true;

                            Debug.Log("Found: Escape 左に逃げる");
                        }
                        else
                        {
                            // 右に逃げる
                            escape_down_flg = false;
                            escape_up_flg = false;
                            escape_right_flg = true;
                            escape_left_flg = false;

                            Debug.Log("Found: Escape 右に逃げる");
                        }
                    }
                }


            }
            Debug.Log("ダンマリもしくは通常移動？");
            //ここにロジックを入れてみる 以下、ダンマリ対策ロジック（仮）

            //相手との距離が近い場合は捕まえる
            float dist1 = Vector3.Distance(this.transform.parent.position, other.transform.position);
            Debug.Log("Dist1 ダンマリもしくは通常移動");
            Debug.Log(dist1);

            if (this.transform.parent.GetComponent<GenerateManager>().MyNumber > other.gameObject.GetComponent<GenerateManager>().MyNumber && dist1 < 1.0f && chachedflg == false)
            {
                //捕まえたので追いかけるフラグをオフにして捕まえたフラグをオンにする
                chargeflg = false;
                chachedflg = true;

                if (this.transform.parent.gameObject.name == "Character(Clone)")
                {
                    //仮
                    GameObject powcreate = GameObject.Find("PowCreate");

                    powcreate.GetComponent<GameManager>().AddPow_Character();
                    Destroy(other.gameObject);
                    Debug.Log("CharacterPow!");
                    Debug.Log(this.transform.parent.gameObject.name);
                }

                else if (this.transform.parent.gameObject.name == "Enemys(Clone)")
                {
                    //仮
                    GameObject enemypowcreate = GameObject.Find("PowCreate");

                    enemypowcreate.GetComponent<GameManager>().AddPow_Enemy();
                    //！！なぜ2回呼ぶのか別途確認！！
                    Destroy(other.gameObject);
                    Debug.Log("EnemyPow!");
                    Debug.Log(this.transform.parent.gameObject.name);
                }
                else if (this.transform.parent.gameObject)
                {
                    Debug.Log("何かがおかしい!");
                    Debug.Log(this.transform.parent.gameObject.name);
                }


            }
            //ここまでロジックを入れてみる ダンマリ対策ロジック（仮）
        }
    }


}
