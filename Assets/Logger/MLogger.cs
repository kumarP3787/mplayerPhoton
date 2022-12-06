using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MLogger : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GraphicRaycaster raycaster;
    [SerializeField] CanvasScaler scaler;
    [SerializeField] TMP_Text txt;


    private void Awake()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        Debug.Log("its test");
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (txt.text == "") txt.text = condition + "\n" + stackTrace ;
        else        
            txt.text+= "\n"+condition + "\n" + stackTrace;
        Show = true;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= Application_logMessageReceived;
    }
    public void DoClear()
    {
        txt.text = "";
        Show = false;
    }
 
    bool Show
    {
        set
        {
            _canvas.enabled = value;
            raycaster.enabled = true;
            scaler.enabled = true;
        }
    }
}
