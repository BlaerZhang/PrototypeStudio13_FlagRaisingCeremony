using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MouseAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y - 1.5f, 1.5f).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
