using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPowRedeemem : MonoBehaviour {


    Vector3 movement;
    Rigidbody characterRigidbody;

    UnityEngine.AI.NavMeshAgent nav;

    private int MovePointCount = 5;
    private List<GameObject> movepointList = new List<GameObject>();

    private int ReturnPointCount = 5;
    private List<GameObject> returnpointList = new List<GameObject>();


   [SerializeField] float m_interval = 1.5f;

    float m_timer;

    bool backflg = false;

    //スタミナ用のタイマー
    float s_timer = 10.0f;

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

            if (movepoint != null)
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

        //捕虜解放者は捕虜めがけて突っ込むだけで追いかけたり逃げたりしない。

        //スタミナに応じて速度を調整する
        s_timer -= Time.deltaTime;
        if (s_timer > 6)
        {
            nav.speed = s_timer;
        }
        else
        {
            nav.speed = 3.0f;
        }
        //ランダム廃止
        //   nav.speed = Random.Range(0.1f, 10.0f);

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
        Debug.Log("捕虜解放用キャラが何かとぶつかった？");
        Debug.Log(collision.gameObject);
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Pow")
        {
            Debug.Log("捕虜解放");

            GameObject powredeem = GameObject.Find("PowCreate");

            powredeem.GetComponent<GameManager>().RedeemPowCharacter();

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

        Debug.Log(" Stay Timer 捕虜解放用キャラ");
        Debug.Log(m_timer);
        Debug.Log(m_interval);

        if (m_timer > m_interval)
        {
            Debug.Log(" Stay2 捕虜解放用キャラnav.pathStatus and nav.hasPath");
            Debug.Log(nav.pathStatus);
            Debug.Log(nav.hasPath);

            m_timer = 0f;
            // 行き先を決める
            GameObject powcreate = GameObject.Find("PowCreate");

            Debug.Log(" Stay3 捕虜解放用キャラ powcreate.GetComponent<GameManager>().Character_PowFLG");
            Debug.Log(powcreate.GetComponent<GameManager>().Enemy_PowFLG);
            Debug.Log(nav.hasPath);

            //つかまっているキャラがいればそこを目指す　そうでなければ通常の移動ポイントへ
            if (powcreate.GetComponent<GameManager>().Enemy_PowFLG == true)
            {
                nav.SetDestination(powcreate.GetComponent<GameManager>().enemypows[powcreate.GetComponent<GameManager>().enemypows.Count - 1].transform.position);

                movestate = MoveState.Forward;

                return;
            }
            else if(nav.hasPath == false)
            {
                int m = Random.Range(0, movepointList.Count);
                nav.SetDestination(movepointList[m].transform.position);

                Debug.Log(" Stay4 捕虜解放用キャラ powcreate.GetComponent<GameManager>().Character_PowFLG");
                Debug.Log(m);
                Debug.Log(nav.pathStatus);
                Debug.Log(nav.hasPath);
            }

            movestate = MoveState.Forward;
        }

        Debug.Log(" Stay 捕虜解放用キャラnav.pathStatus and nav.hasPath");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);

    }

    void Forward()
    {
        Debug.Log("捕虜解放用キャラnav.pathStatus and nav.hasPath");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);

        GameObject powcreate = GameObject.Find("PowCreate");

        if (powcreate.GetComponent<GameManager>().Enemy_PowFLG == false && (backflg == true))
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

        Debug.Log("Back1捕虜解放用キャラnav.pathStatus and nav.hasPath");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);

        int r = Random.Range(0, returnpointList.Count);
        nav.SetDestination(returnpointList[r].transform.position);

        Debug.Log("Back1.5捕虜解放用キャラnav.pathStatus and nav.hasPath");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);
        Debug.Log(nav.pathPending);
        Debug.Log(nav.remainingDistance);
        Debug.Log(returnpointList[r].transform.position);

        movestate = MoveState.End;

        //!nav.pathPendingは試しに実装
        //今までのコードだとBackが複数呼ばれる間に「int r = Random.Range(0, returnpointList.Count);」が何度も呼ばれ戻り値が確定しないので
        //以下のように距離がちじまったらステータスを変更するのではなく戻るポイントを設定したらEndにもっていくように設定。
        //   if (!nav.pathPending && nav.hasPath && nav.remainingDistance < 0.5f)
        //   {

        //上記で設定したnav.SetDestinationが効かずに以前の設定が反映されてしまっておりnav.remainingDistanceの条件にすぐに入ってしまって
        //すぐにEndになってしまう。解放したばかりだと当然距離は近いので。
        //対応を検討


        //ゴールについたら得点追加
        //    GameData.CharacterScore += 1;

        //  movestate = MoveState.End;

        // }
    }

    void End()
    {
        Debug.Log("End捕虜解放用キャラnav.pathStatus and nav.hasPath");
        Debug.Log(nav.pathStatus);
        Debug.Log(nav.hasPath);
        Debug.Log(nav.remainingDistance);

        if (!nav.pathPending && nav.hasPath && nav.remainingDistance < 0.5f)
        {
            GameData.CharacterScore += 1;
            //ゴール地点に到達したら自分を消す
            GameData.NUMBER_OF_CHARACTERS += 1;
            Destroy(this.gameObject);
        }


        //ゴール地点に到達したら自分を消す
    //    GameData.NUMBER_OF_CHARACTERS += 1;
    //    Destroy(this.gameObject);

    }

}
