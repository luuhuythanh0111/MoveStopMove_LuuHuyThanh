using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : GameUnit
{
    private Transform m_Transform;
    public Transform Transform
    {
        get
        {
            if (m_Transform == null)
                m_Transform = transform;
            return m_Transform;
        }
    }

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public BuffType buffType;

    public float radiusBuff;
    public float moveSpeedBuff;

    private Character currentCharacter;

    private float radiusBuffChange = 0;
    private float moveSpeedBuffChange = 0;

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        if (currentCharacter == null)
        {
            return;
        }

        currentCharacter.ChangeRadius(-radiusBuffChange);
        currentCharacter.ChangeMoveSpeed(-moveSpeedBuffChange);

        radiusBuffChange = 0;
        moveSpeedBuffChange = 0;
    }

    public override void OnInit()
    {
        
    }

    public override void OnInit(Character t)
    {
        radiusBuffChange = t.defaultRadius * (radiusBuff / 100);
        moveSpeedBuffChange = t.defaultMoveSpeed * (moveSpeedBuff / 100);
        t.ChangeRadius(radiusBuffChange);
        t.ChangeMoveSpeed(moveSpeedBuffChange);
        currentCharacter = t;
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        
    }

    
}

public enum BuffType
{
    Range,
    MoveSpeed,
    Gold
}
