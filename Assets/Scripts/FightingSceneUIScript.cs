using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.LogicSystem;
using System.Collections.Generic;

public class FightingSceneUIScript : MonoBehaviour {

    public GameObject[] SkillIconsPrefabs;


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
			Events.Instance.RegisterForEvent(sheep.name, CreateSkillButtons);
        }
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
		Debug.Log ("Length " + SkillIconsPrefabs.Length);
		for (int i = 0; i < SkillIconsPrefabs.Length; i++)
			CreateBubble(touchPos, SkillIconsPrefabs[i], i, sheep);
	}

	void CreateBubble(Vector2 touchPos, GameObject prefab, int ordinal, GameObject sheep)
    {
		prefabRectTransform = prefab.GetComponent<RectTransform>();
		float ratioX = containerRectTransform.rect.width / Camera.main.pixelWidth;
		float ratioY = containerRectTransform.rect.height / Camera.main.pixelHeight;
		float radius = 100f;
		float angle = -Mathf.PI * 3 / 18;

        //create a new item, name it, and set the parent
		GameObject newItem = Instantiate(prefab) as GameObject;
		newItem.name = prefab.name;
        newItem.transform.parent = gameObject.transform;

		newItem.GetComponent<Button> ().onClick.AddListener (() => {
			TurnManager.isSkillActive = true;
			TurnManager.skillName = prefab.name;
		});

        //move and size the new item
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
		float x = ratioX * (touchPos.x + Mathf.Cos((ordinal-1) * angle)*radius);
		float y = ratioY * (touchPos.y + Mathf.Sin((ordinal-1) * angle)*radius);
        rectTransform.offsetMin = new Vector2(x, y);

		x = rectTransform.offsetMin.x + prefabRectTransform.rect.width * ratioX * 0.5f;
		y = rectTransform.offsetMin.y + prefabRectTransform.rect.height * ratioX * 0.5f;
        rectTransform.offsetMax = new Vector2(x, y);
    }
		
}
