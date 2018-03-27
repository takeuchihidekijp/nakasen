using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelect : MonoBehaviour {

    private GameObject MainCam;
    private GameObject StCam;

	// Use this for initialization
	void Start () {

        MainCam = GameObject.Find("Main Camera");
        StCam = GameObject.Find("StorategyCamera");

        StCam.SetActive(false);
        MainCam.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {

        //戦略シーンはいったん外す
        //if (Input.GetKeyDown("space"))
        //{
        //if (MainCam.activeSelf)
        //{
        //MainCam.SetActive(false);
        //StCam.SetActive(true);
        //}
        //      else
        //{
        //MainCam.SetActive(true);
        //StCam.SetActive(false);
        //}
        //}
    }


}
