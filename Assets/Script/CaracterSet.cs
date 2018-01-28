using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterSet : MonoBehaviour {


    public bool zooming;
    public float zoomSpeed;
    public Camera camera;

    public GameObject Caracter;

    private Vector3 ClickPosition;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            Camera camera = GetComponent<Camera>();

            // Vector3でマウスがクリックした位置座標を取得する
            ClickPosition = Input.mousePosition;

            ClickPosition.z = 5f;

            Vector3 instancePos = camera.ScreenToWorldPoint(ClickPosition);
            //落ちないY座標を指定
            instancePos.y = 1;

            // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
            // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
            Instantiate(Caracter, instancePos, Caracter.transform.rotation);
        }

    }
}
