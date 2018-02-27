using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //味方の捕虜リスト
    private List<GameObject> charapows = new List<GameObject>();
    public GameObject EnemyPowPrefab;

    //敵の捕虜リスト
    private List<GameObject> enemypows = new List<GameObject>();
    public GameObject CharacterPowPrefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPow_Character()
    {
        var ins = Instantiate(EnemyPowPrefab, new Vector3(-4, 2, charapows.Count - 10), Quaternion.identity);

        charapows.Add(ins);

    }

    public void AddPow_Enemy()
    {
        var ins = Instantiate(CharacterPowPrefab, new Vector3(4, 2, enemypows.Count + 10), Quaternion.identity);

        enemypows.Add(ins);

    }
}
