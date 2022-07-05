using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Test2 : MonoBehaviour
{
    const float scrollSpeed = 0.5f;
    float offset;
    [SerializeField] Material mat;
    [SerializeField] Transform source;
    [SerializeField] Transform destination;
    LineRenderer lineRenderer;


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Assert.IsNotNull(lineRenderer);
        Assert.AreEqual(lineRenderer.positionCount, 2);
        lineRenderer.SetPosition(0,source.transform.position);
        lineRenderer.SetPosition(1, destination.transform.position);
    }

    private void OnEnable() => SetTextureOffset(Vector2.zero);
    private void OnDisable() => SetTextureOffset(Vector2.zero);

    void Update()
    {
        /*offset += (Time.deltaTime * scrollSpeed);
        SetTextureOffset(-Vector2.left * offset);*/
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, destination.transform.position);

    }

    private void SetTextureOffset(Vector2 value) => mat.SetTextureOffset("_MainTex", value);
}
