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
        Debug.Log("OnFound");
    }

    private void OnLost(GameObject i_lostObject)
    {
        Debug.Log("OnLost");
    }
}
