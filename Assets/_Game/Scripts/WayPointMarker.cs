using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WayPointMarker : GameUnit
{
    public Image image;
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

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = 0;
        image.transform.position = pos;
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
