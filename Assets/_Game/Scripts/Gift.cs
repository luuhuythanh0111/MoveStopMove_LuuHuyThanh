using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gift : GameUnit
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

    public LayerMask groundLayerMask;
    public new Rigidbody rigidbody;

    private void Update()
    {
        OnGround();
    }

    private void OnGround()
    {
        RaycastHit hit;

        Debug.DrawLine(Transform.position, Transform.position + Vector3.down*0.4f, Color.red);

        if (Physics.Raycast(Transform.position, Vector3.down , out hit, 0.4f, groundLayerMask))
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Cache.GetString("PlayerBody")))
        {
            Character character = Cache.GetPlayerBody(other).character;

            character.TriggerWithGift();
            OnDespawn();
        }
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        LevelManager.Instance.GiftDespawn(this);
    }

    public override void OnInit()
    {
        Transform.position = new Vector3(Random.Range(-25f, 25f),
                                         20,
                                         Random.Range(-25f, 25f));
        rigidbody.useGravity = true;
        rigidbody.isKinematic = false;
    }

    public override void OnInit(Character t)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }

    
}
