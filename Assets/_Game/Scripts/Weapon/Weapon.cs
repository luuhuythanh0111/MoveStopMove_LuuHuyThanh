using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : GameUnit
{

    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Transform flyingThing;
    
    public Character owner;

    private float lifeTime; public float LifeTime { set { lifeTime = value; } }

    private float timer = 0f;
    private float speed = 6f; public float Speed { get { return speed; } set { speed = value; } }

    private bool specialThrow;

    private Vector3 moveDirection;
    private Vector3 localPosition;
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

    virtual protected void Update()
    {
        rigidbody.velocity = speed * moveDirection.normalized;
        Transform.Rotate(0, 360 * Time.deltaTime, 0);

        ThrowSpecialScale();

        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            rigidbody.velocity = Vector3.zero;
            OnDespawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cache.GetString("PlayerBody")))
        {
            PlayerBody enemy = Cache.GetPlayerBody(other);

            //Character c = other.GetComponent<Character>();
            //TODO:
            //c.OnHit();
            enemy.character.OnDespawn();
            OnDespawn();
            owner.AddLevel(enemy);
        }
    }

    public void ThrowSpecialScale()
    {
        Transform.localScale += (specialThrow == true ? 1 : 0) * Transform.localScale * Time.deltaTime;
    }

    public void SetLifeTime(float radius)
    {
        lifeTime = radius / speed;
    }

    public void SetOwner(Character t)
    {
        owner = t;
    }

    public void SetSpecialThrow(bool isSpecialThrow)
    {
        specialThrow = isSpecialThrow;
    }

    public void SetScale(Vector3 scale)
    {
        Transform.localScale = scale;
    }

    public void ResetScale()
    {
        Transform.localScale = Vector3.one;
    }


    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        if(spawnPosition == null || targetEnemy == null)
        {
            OnDespawn();
            return;
        }
        timer = 0f;
        ResetScale();
        localPosition = transform.localPosition;
        transform.position = spawnPosition;
        
        moveDirection = targetEnemy - spawnPosition;
        transform.LookAt(targetEnemy);
        
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
        
    }
}
