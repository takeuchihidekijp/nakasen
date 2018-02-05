using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour {

    private GameObject MainCam;

    //座標ログ
    private List<Vector3> CharacterPositionLog = new List<Vector3>();

    // Use this for initialization
    void Start () {

        MainCam = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = GetComponent<Camera>();

        // フローアにいる全キャラクターを取得
        Character[] characters = FindObjectsOfType<Character>();

        //全キャラクター分ループ

        float maxz = 0;
        foreach (var character in characters ) {
            float z = character.transform.position.z - camera.transform.position.z;
            if (maxz < z)
            {
                maxz = z;
            }
        }
        camera.farClipPlane = 10f + maxz;
    }
}
