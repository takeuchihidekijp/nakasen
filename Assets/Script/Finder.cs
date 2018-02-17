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

        Debug.Log(GenerateManager.MyNumber);

        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("OnFound");
        builder.AppendLine("myname: " + this.transform.root.gameObject.name);
        builder.AppendLine("name: " + transform.root.gameObject.name);
        builder.AppendLine("position: " + transform.position.ToString());
        builder.AppendLine("other_number: " + GenerateManager.MyNumber);
        builder.AppendLine("target: " + i_foundObject);
        Debug.Log(builder.ToString());
    //    Debug.Log("OnFound");
    }

    private void OnLost(GameObject i_lostObject)
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.AppendLine("OnLost");
        builder.AppendLine("myname: " + this.transform.root.gameObject.name);
        builder.AppendLine("name: " + transform.root.gameObject.name);
        builder.AppendLine("position: " + transform.position.ToString());
        builder.AppendLine("target: " + i_lostObject);
        Debug.Log(builder.ToString());
     //   Debug.Log("OnLost");
    }
}
