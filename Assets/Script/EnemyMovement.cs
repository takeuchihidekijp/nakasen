using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Vector3 movement;
    Rigidbody characterRigidbody;

    UnityEngine.AI.NavMeshAgent nav;

    [SerializeField] float m_interval = 1.5f;
    float m_timer;

    // Use this for initialization
    void Start () {
        characterRigidbody = GetComponent<Rigidbody>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        //  CharacterMoveMent();
        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0f;
            SetRandomDestination();
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
}
