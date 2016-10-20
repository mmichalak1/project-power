using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangeImageColor : MonoBehaviour {

    public Color[] Colors;

    private Image MyImage;

    public void ChangeColor(int number)
    {
        MyImage.color = Colors[number];
    }

	// Use this for initialization
	void Start () {
        MyImage = gameObject.GetComponent<Image>();
	}
}
