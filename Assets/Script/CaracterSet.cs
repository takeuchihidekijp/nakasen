using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaracterSet : MonoBehaviour {


    public Camera mainCamera;
    public Camera strategyCamera;

    public GameObject prefab;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        // マウスクリック
        if (Input.GetMouseButtonDown(0))
        {
            // マウス座標と指定カメラからの Rayを作成
            Ray ray = strategyCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ray と何か（オブジェクト）がヒットするか？
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                // 何かにヒットしたら if に入る

                if (hit.collider.gameObject.tag == "Floor")
                {
                    // フロアーにヒットしたらキャラを配置
                    Instantiate(prefab, hit.point + Vector3.up * 2.0f,prefab.transform.rotation);

                }
            }
        }

    }

}
