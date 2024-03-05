using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetSplitScreenOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        DOTween.To(() => Camera.main.rect, x => Camera.main.rect = x, new Rect(new Vector2(-0.3f, 0), Vector2.one), 1);
    }

    private void OnDisable()
    {
        DOTween.To(() => Camera.main.rect, x => Camera.main.rect = x, new Rect(new Vector2(0, 0), Vector2.one), 1);
    }
}
