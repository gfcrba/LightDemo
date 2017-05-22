using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgControl : MonoBehaviour
{
    public float defaultTime = 2.0f;
    private float currentMsgTimer = 0.0f;
    private Color fadeColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Text txtCtrl;

    void Start()
    {
        txtCtrl = GetComponent<Text>();
    }

    public void SetMessage(string msg)
    {
        
        txtCtrl.text = msg;
        currentMsgTimer = defaultTime;
        txtCtrl.color = defaultColor;
    }

    void Update()
    {
        if(currentMsgTimer > 0.0f)
        {
            currentMsgTimer -= Time.deltaTime;
        }
        else
        {
            txtCtrl.color = Color.Lerp(txtCtrl.color, fadeColor, Time.deltaTime);
        }
    }
}
