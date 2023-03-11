using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountdownScreen : MonoBehaviourSingleton<CountdownScreen>
{
    [SerializeField] TextMeshProUGUI _countdown;
    [SerializeField] Animator _anim;
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void ChangeText(string text)
    {
        _countdown.text = text;
        _anim.SetTrigger("Play");
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
