using System.Collections;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] protected Transform throwPosition;
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] internal Weapon weaponPrefab;

    public Animator anim;
    public Transform playerBody;
    public float radius;

    internal bool isDead = false;

    protected Transform targetEnemy;

    private Transform m_Transform;
    private Collider[] rangeCheck;
    private bool isMoving = false;
    private bool isAttack = false;
    private bool haveTarget = false;

    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public bool HaveTarget { get { return haveTarget; } set { haveTarget = value; } }
    public Transform Transform
    {
        get
        {
            if (m_Transform == null)
                m_Transform = transform;
            return m_Transform;
        }
    }

    public StateMachine<Character> currentState;
    public float ResetAttackTime;
    protected string currentAnimName = "";

    private void Awake()
    {
        currentState = new StateMachine<Character>();
        currentState.SetOwner(this);
    }

    virtual protected void Start()
    {
        StartCoroutine(FOVRoutine(0.2f));
        this.OnInit();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        this.UpdateCharacterState();
    }

    virtual protected void UpdateCharacterState()
    {
        currentState.UpdateState(this);
    }


    ///=======================================================================\
    /// Animation and Check Animation ended
    ///=======================================================================\
    public void ChangeAnim(string animName)
    {
        if (currentAnimName == animName)
            return;
        //Debug.Log(currentAnimName + " -> " + animName);
        anim.ResetTrigger(animName);
        currentAnimName = animName;
        anim.SetTrigger(currentAnimName);
    }

    public bool CheckAnimationFinish()
    {
        return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0));
    }

    ///=======================================================================\


    ///=======================================================================\
    /// RangeAttack and Target
    ///=======================================================================\

    private IEnumerator FOVRoutine(float time)
    {
        Cache.GetWFS(time);

        
        while (true)
        {
            yield return time;
            FieldOfViewCheck();
        }
    }

    virtual protected void FieldOfViewCheck()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) == false)
            return;
        rangeCheck = Physics.OverlapSphere(playerBody.position, radius, targetMask);
        
        haveTarget = false;
        if (rangeCheck.Length <= 1) /// 1 because playerbody = 1
        {
            targetEnemy = null;
            return;
        }
        haveTarget = true;

        float distance = 1000f;
        float temporaryDistance;
        
        for (int i = 0; i < rangeCheck.Length; i++)
        {
            Debug.DrawLine(playerBody.position, rangeCheck[i].transform.position, Color.red);

            temporaryDistance = Vector3.Distance(playerBody.position, rangeCheck[i].transform.position);
            if (temporaryDistance < 0.1f)
                continue;
            if (distance > temporaryDistance)
            {
                distance = temporaryDistance;
                targetEnemy = rangeCheck[i].transform;
            }
        }
    }

    ///=======================================================================\


    ///=======================================================================\
    /// Attack and LookAtTarget
    ///=======================================================================\

    virtual internal void Attack()
    {
        if (IsAttack || haveTarget == false)
            return;

        LookAtTarget();
        Debug.DrawLine(playerBody.position, targetEnemy.position, Color.blue, 2f);

    }

    internal void LookAtTarget()
    {
        playerBody.LookAt(targetEnemy);

        /// throw weapon
        Weapon weapon = SimplePool.Spawn<Weapon>(weaponPrefab);
        weapon.transform.position = Transform.position;
        weapon.LifeTime = radius / weapon.Speed;
        weapon.OnInit(throwPosition.position, targetEnemy.position);

        ///
    }

    public override void OnInit()
    {
        isMoving = false;
        isAttack = false;
        currentAnimName = "Idle";
        ChangeAnim("Idle");
        targetEnemy = null;

        /// Spawn Position , Need to update new Way to spawn
        /////
        if (this is Player)
            return;
        Vector3 spawnPosition = new Vector3(Random.Range(-25f, 25f),
                        0, Random.Range(-25f, 25f)
            );
        transform.position = spawnPosition;


    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }

    ///=======================================================================\

    ///=======================================================================\
    /// OnTrigger
    ///=======================================================================\


}
