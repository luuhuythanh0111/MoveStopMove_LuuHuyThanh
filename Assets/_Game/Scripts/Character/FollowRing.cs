using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowRing : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public Transform m_Transform;

    public Transform Transform
    {
        get
        {
            if (m_Transform == null)
                m_Transform = transform;
            return m_Transform;
        }
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Cache.GetString("Obstacle")))
        {
            Obstacle obstacle = Cache.GetObstacle(other);

            obstacle.ChangeMaterialToTransparent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Cache.GetString("Obstacle")))
        {
            Obstacle obstacle = Cache.GetObstacle(other);

            obstacle.ChangeMaterialToDefault();
        }
    }

    public void ChangeOffset(int scale)
    {
        offset += offset * scale / 100f;
    }
}
