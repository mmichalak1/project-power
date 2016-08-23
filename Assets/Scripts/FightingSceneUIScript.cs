﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour {

    public GameObject[] SkillIconsPrefabs;
    public Text TextLabel;

    private RectTransform containerRectTransform;
    private RectTransform prefabRectTransform;

    private List<GameObject> sheeps;


	// Use this for initialization
	void Start () {
		
        containerRectTransform = gameObject.GetComponent<RectTransform>();
        sheeps = new List<GameObject>();

        sheeps.AddRange(GameObject.FindGameObjectsWithTag("Sheep"));

        foreach(GameObject sheep in sheeps)
        {
			Events.Instance.RegisterForEvent(sheep.name + "skill", CreateSkillButtons);
        }

        Events.Instance.RegisterForEvent("SetText", SetText);
	}
	
	// Update is called once per frame
	void Update () {


	}

	void CreateSkillButtons(object obj)
	{
		List<GameObject> UIElementsToDestroy = new List<GameObject>();

		UIElementsToDestroy.AddRange(GameObject.FindGameObjectsWithTag("Bubble"));

		foreach (GameObject X in UIElementsToDestroy)
		{
			Debug.Log("Destroy " + X.name);
			Destroy(X);
		}

		GameObject sheep = obj as GameObject;
		Vector3 wp = Camera.main.WorldToScreenPoint(sheep.transform.position);
		Vector2 touchPos = new Vector2(wp.x, wp.y);

		for (int i = 0; i < SkillIconsPrefabs.Length; i++)
			CreateBubble(touchPos, SkillIconsPrefabs[i], i, sheep);
	}

	void CreateBubble(Vector2 touchPos, GameObject prefab, int ordinal, GameObject sheep)
    {
		prefabRectTransform = prefab.GetComponent<RectTransform>();
		float ratioX = containerRectTransform.rect.width / Camera.main.pixelWidth;
		float ratioY = containerRectTransform.rect.height / Camera.main.pixelHeight;
        float radius = 100 * Screen.currentResolution.width / containerRectTransform.rect.width;
		float angle = -Mathf.PI * 3 / 18;

        //create a new item, name it, and set the parent
		GameObject newItem = Instantiate(prefab) as GameObject;
		newItem.name = prefab.name;
        newItem.transform.parent = gameObject.transform;

		newItem.GetComponent<Button> ().onClick.AddListener (() => {
			TurnManager.isSkillActive = true;
			TurnManager.skillName = prefab.name;
            Debug.Log("Active skill: " + prefab.name);
		});

        //move and size the new item
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
		float x =  (touchPos.x + Mathf.Cos((ordinal-1) * angle)*radius);
		float y =  (touchPos.y + Mathf.Sin((ordinal-1) * angle)*radius);

        rectTransform.position = new Vector3(x, y, 0);

    }


    public void Cancel()
    {
        TurnManager.isSkillActive = false;
    }

    void SetText(object text)
    {
        TextLabel.text = (string)text;
    }
		
}
