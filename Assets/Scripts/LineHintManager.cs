using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LineHintManager : MonoBehaviour
{
    public static LineHintManager Instance;
    bool isActive;
    const float scrollSpeed = 1.5f;
    float offset;
    [SerializeField] Material mat;
    [SerializeField] Transform origin;
    [SerializeField] Transform target;
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Assert.IsNotNull(lineRenderer);
        Assert.AreEqual(lineRenderer.positionCount, 2);
        isActive = false;
    }

    private void OnEnable() => SetTextureOffset(Vector2.zero);
    private void OnDisable() => SetTextureOffset(Vector2.zero);

    void Update()
    {
        if (!isActive) return;
        offset += (Time.deltaTime * scrollSpeed);
        SetTextureOffset(-Vector2.right * offset);
        lineRenderer.SetPosition(0, origin.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);

        float dist = Vector3.Distance(origin.position, target.position);
        Debug.Log(dist);
        mat.SetTextureScale("_MainTex", new Vector2(dist, 1));
    }

    private void SetTextureOffset(Vector2 value) => mat.SetTextureOffset("_MainTex", value);

    public void ShowHintLine(Transform origin, Transform target)
    {
        if (origin == null || target == null) return;
        this.origin = origin;
        this.target = target;
        isActive = true;
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 36;
        GUI.skin.button.fontSize = 36;
        if (GUILayout.Button("A to B")) ShowHintLine(GameObject.Find("Cube A").transform, GameObject.Find("Cube B").transform);
    }
}
