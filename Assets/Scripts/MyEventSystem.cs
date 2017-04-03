using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyEventSystem : MonoBehaviour {
    EventSystem es;

    public GameObject selectedText;

	// Use this for initialization
	void Start () {
        es.SetSelectedGameObject(selectedText);
	}
}
