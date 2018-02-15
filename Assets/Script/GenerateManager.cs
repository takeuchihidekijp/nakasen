using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateManager : MonoBehaviour {

    public List<GameObject> generateList = new List<GameObject>();

    public int MyNumber;

    // Use this for initialization
    void Start () {

        GameData.NUMBER_OF_GENERATE = GameData.NUMBER_OF_GENERATE + 1;

        MyNumber = GameData.NUMBER_OF_GENERATE;

     //   generateList.Add(MyNumber);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
