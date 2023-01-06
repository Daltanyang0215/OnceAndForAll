using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAhlpa : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float timer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        lineRenderer.materials[0].color = new Color(1, 1, 1, 0.75f);
        timer = 1;
    }

    private void Update()
    {
        lineRenderer.materials[0].color = new Color(1, 1, 1, 0.75f * timer);
        timer -= Time.deltaTime;
    }
}
