using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCharacterChange : MonoBehaviour {

    Toggle toggle;

    public bool normal_flg = true;

	// Use this for initialization
	void Start () {
        toggle = GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeToggle()
    {
        Debug.Log("Toggleが変更されました");

        if(this.toggle.isOn == true)
        {
            normal_flg = true;
            Debug.Log("Toggleが変更されました normal_flg = true");
        }
        else
        {
            normal_flg = false;
            Debug.Log("Toggleが変更されました normal_flg = false");
        }
    }
}
