using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LineHintManager : MonoBehaviour
{
    public static LineHintManager Instance;
    LineRenderer lineRenderer;
    Material mat;
    Transform origin;
    Transform target;
    bool isActive;
    const float scrollSpeed = 1.5f;
    float offset;

    void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
    }

    private void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Assert.IsNotNull(lineRenderer);
        mat = lineRenderer.materials[0];
        Assert.IsNotNull(mat);
        isActive = false;
        origin = target = null;
    }

    private void Reset()
    {
        isActive = false;
        origin = target = null;
        SetTextureOffset(Vector2.zero);
        SetTextureScale(0);
    }

    private void OnEnable() => Reset();
    private void OnDisable() => Reset();

    void Update()
    {
        if (!isActive) return;
        offset += (Time.deltaTime * scrollSpeed);
        SetTextureOffset(-Vector2.right * offset);
        SetTextureScale(Vector3.Distance(origin.position, target.position));
        lineRenderer.SetPosition(0, origin.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);
    }

    private void SetTextureOffset(Vector2 value) => mat.SetTextureOffset("_MainTex", value);
    private void SetTextureScale(float value) => mat.SetTextureScale("_MainTex", new Vector2(value, 1));

    public void ShowHintLine(Transform origin, Transform target)
    {
        if (origin == null || target == null) return;
        if (origin.gameObject.GetInstanceID() == target.gameObject.GetInstanceID()) return;
        this.origin = origin;
        this.target = target;
        isActive = true;
    }
}
