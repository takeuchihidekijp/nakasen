using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //敵の捕虜リスト
    private List<GameObject> enemypows = new List<GameObject>();

    public GameObject CharacterPowPrefab;

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

        if (collision.gameObject.tag == "Character" && this.transform.gameObject.layer != collision.gameObject.layer)
        {
            Debug.Log("Found: ぶつかった");

            if (this.transform.GetComponent<GenerateManager>().MyNumber > collision.gameObject.GetComponent<GenerateManager>().MyNumber)
            {
                //味方の方が強かったら捕虜追加
                AddPow();

                Debug.Log("Found: Charge");

                Destroy(collision.gameObject);
            }
        }
    }

    public void AddPow()
    {
        var ins = Instantiate(CharacterPowPrefab);

        enemypows.Add(ins);

        Debug.Log("AddPow_Enemy");
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

        if (transform.GetComponentInChildren<Finder>().chargeflg == true)
        {
            Debug.Log("EnemyMovement: Charge");

            //  nav.SetDestination(transform.GetComponentInChildren<Finder>().);
            nav.SetDestination(transform.GetComponentInChildren<Finder>().gameObject.transform.position);

        }
        // 目的地についたら戻り先を決める

        if ((nav.hasPath && nav.remainingDistance < 0.5f) || (backflg == true))
        {

            int r = Random.Range(0, enemy_returnpointList.Count);
            nav.SetDestination(enemy_returnpointList[r].transform.position);

            movestate = MoveState.Back;

        }


    }

    //追いかけるロジック
    void Chase()
    {

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
