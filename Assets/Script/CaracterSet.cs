using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaracterSet : MonoBehaviour {


    public Camera mainCamera;
    public Camera strategyCamera;

    public GameObject prefab;

    public GameObject PowRedeemPrefab;


    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {


        // マウスクリック
        if (Input.GetMouseButtonDown(0))
        {
            // マウス座標と指定カメラからの Rayを作成
            //ストラテジーカメラではなくいったんMainのみで。ストラテジー画面は今回は使わない
         //   Ray ray = strategyCamera.ScreenPointToRay(Input.mousePosition);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ray と何か（オブジェクト）がヒットするか？
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                // 何かにヒットしたら if に入る

                if (hit.collider.gameObject.tag == "Floor")
                {
                    // フローアにいる全キャラクターを取得（重いがクリックしたときのみなので）
                    Character[] characters = FindObjectsOfType<Character>();

                    bool putable = true;

                    //全キャラクター分ループ
                    foreach (var character in characters)
                    {
                        //各キャラクターとの距離を測る
                        Vector3 dist = character.transform.position - hit.point;

                        //近い場合キャラクターの作成フラグをオフ
                        if (dist.magnitude < 0.8f)
                        {
                            putable = false;
                        }

                    }
                    //おける場合は、上記条件でputable == trueの場合と残キャラがいる場合と配置可能位置（Z軸で―10以下）if (putable == true && GameData.NUMBER_OF_CHARACTERS > 0 && hit.point.z < GameData.CharacterAreaZ)
                    //if (putable == true && GameData.NUMBER_OF_CHARACTERS > 0 && hit.point.z < -10)
                    if (putable == true && GameData.NUMBER_OF_CHARACTERS > 0 && hit.point.z < GameData.CharacterAreaZ)
                    {
                        //通常モードなら普通のキャラをそうでないなら捕虜解放用のキャラクターを設定
                        GameObject normal_flg = GameObject.Find("Normal");                   

                        if (normal_flg.GetComponent<ToggleCharacterChange>().normal_flg == true)
                        {
                            Instantiate(prefab, hit.point + Vector3.up * 0.6f, prefab.transform.rotation);

                        }
                        else
                        {
                            Instantiate(PowRedeemPrefab, hit.point + Vector3.up * 0.6f, PowRedeemPrefab.transform.rotation);
                        }
                      //  Instantiate(prefab, hit.point + Vector3.up * 0.6f, prefab.transform.rotation);

                        GameData.NUMBER_OF_CHARACTERS -= 1;

                        
                    }


                }
            }
        }


    }

}
