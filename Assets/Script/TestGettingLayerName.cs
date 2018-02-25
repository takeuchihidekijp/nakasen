using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGettingLayerName : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LogLayerName();
	}
	

    public void LogLayerName()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        GameObject[] goArray = GameObject.FindObjectsOfType<GameObject>();

        foreach (var go in goArray)
        {
            if(go.layer != LayerMask.NameToLayer("Default"))
            {
                string layerName = LayerMask.LayerToName(go.layer); // https://docs.unity3d.com/ScriptReference/LayerMask.LayerToName.html
                builder.AppendLine("Name: " + go.name + ", Layer: " + layerName);
            }
        }
        Debug.Log(builder.ToString());

    }
}
