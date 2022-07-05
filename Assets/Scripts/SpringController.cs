using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpringController : MonoBehaviour
{
    [Header("Spring")]
    [SerializeField] GameObject springUnitPrefab;
    [SerializeField] private Transform springContainer;
    private Transform tempSpringContainer;
    [SerializeField] private readonly int springLength = 4;
    [SerializeField] private List<GameObject> springs;
    
    [Header("Collectables")]
    //[SerializeField] private ManContainer manContainer;
    
    float manCount = 0;

    void Start()
    {
        tempSpringContainer = springContainer;
        springs = new List<GameObject>();
    }

    private float totalSpringLength;
    private float safeSpringLength;
    private float currentSpringLength;
    void Update()
    {
        totalSpringLength = springs.Count * springLength;
        safeSpringLength = totalSpringLength - (springs.Count * (springLength-1));
        currentSpringLength = totalSpringLength - (manCount * 1f); // man weight = *1
    }
    
    private void OnGUI()
    {
        GUI.skin.label.fontSize = 36;
        GUI.skin.button.fontSize = 36; 
        GUILayout.Label("Spring Count = " + springs.Count);
        GUILayout.Label("Current Spring Length = " + currentSpringLength);
        GUILayout.Label("Man Count = " + manCount);
        if (GUILayout.Button("Add Spring")) AddSpringUnit();
        if (GUILayout.Button("Remove Spring")) RemoveSpringUnit();
        if (GUILayout.Button("Add Man")) AddTempMan();//manContainer.AddMan();
        if (GUILayout.Button("Remove Man")) RemoveTempMan();//manContainer.RemoveMan();
    }

    void AddSpringUnit()
    {
        GameObject springUnit = Instantiate(springUnitPrefab, tempSpringContainer.transform);
        springUnit.transform.localPosition += new Vector3(0f, 0f, NoSprings()?0:springLength);
        tempSpringContainer = springUnit.transform;
        springs.Add(springUnit);
        //manContainer.transform.position = new Vector3(0f, 0f, springs.Count*springLength);
    }
    
    void RemoveSpringUnit()
    {
        if (NoSprings()) return;
        Destroy(springs[springs.Count - 1]);
        springs.RemoveAt(springs.Count - 1);
        //manContainer.transform.position = new Vector3(0f, 0f, springs.Count*springLength);
        SetNewParentForNextSpring();
    }

    void SetNewParentForNextSpring()
    {
        if(NoSprings()) 
            tempSpringContainer = springContainer.transform;
        else 
            tempSpringContainer = springs[springs.Count - 1].transform;
    }
    
    bool NoSprings()
    {
        return springs.Count.Equals(0);
    }

    void AddTempMan()
    {
        float newSpringLength = totalSpringLength - ((manCount+1) * 1f); // man weight = *1
        if ((int)newSpringLength < (int)safeSpringLength) return;
        manCount++;
        springContainer.transform.localScale = new Vector3(1f, 1f, newSpringLength / totalSpringLength);
    }

    void RemoveTempMan()
    {
        if (manCount <= 0) return;
        float tempCurrentSpringLength = currentSpringLength;
        float newSpringLength = tempCurrentSpringLength + (1 * 1f); // 1 man remove * man weight (1)
        manCount--;
        springContainer.transform.localScale = new Vector3(1f, 1f, newSpringLength / totalSpringLength);
    }
}
