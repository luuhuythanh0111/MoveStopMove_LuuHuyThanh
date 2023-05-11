using UnityEngine;

public class Weapon : GameUnit
{

    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Transform flyingThing;

    private float lifeTime; public float LifeTime { set { lifeTime = value; } }

    private float timer = 0f;
    private float speed = 6f; public float Speed { get { return speed; } set { speed = value; } }

    private Vector3 moveDirection;

    virtual protected void Update()
    {
        rigidbody.velocity = speed * moveDirection.normalized;

        timer += Time.deltaTime;

        if (timer > lifeTime)
        {
            rigidbody.velocity = Vector3.zero;
            OnDespawn();
        }
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        timer = 0f;
        transform.position = spawnPosition;
        moveDirection = targetEnemy - spawnPosition;
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit()
    {
        throw new System.NotImplementedException();
    }
}
