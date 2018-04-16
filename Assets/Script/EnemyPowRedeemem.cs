using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPowRedeemem : MonoBehaviour {

    Vector3 movement;
    Rigidbody characterRigidbody;

    UnityEngine.AI.NavMeshAgent nav;

    [SerializeField] float m_interval = 1.5f;
    float m_timer;

    //移動ポイントのオブジェクト用List（キャラクターの移動ポイントをMovePointとする）
    private int EnemyMovePointCount = 5;
    private List<GameObject> enemy_movepointList = new List<GameObject>();

    private int EnemyReturnPointCount = 5;
    private List<GameObject> enemy_returnpointList = new List<GameObject>();




    bool backflg = false;

    enum MoveState
    {
        Stay,
        Forward,
        Back,
        Chase,
        Escape,
        End,
    }

    MoveState movestate;

    // Use this for initialization
    void Start () {

        for (int i = 1; i <= EnemyMovePointCount; ++i)
        {
            GameObject movepoint = GameObject.Find("EnemyMovePoint" + i.ToString());

            if (movepoint != null)
            {
                enemy_movepointList.Add(movepoint);
            }
        }

        for (int i = 1; i <= EnemyReturnPointCount; ++i)
        {
            GameObject returnpoint = GameObject.Find("EnemyReturnPoint" + i.ToString());

            if (returnpoint != null)
            {
                enemy_returnpointList.Add(returnpoint);
            }
        }

        characterRigidbody = GetComponent<Rigidbody>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log("Update　EnemyPowRedeemem");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);
        Debug.Log(movestate);
        Debug.Log(nav.pathPending);

        //捕虜解放者は捕虜めがけて突っ込むだけで追いかけたり逃げたりしない。

        switch (movestate)
        {

            case MoveState.Stay:
                Stay();
                break;
            case MoveState.Forward:
                Forward();
                break;
            case MoveState.Back:
                Back();
                break;
            case MoveState.End:
                End();
                break;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "EnemyPow")
        {

            GameObject powredeem = GameObject.Find("PowCreate");

            powredeem.GetComponent<GameManager>().RedeemPowEnemy();

            movestate = MoveState.Back;

        }

        if (collision.gameObject.tag == "Line")
        {
            backflg = true;
        }



    }

    void Stay()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0f;
            // 行き先を決める
            GameObject powcreate = GameObject.Find("PowCreate");

            //つかまっているキャラがいればそこを目指す　そうでなければ通常の移動ポイントへ
            if (powcreate.GetComponent<GameManager>().Character_PowFLG == true)
            {
                //検証が必要
                nav.SetDestination(powcreate.GetComponent<GameManager>().charapows[powcreate.GetComponent<GameManager>().charapows.Count - 1].transform.position);

                return;
            }
            else
            {
                int m = Random.Range(0, enemy_movepointList.Count);
                nav.SetDestination(enemy_movepointList[m].transform.position);
            }

            movestate = MoveState.Forward;
        }

    }

    void Stay1()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0f;
            // 行き先を決める
            int m = Random.Range(0, enemy_movepointList.Count);
            nav.SetDestination(enemy_movepointList[m].transform.position);

            movestate = MoveState.Forward;
        }

    }

    void Forward()
    {

        Debug.Log("捕虜解放用キャラnav.pathStatus and nav.hasPath　Enemy");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);

        GameObject powcreate = GameObject.Find("PowCreate");

        if (powcreate.GetComponent<GameManager>().Character_PowFLG == false)
        {
            movestate = MoveState.Back;
        }

        //目標を見失ったら戻る
        if (nav.hasPath == false)
        {
            movestate = MoveState.Back;
        }
    }

    void Back()
    {
        Debug.Log("Back捕虜解放用キャラnav.pathStatus and nav.hasPath　Enemy");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);

        int r = Random.Range(0, enemy_returnpointList.Count);
        nav.SetDestination(enemy_returnpointList[r].transform.position);

        movestate = MoveState.End;
    }

    void End()
    {
        Debug.Log("End捕虜解放用キャラnav.pathStatus and nav.hasPath");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);
        Debug.Log(nav.remainingDistance);


        if (!nav.pathPending && nav.hasPath && nav.remainingDistance < 0.5f)
        {
            //ゴールについたら得点追加
            GameData.EnemyScore += 1;

            //ゴール地点に到達したら自分を消す
            GameData.NUMBER_OF_ENEMYS += 1;
            Destroy(this.gameObject);


        }


    }
}
