using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    Vector3 movement;
    Rigidbody characterRigidbody;

    UnityEngine.AI.NavMeshAgent nav;

    //移動ポイントのオブジェクト用List（キャラクターの移動ポイントをMovePointとする）
    private int MovePointCount = 5;
    private List<GameObject> movepointList = new List<GameObject>();

    private int ReturnPointCount = 5;
    private List<GameObject> returnpointList = new List<GameObject>();

    //味方の捕虜リスト
    private List<GameObject> charapows = new List<GameObject>();

    public GameObject EnemyPowPrefab;


    [SerializeField] float m_interval = 1.5f;
    float m_timer;

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

        for (int i = 1; i <= MovePointCount; ++i)
        {
            GameObject movepoint = GameObject.Find("MovePoint" + i.ToString());

            if(movepoint != null)
            {
                movepointList.Add(movepoint);
            }
        }

        for (int i = 1; i <= ReturnPointCount; ++i)
        {
            GameObject returnpoint = GameObject.Find("ReturnPoint" + i.ToString());

            if (returnpoint != null)
            {
                returnpointList.Add(returnpoint);
            }
        }

        characterRigidbody = GetComponent<Rigidbody>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {

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


        //  CharacterMoveMent();
        //    m_timer += Time.deltaTime;


        //    if ( m_timer > m_interval )
        //   {
        //   m_timer = 0f;
        //    CharacterMoveMent();
        // SetRandomDestination();
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Line")
        {
            backflg = true;
        }

   //     if (collision.gameObject.tag == "Character" && this.transform.gameObject.layer != collision.gameObject.layer)
   //     {
   //         Debug.Log("Found: ぶつかった");

   //         if (this.transform.GetComponent<GenerateManager>().MyNumber > collision.gameObject.GetComponent<GenerateManager>().MyNumber)
   //         {
                //味方の方が強かったら捕虜追加
   //             AddPow();

    //            Debug.Log("Found: Charge");

    //            Destroy(collision.gameObject);
    //        }
    //    }
    }

    public void AddPow()
    {
        var ins = Instantiate(EnemyPowPrefab);

        charapows.Add(ins);

        Debug.Log("AddPow");
    }

    private void CharacterMoveMent()
    {

        if (backflg == false)
        {
            int m = Random.Range(0, movepointList.Count);
            nav.SetDestination(movepointList[m].transform.position);
        }

        //if (nav.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        //if (!nav.pathPending && !nav.hasPath)
        //if (nav.hasPath && nav.remainingDistance < 0.5f)
        if (nav.hasPath && nav.remainingDistance < 0.5f)
        {
            backflg = true;

        }

        if (backflg == true)
        {
            int r = Random.Range(0, returnpointList.Count);
            nav.SetDestination(returnpointList[r].transform.position);
        }


    }

    void SetRandomDestination()
    {
        nav.SetDestination(new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-10.0f, 10.0f)));
    }

    void Stay()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0f;
            // 行き先を決める
            int m = Random.Range(0, movepointList.Count);
            nav.SetDestination(movepointList[m].transform.position);

            movestate = MoveState.Forward;
        }

    }

    void Forward()
    {


        //  if(transform.GetComponentInChildren<Finder>().chargeflg == true)
        if (transform.GetComponentInChildren<Finder>().chargeflg == true && transform.GetComponentInChildren<Finder>().target != null)
       {
            Debug.Log("CharacterMovement: Charge");

            nav.SetDestination(transform.GetComponentInChildren<Finder>().target.transform.position);

            //つかまえたら捕虜にする
            if (transform.GetComponentInChildren<Finder>().chachedflg == true)
            {
             //   AddPow();
            }

        }

        // 目的地についたら戻り先を決める

        if ((nav.hasPath && nav.remainingDistance < 0.5f)  || (backflg == true))
        {

            int r = Random.Range(0, returnpointList.Count);
            nav.SetDestination(returnpointList[r].transform.position);

            movestate = MoveState.Back;

        }


    }

    //追いかけるロジック
    void Chase()
    {
        // ||(this.transform.position.z >= 5)
    }

    //逃げるロジック
    void Escape()
    {

    }

    void Back()
    {
        if (nav.hasPath && nav.remainingDistance < 0.5f)
        {

            movestate = MoveState.End;

        }
    }

    void End()
    {
        //ゴール地点に到達したら自分を消す
        Destroy(this.gameObject);
        
    }
}
