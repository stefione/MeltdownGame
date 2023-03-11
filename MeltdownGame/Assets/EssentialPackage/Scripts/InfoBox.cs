using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
public class InfoBox : Popup<InfoBox>
{
    [SerializeField] TextMeshProUGUI _TitleText;
    [SerializeField] TextMeshProUGUI infoText;
    Action _onInfoBoxClose;
    public void Show(string title,string msg,Action onClose=null)
    {
        _onInfoBoxClose = onClose;
        _TitleText.text = title;
        infoText.text = msg;
        ShowPopup();
    }

    public override void HidePopup()
    {
        _onInfoBoxClose?.Invoke();
        base.HidePopup();
    }
}
