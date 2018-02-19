﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finder : MonoBehaviour {

    

    // トリガーに入ってきた瞬間
    private void OnTriggerEnter(Collider other)
    {
        CheckSight(other);
    }

    // トリガーにとどまった間呼ばれ続ける
    private void OnTriggerStay(Collider other)
    {
        CheckSight(other);
    }

    private void CheckSight(Collider other)
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

            // 視界の方向に相手がいるか
            // 前方４５度（cos45 の値より大きい）なら視野の中にいる
            if (Vector3.Dot(dir, enemyDir) > 0.5253f)
            {

                // 見つけた処理
                Debug.Log("Found");

              //  var gm = this.GetComponent<GenerateManager>();
                if(this.GetComponent<GenerateManager>().MyNumber > other.GetComponent<GenerateManager>().MyNumber)
                {
                    Debug.Log("Found1");
                }
                else
                {
                    Debug.Log("Found2");
                }

             //  if(this)

            }
        }
    }
}
