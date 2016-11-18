using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class FadeInAndOut : MonoBehaviour {

    static Color IncerentalColor = new Color(0, 0, 0, 10);

    public float Speed = 1.0f;

    private UnityEngine.UI.Image Image;
    private State state = State.StandBy;


	void Start () {
        Image = GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(state != State.StandBy)
        {
            if (state == State.FadingIn)
            {
                Image.color += IncerentalColor * Time.deltaTime * Speed;
                if (Image.color.a > 1.0f)
                    state = State.FadingOut;
            }
            else
            {
                Image.color -= IncerentalColor * Time.deltaTime * Speed;
                if (Image.color.a < 0.0f)
                    state = State.StandBy;
            }
                
        }
	}

    public void Play()
    {
        if(state ==  State.StandBy)
            state = State.FadingIn;
    }

    private enum State
    {
        FadingIn,
        FadingOut,
        StandBy
    }
}
