using UnityEngine;
using System.Collections;

public class DisplayHealth : MonoBehaviour
{

    [SerializeField]
    private HealthController _controller;

    private UnityEngine.UI.Image _image;

    void Start()
    {
        _image = gameObject.GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller != null)
            _image.fillAmount = (float)_controller.CurrentHealth / _controller.MaxHealth;
    }
}
