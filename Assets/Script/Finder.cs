using System.Collections;
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

    //見つけた相手の GameObject を保持する変数
    public GameObject target;

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
                    escapeflg = false;

                    // 追いかける相手を変数に渡す
                    target = other.gameObject;

                    //相手との距離が近い場合は捕まえる
                    float dist = Vector3.Distance(this.transform.parent.position,other.transform.position);
                    Debug.Log("Dist");
                    Debug.Log(dist);

                    if(dist < 1.5f)
                    {
                        Debug.Log("Catched!");
                        Debug.Log(dist);
                        chachedflg = true;
                    }



                }
                else
                {
                    Debug.Log("Found: Escape");
                    chargeflg = false;
                    escapeflg = true;
                }

             //  if(this)

            }
        }
    }
}
