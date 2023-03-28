using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BarMng : MonoBehaviour
{
    public enum Statuses
    {
        Off = 0,
        On = 1,
        Alert = 3
    }

    public TextMeshProUGUI TextField;
    public string Value
    {
        get { return TextField.text; }
        set { TextField.SetText(Value); }
    }

    public List<Image> StatysImages;

    private Statuses status;
    public Statuses Status
    {
        get { return status; }
        set { UpdateStatus(value); }
    }
    // Start is called before the first frame update
    void Start()
    {
        TextField.SetText(Value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateStatus(Statuses value)
    {
        if ((value < Statuses.Off)| (value > Statuses.Alert))
        {
            throw new ArgumentException(gameObject.name +" Статус Бар получил не верный статус");
        }
        StatysImages[(int)status].enabled = false;
        StatysImages[(int)value].enabled = true;
        status = value;

    }
}
