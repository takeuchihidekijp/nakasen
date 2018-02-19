using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//仮
using UnityEngine.AI;
//仮
[RequireComponent(typeof(NavMeshAgent))]
public class Finder : MonoBehaviour {
    //仮
    //追いかけるフラグ
    public bool chargeflg = false;
    //逃げるフラグ
    public bool escapeflg = false;
    //仮
    [SerializeField]
    private Transform m_target = null;

    private NavMeshAgent m_navAgent = null;
    //仮
    //仮
    private void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
    }

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
                Debug.Log(this.transform.parent.GetComponent<GenerateManager>().MyNumber);

                Debug.Log(other.gameObject.GetComponent<GenerateManager>().MyNumber);


                if (this.transform.parent.GetComponent<GenerateManager>().MyNumber > other.gameObject.GetComponent<GenerateManager>().MyNumber)
                {
                    Debug.Log("Found: Charge");
                    chargeflg = true;
                    escapeflg = false;
                    //仮
                    m_navAgent.destination = other.gameObject.transform.position;
                    

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
