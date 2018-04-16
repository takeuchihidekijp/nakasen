using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {

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


        //敵がキャラクターを特定の位置まで最後まで追いかけないようにする
        //if (this.transform.position.z < -9)
        if (this.transform.position.z < GameData.EnemyChaseLimitZ)
        {
            movestate = MoveState.Back;
        }


        switch (movestate)
        {

            case MoveState.Stay:
                Stay();
                break;
            case MoveState.Forward:
                Forward();
                break;
            case MoveState.Chase:
                Chase();
                break;
            case MoveState.Escape:
                Escape();
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
        if (collision.gameObject.tag == "Line")
        {
            backflg = true;
        }

        //ぶつからないので廃止方向。
        if (collision.gameObject.tag == "Character" && this.transform.gameObject.layer != collision.gameObject.layer)
        {
         Debug.Log("敵の方！Found: ぶつかった");

        // if (this.transform.GetComponent<GenerateManager>().MyNumber > collision.gameObject.GetComponent<GenerateManager>().MyNumber)
        // {
        //味方の方が強かったら捕虜追加
        // AddPow();

        // Debug.Log("Found: Charge");

        // Destroy(collision.gameObject);
        // }
        }
    }


    void SetRandomDestination()
    {
        //   nav.SetDestination(new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(0.0f, 10.0f)));
        if (!nav.SetDestination(new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(0.0f, 10.0f))))
        {
            // SetDestination() に失敗した時は情報を出す
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.AppendLine("NavMeshAgent.SetDestination failed.");
            builder.AppendLine("name: " + gameObject.name);
            builder.AppendLine("position: " + transform.position.ToString());
            Debug.LogError(builder.ToString());
        }
    }

    void Stay()
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

        if (transform.GetComponentInChildren<Finder>().chargeflg == true && transform.GetComponentInChildren<Finder>().target != null)
        {
            Debug.Log("enemy movestate = MoveState.Chase");
            movestate = MoveState.Chase;


        }

        //逃げる
        else if (transform.GetComponentInChildren<Finder>().escapeflg == true && transform.GetComponentInChildren<Finder>().target != null)
        {
            Debug.Log("enemy movestate = MoveState.Escape");
            movestate = MoveState.Escape;
        }

        // 目的地についたら戻り先を決める

        else if ((nav.hasPath && nav.remainingDistance < 0.5f) || (backflg == true))
        {
            //目的地についたら得点追加
            GameData.EnemyScore += 1;
            movestate = MoveState.Back;

        }
        //どれにも当てはまらず、行き先が不明な場合
        else if (nav.hasPath == false)
        {
            int m = Random.Range(0, enemy_movepointList.Count);
            nav.SetDestination(enemy_movepointList[m].transform.position);
        }


    }

    //追いかけるロジック
    void Chase()
    {
        if(transform.GetComponentInChildren<Finder>().target == null)
        {
            movestate = MoveState.Stay;
            return;
        }

        nav.SetDestination(transform.GetComponentInChildren<Finder>().target.transform.position);

        if (transform.GetComponentInChildren<Finder>().chachedflg == true)
        {

            //捕まえたらステータスをForwardに戻す
            movestate = MoveState.Forward;
        }
    }

    //逃げるロジック
    void Escape()
    {
        if (transform.GetComponentInChildren<Finder>().escape_down_flg == true)
        {
            Escape_down();
        }
        else if (transform.GetComponentInChildren<Finder>().escape_up_flg == true)
        {
            Escape_up();
        }
        else if (transform.GetComponentInChildren<Finder>().escape_right_flg == true)
        {
            Escape_right();
        }
        else if (transform.GetComponentInChildren<Finder>().escape_left_flg == true)
        {
            Escape_left();
        }
    }

    void Back()
    {
        int r = Random.Range(0, enemy_returnpointList.Count);
        nav.SetDestination(enemy_returnpointList[r].transform.position);

        movestate = MoveState.End;


    }

    void End()
    {
        Debug.Log("Enemy End()");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);
        Debug.Log(nav.pathPending);
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

    void Escape_down()
    {
        //戻るフラグがオンの場合は位置を+1(敵はキャラクターと向きが逆なので)したうえで陣地まで下がらせる
        if (backflg == true)
        {
            Debug.Log("Enemy Escape_down!");
            this.transform.Translate(0, 0, 0.5f);
            movestate = MoveState.Back;
        }
        else
        {
            //仮
            movestate = MoveState.Forward;
        }

    }

    void Escape_up()
    {
        //戻るフラグがオンの場合は位置を-1(敵はキャラクターと向きが逆なので)したうえで陣地まで下がらせる
        if (backflg == true)
        {
            Debug.Log("Enemy Escape_up!");
            this.transform.Translate(0, 0, -0.5f);
            movestate = MoveState.Back;
        }
        else
        {
            //仮
            movestate = MoveState.Forward;
        }
    }

    void Escape_right()
    {
        Debug.Log("Enemy Escape_right!");
        //仮(Characterと向きが逆なので注意)仮のロジック
        if (this.transform.position.x > GameData.StageChaseLimitLeftx)
        {
            this.transform.Translate(-0.5f, 0, 0);
        }

        movestate = MoveState.Forward;
    }

    void Escape_left()
    {
        Debug.Log("Enemy Escape_left!");
        //仮(Characterと向きが逆なので注意)仮のロジック
        if (this.transform.position.x < GameData.StageChaseLimitRightx)
        {
            this.transform.Translate(0.5f, 0, 0);
        }

        movestate = MoveState.Forward;
    }

}
