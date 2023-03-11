using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLayout : MonoBehaviour,ISingleton
{
    public RuntimePlatform RuntimePlatform;

    public void Initialize()
    {
#if UNITY_EDITOR
#if UNITY_IOS
        if (RuntimePlatform == RuntimePlatform.IPhonePlayer)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
#elif UNITY_ANDROID
        if (RuntimePlatform == RuntimePlatform.Android)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
#endif
        return;
#endif
        if (RuntimePlatform == Application.platform)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
