using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Character[] characters = FindObjectsOfType<Character>();

        float dist = 9999f;
        foreach (var character in characters)
        {
            if (character.tag == "Enemy")
            {
                Vector3 d = this.transform.position - character.transform.position;
                if (dist > d.magnitude)
                {
                    dist = d.magnitude;
                }
            }
        }

        if (dist < 10f)
        {
            // 追いかけるなど
        }
    }
}
