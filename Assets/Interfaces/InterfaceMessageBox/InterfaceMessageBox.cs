using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceMessageBox : MonoBehaviour
{
    public Text messageBoxText;

    public string text;

    void Start()
    {
        messageBoxText.text = text;
    }

    public void CloseMessageBox()
    {
        Destroy(gameObject);
    }
}
