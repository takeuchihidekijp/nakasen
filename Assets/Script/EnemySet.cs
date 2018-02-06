using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySet : MonoBehaviour {

    public GameObject prefab;

    [SerializeField] float m_interval = 15.0f;
    float m_timer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        m_timer += Time.deltaTime;

        if (m_timer > m_interval)
        {
            m_timer = 0f;
            CreateEnemy();
        }

    }


    void CreateEnemy()
    {
        Vector3 pos = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(10.0f, 20.0f));

        Instantiate(prefab, pos, Quaternion.identity);
    }

}
