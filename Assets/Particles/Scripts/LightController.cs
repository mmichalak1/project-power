using UnityEngine;
using System.Collections.Generic;

public class LightController : MonoBehaviour {

    public Light Light;
    public List<float> times;
    public float FadeRatio;
    private int index = 0;
    private float deltaTime = 0;

	void Update () {
        deltaTime += Time.deltaTime;

	    if(index < times.Count &&  times[index] <= deltaTime)
        {
            index++;
            Light.intensity = 5;
        }

        Fade();
	}

    void Fade()
    {
        if(Light.intensity > 0)
        {
            Light.intensity -= Time.deltaTime * FadeRatio;
            if (Light.intensity < 0)
                Light.intensity = 0;
        }
    }
}
