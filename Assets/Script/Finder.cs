using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finder : MonoBehaviour {

    private List<GameObject> m_target = new List<GameObject>();

    private void Awake()
    {

        var searching = GetComponentInChildren<SearchingBehavior>();
        searching.onFound += OnFound;
        searching.onLost += OnLost;
    }

    private void OnFound (GameObject i_foundObject)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("OnFound");
        builder.AppendLine("name: " + gameObject.name);
        builder.AppendLine("position: " + transform.position.ToString());
        Debug.Log(builder.ToString());
    //    Debug.Log("OnFound");
    }

    private void OnLost(GameObject i_lostObject)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("OnLost");
        builder.AppendLine("name: " + gameObject.name);
        builder.AppendLine("position: " + transform.position.ToString());
        Debug.Log(builder.ToString());
     //   Debug.Log("OnLost");
    }
}
