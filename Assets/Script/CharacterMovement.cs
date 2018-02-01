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

        characterRigidbody = GetComponent<Rigidbody>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {

        CharacterMoveMent();
		
	}

    private void CharacterMoveMent()
    {
        int r = Random.Range(0, movepointList.Count);

        nav.SetDestination(movepointList[r].transform.position);
    }
}
