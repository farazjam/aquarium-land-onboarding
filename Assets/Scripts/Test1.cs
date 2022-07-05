using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Test1 : MonoBehaviour
{
    const float scrollSpeed = 0.5f;
    float offset;
    Material mat;

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().materials[0];
        Assert.IsNotNull(mat);
        mat.SetTextureScale("_MainTex", new Vector2(1, 5));
    }

    private void OnEnable() => SetTextureOffset(Vector2.zero);
    private void OnDisable() => SetTextureOffset(Vector2.zero);

    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed);
        SetTextureOffset(-Vector2.up * offset);
    }

    private void SetTextureOffset(Vector2 value) => mat.SetTextureOffset("_MainTex", value);
}
