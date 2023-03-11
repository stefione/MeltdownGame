using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DebugScreen : MonoBehaviourSingletonDDOL<DebugScreen>
{
    [SerializeField] bool enableTesting;

    [SerializeField] TextMeshProUGUI textPrefab;
    [SerializeField] RectTransform content;

    [SerializeField] bool messageLog;
    [SerializeField] bool errorLog;
    [SerializeField] bool stackTraceFlag;
    public override void Initialize()
    {
        base.Initialize();
    }

    private void Start()
    {
        if (enableTesting)
        {
            Application.logMessageReceived += OnLogMessage;
        }
    }

    private void OnLogMessage(string condition, string stackTrace, LogType type)
    {
        string debug = condition;
        if (stackTraceFlag)
        {
            debug += stackTrace;
        }
        if ((type==LogType.Assert||type==LogType.Log)&&messageLog)
        {
            ShowMessage(debug, Color.white);
        }
        if ((type==LogType.Exception||type==LogType.Error)&&errorLog)
        {
            ShowMessage(debug, Color.red);
        }
    }

    void ShowMessage(string msg,Color color)
    {
        TextMeshProUGUI text = Instantiate(textPrefab, content);
        text.text = msg;
        text.color = color;
        content.ForceUpdateRectTransforms();
        Canvas.ForceUpdateCanvases();
    }

    public void ClearLog_Button()
    {
        foreach(Transform i in content)
        {
            Destroy(i.gameObject);
        }
    }
}
