using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MessageBox : MonoBehaviour
{
    public static void Show(string text)
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Interfaces/InterfaceMessageBox/InterfaceMessageBox_Prefab.prefab", typeof(GameObject));
        GameObject InterfaceMessageBoxPrefab = (GameObject)prefab;
        GameObject gameObject = Instantiate(InterfaceMessageBoxPrefab, new Vector3(), new Quaternion());
        InterfaceMessageBox interfaceMessageBox = gameObject.GetComponent<InterfaceMessageBox>();
        interfaceMessageBox.text = text;
    }
}
