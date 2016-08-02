using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.LogicSystem;

public class TurnManager : MonoBehaviour {

    GameObject[] enemies;
    public static bool ourTurn;
    public Button turnButton;
	public static bool isSkillActive;
	public static string skillName;
	RuntimePlatform platform = Application.platform;

	// Use this for initialization
	void Start () {
        ourTurn = false;
		isSkillActive = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("Is Skill Active : " + isSkillActive);
		if (ourTurn) 
		{
			if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch(0).phase == TouchPhase.Began)
					{
						checkTouch(Input.GetTouch(0).position);
					}
				}
			}
			else if (platform == RuntimePlatform.WindowsEditor)
			{
				if (Input.GetMouseButtonUp(0))
				{
					checkTouch(Input.mousePosition);
				}
			}
		}
	}

    public void ChangeTurn()
    {
		ourTurn = false;
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<AttackController>().PerformAction();
        }
		ourTurn = true;
    }

	void checkTouch(Vector3 pos)
	{
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) 
		{
			if (isSkillActive) {
				switch (hit.transform.gameObject.tag) {

				case "Sheep":
					{
						if (skillName == "HealSkill")
							hit.transform.gameObject.GetComponent<HealthController> ().Heal (5);
						else
							hit.transform.gameObject.GetComponent<HealthController> ().DealDamage (10);
					}
					break;

				case "Enemy":
					{
						if (skillName == "HealSkill")
							hit.transform.gameObject.GetComponent<HealthController> ().Heal (5);
						else
							hit.transform.gameObject.GetComponent<HealthController> ().DealDamage (10);
					}
					break;
				default:
					{
						
					}
					break;
				}
			}
			else
				if (hit.transform.gameObject.tag == "Sheep") 
				{
					Events.Instance.DispatchEvent (hit.transform.gameObject.name, hit.transform.gameObject);
				}
		}
	}
}
