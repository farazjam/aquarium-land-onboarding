using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Tester : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] Transform target;


    void Start()
    {
        Assert.IsNotNull(origin);
        Assert.IsNotNull(target);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 36;
        GUI.skin.button.fontSize = 36;
        if (GUILayout.Button("Show Hint Line"))  LineHintManager.Instance.ShowHintLine(origin.transform, target.transform);
    }

}
