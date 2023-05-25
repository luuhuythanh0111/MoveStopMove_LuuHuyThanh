using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WayPointMarker : GameUnit
{
    public Image image;
    public RectTransform rectTransform;
    public RectTransform arrowImage;

    public Transform target;
    public TextMeshProUGUI levelText;
    public Vector2 imageSize;
    public Vector3 offset;

    private void Update()
    {
        if (target == null)
            return;
        float minX = imageSize.x / 2;
        float minY = imageSize.y / 2;

        float maxX = Screen.width - minX;
        float maxY = Screen.height - minY;
        Vector3 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (pos.z < 0)
        {
            pos.y = Screen.height - pos.y;
            pos.x = Screen.width - pos.x;
        }

        /// Check to Show arrow image
        if (IsBetween(pos.x, minX, maxX) && IsBetween(pos.y, minY, maxY))
        {
            arrowImage.gameObject.SetActive(false);
        }
        else
        {
            arrowImage.gameObject.SetActive(true);
        }
        ///

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;
        image.transform.position = pos;
        

        if(rectTransform.anchoredPosition.x < 0)
        {
            arrowImage.eulerAngles = new Vector3(0, 0, 90f - ConvertRadianToDegree(rectTransform.anchoredPosition.y / -rectTransform.anchoredPosition.x));
        }
        else
        {
            arrowImage.eulerAngles = new Vector3(0, 0,-90 + ConvertRadianToDegree(rectTransform.anchoredPosition.y / rectTransform.anchoredPosition.x));
        }

        Vector2 lookDirect = rectTransform.anchoredPosition - Vector2.zero;
        arrowImage.anchoredPosition = Vector2.zero + lookDirect.normalized * 50f;
    }
    private bool IsBetween(float testValue, float bound1, float bound2)
    {
        return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
    }

    private float ConvertRadianToDegree(float radian)
    {
        return (Mathf.Atan(radian) * 180) / Mathf.PI;
    }

    public void SetLevelText(int level)
    {
        levelText.text = level.ToString(); 
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInit(Character t)
    {
        target = t.transform;
        SetLevelText(t.characterLevel);
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }

}
