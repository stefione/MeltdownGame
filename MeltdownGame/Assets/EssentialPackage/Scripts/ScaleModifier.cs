using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleModifier : MonoBehaviour
{
    CanvasScaler canvasScaler;
    private void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        float aspect = (float)Screen.width / (float)Screen.height;
        float refAspect = 1080f / 1920f;
        if (aspect < refAspect)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
        Canvas.ForceUpdateCanvases();
    }
}
