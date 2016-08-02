﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour {

    public GameObject UIButtonPrefab;

    private RectTransform containerRectTransform;
    private RectTransform prefabRectTransform;
    private List<GameObject> sheeps;

	// Use this for initialization
	void Start () {
	
        prefabRectTransform = UIButtonPrefab.GetComponent<RectTransform>();
        containerRectTransform = gameObject.GetComponent<RectTransform>();
        sheeps = new List<GameObject>();

        sheeps.AddRange(GameObject.FindGameObjectsWithTag("Sheep"));

        foreach(GameObject sheep in sheeps)
        {
            Events.Instance.RegisterForEvent(sheep.name, CreateBubble);
        }
	}
	
    void CreateBubble(object pair)
    {

        Vector2 touchPos = ((KeyValuePair<Vector2, Transform>)pair).Key;
        Transform trans= ((KeyValuePair<Vector2, Transform>)pair).Value;


        List<GameObject> UIElementsToDestroy = new List<GameObject>();

        UIElementsToDestroy.AddRange(GameObject.FindGameObjectsWithTag("Bubble"));

        foreach (GameObject obj in UIElementsToDestroy)
        {
            //Debug.Log("Destroy " + obj.name);
            Destroy(obj);
        }
        //create a new item, name it, and set the parent
        GameObject newItem = Instantiate(UIButtonPrefab) as GameObject;
        newItem.name = trans.name + " Bubble ";
        newItem.transform.parent = gameObject.transform;

        //move and size the new item
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        float x = touchPos.x * containerRectTransform.rect.width / Camera.main.pixelWidth * 1.15f;
        float y = touchPos.y * containerRectTransform.rect.height / Camera.main.pixelHeight * 1.15f;
        rectTransform.offsetMin = new Vector2(x, y);

        //Debug.Log(containerRectTransform.rect.width + "   " + containerRectTransform.rect.height);

        x = rectTransform.offsetMin.x + prefabRectTransform.rect.width * 0.25f;
        y = rectTransform.offsetMin.y + prefabRectTransform.rect.height * 0.25f;
        rectTransform.offsetMax = new Vector2(x, y);
    }
}
