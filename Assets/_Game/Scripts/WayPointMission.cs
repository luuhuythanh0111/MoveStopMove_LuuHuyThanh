using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPointMission : GameUnit
{
    public Image image;
    public Transform target;

    public Vector2 offset;

    private float previosX = 0;
    private float previosY = 0;

    void Update()
    {
        if (target != null)
        {
            float minX = image.GetPixelAdjustedRect().width / 2;
            float minY = image.GetPixelAdjustedRect().height / 2;

            float maxX = Screen.width - minX;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.current.WorldToScreenPoint(target.position);
            
            //// need to fix camera


            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            
            image.transform.position = pos; 
        }
    }

    public override void OnDespawn()
    {
        throw new System.NotImplementedException();
    }

    public override void OnInit()
    {
        //Debug.Log(1);
    }

    public override void OnInit(Character t)
    {
        target = t.transform;
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }
}
