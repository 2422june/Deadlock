using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficLightController : MonoBehaviour
{
    Color on, off;
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        on = new Color(0, 255, 163);
        off = new Color(0, 0, 0);
    }

    public void OnLight()
    {
        image.color = on;
    }

    public void OffLight()
    {
        image.color = off;
    }
}
