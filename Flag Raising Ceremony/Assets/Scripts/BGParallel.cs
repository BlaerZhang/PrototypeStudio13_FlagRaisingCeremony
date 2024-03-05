using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallel : MonoBehaviour
{

    private RectTransform imageRect;
    public float rollingSpeed;
    void Start()
    {
        imageRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        imageRect.anchoredPosition = new Vector3(0, -Camera.main.transform.position.y * rollingSpeed, 0);
    }
}
