using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

    Vector3 movement;
    Rigidbody characterRigidbody;

    UnityEngine.AI.NavMeshAgent nav;

    //移動ポイントのオブジェクト用List（キャラクターの移動ポイントをMovePointとする）
    private int MovePointCount = 5;
    private List<GameObject> movepointList = new List<GameObject>();

    private int ReturnPointCount = 5;
    private List<GameObject> returnpointList = new List<GameObject>();

    //Character用スコアを表示するテキスト
    private GameObject chara_scoreText;

    //Character用使えるメンバを表示するテキスト
    private GameObject chara_memberText;

    //Characterの保持している捕虜の数を表示するテキスト
    private GameObject chara_powText;


    //Game終了時に表示するテキスト（You Win or You Lose）
    private GameObject stateText;

    //GameのTimerを表示するテキスト
    private GameObject timerText;

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


        this.chara_scoreText = GameObject.Find("CharaScoreText");

        this.chara_memberText = GameObject.Find("CharaMemberText");

        this.chara_powText = GameObject.Find("CharaPowText");


        this.stateText = GameObject.Find("GameResultText");

        this.timerText = GameObject.Find("TimeText");


    }
	
	// Update is called once per frame
	void Update () {

        //Timerを減らす
        GameData.TotalTime -= Time.deltaTime;

        //Timerを表示
        this.timerText.GetComponent<Text>().text = "Time" + GameData.TotalTime + "s";

        //Scoreを表示
        this.chara_scoreText.GetComponent<Text>().text = "Score:" + GameData.CharacterScore;

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

            //仮2018
         //   nav.speed = Random.Range(0.1f, 10.0f);
            //仮2018

            movestate = MoveState.Forward;
        }

    }

    void Forward()
    {


        //  if(transform.GetComponentInChildren<Finder>().chargeflg == true)
        if (transform.GetComponentInChildren<Finder>().chargeflg == true && transform.GetComponentInChildren<Finder>().target != null)
       {
            Debug.Log("movestate = MoveState.Chase");
            movestate = MoveState.Chase;


        }

        //逃げる
        else if(transform.GetComponentInChildren<Finder>().escapeflg == true && transform.GetComponentInChildren<Finder>().target != null)
        {
            Debug.Log("movestate = MoveState.Escape");
            movestate = MoveState.Escape;
        }

        // 目的地についたら戻り先を決める

        else if ((nav.hasPath && nav.remainingDistance < 0.5f)  || (backflg == true))
        {

            //中線を踏んだら得点追加
            GameData.CharacterScore += 1;

            movestate = MoveState.Back;

        }
        //どれにも当てはまらず、行き先が不明な場合
        else if(nav.hasPath == false){
            int m = Random.Range(0, movepointList.Count);
            nav.SetDestination(movepointList[m].transform.position);
        }


    }

    //追いかけるロジック
    void Chase()
    {
        // ||(this.transform.position.z >= 5)

        if (transform.GetComponentInChildren<Finder>().target == null)
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
        if(transform.GetComponentInChildren<Finder>().escape_down_flg == true)
        {
            Escape_down();
        }else if (transform.GetComponentInChildren<Finder>().escape_up_flg == true)
        {
            Escape_up();
        }else if (transform.GetComponentInChildren<Finder>().escape_right_flg == true)
        {
            Escape_right();
        }else if (transform.GetComponentInChildren<Finder>().escape_left_flg == true)
        {
            Escape_left();
        }
    }

    void Back()
    {

        int r = Random.Range(0, returnpointList.Count);
        nav.SetDestination(returnpointList[r].transform.position);

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

    void Escape_down()
    {
        Debug.Log("Chara Escape_down!");
        //仮
        movestate = MoveState.Forward;
    }

    void Escape_up()
    {
        Debug.Log("Chara Escape_up!");
        //仮
        movestate = MoveState.Forward;
    }

    void Escape_right()
    {
        Debug.Log("Chara Escape_right!");
        //仮
        movestate = MoveState.Forward;
    }

    void Escape_left()
    {
        Debug.Log("Chara Escape_left!");
        //仮
        movestate = MoveState.Forward;
    }
}
