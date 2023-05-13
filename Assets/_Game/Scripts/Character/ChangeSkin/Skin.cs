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

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        
    }

    
}
