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
                nav.SetDestination(powcreate.GetComponent<GameManager>().enemypows[powcreate.GetComponent<GameManager>().enemypows.Count - 1].transform.position);

                return;
            }
            else
            {
                int m = Random.Range(0, movepointList.Count);
                nav.SetDestination(movepointList[m].transform.position);
            }

            movestate = MoveState.Forward;
        }

    }

    void Forward()
    {

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

}
