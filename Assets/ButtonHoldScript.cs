using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonHoldScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool isShown = false;
    private bool isBeingClicked = false;
    private Button MyButton;

    public Skill MySkill;
    public SkillDescription SkillDescription;


    public float RequestedTouchTime = 2f;
    private float timer = 0.0f;
    

    void Start()
    {
        MyButton = gameObject.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBeingClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingClicked = false;
        SkillDescription.gameObject.SetActive(false);
        isShown = false;
        timer = 0.0f;
        FightingSceneUIScript.DisableSkillCanvases();
    }
	
	// Update is called once per frame
	void Update () {
	    if(isBeingClicked)
        {
            timer += Time.deltaTime;
            if(timer > RequestedTouchTime)
            {
                if(!isShown)
                {
                    isShown = true;
                    MyButton.onClick.RemoveAllListeners();
                    SkillDescription.gameObject.SetActive(true);
                    SkillDescription.LoadSkillData(MySkill);
                    Debug.Log("Hey, I was held for more than " +    RequestedTouchTime);
                }
            }
        }
	}

}
