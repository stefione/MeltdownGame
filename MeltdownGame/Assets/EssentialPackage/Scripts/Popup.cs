using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Popup<T> : MonoBehaviourSingleton<T> where T:MonoBehaviour
{
    public virtual void ShowPopup()
    {
        gameObject.SetActive(true);
    }
    public virtual void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
