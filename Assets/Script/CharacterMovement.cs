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

    [SerializeField] float m_interval = 1.5f;
    float m_timer;

    bool backflg = false;

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

        //  CharacterMoveMent();
        m_timer += Time.deltaTime;


        if ( m_timer > m_interval )
        {
            m_timer = 0f;
            CharacterMoveMent();
           // SetRandomDestination();
        }
		
	}

    private void CharacterMoveMent()
    {

        if (backflg == false)
        {
            int m = Random.Range(0, movepointList.Count);
            nav.SetDestination(movepointList[m].transform.position);

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.AppendLine("NavMeshAgent.SetDestination Status.");
            builder.AppendLine("status if in: " + nav.pathStatus);
            builder.AppendLine("position: " + transform.position.ToString());
            Debug.Log(builder.ToString());
        }

        System.Text.StringBuilder builder1 = new System.Text.StringBuilder();
        builder1.AppendLine("NavMeshAgent.SetDestination Status.");
        builder1.AppendLine("status if out: " + nav.pathStatus);
        builder1.AppendLine("position: " + transform.position.ToString());
        Debug.Log(builder1.ToString());

        if (nav.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
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
}
