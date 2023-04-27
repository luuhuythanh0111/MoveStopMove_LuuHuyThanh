using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;

    public Animator anim;
    public Transform playerBody;
    public float radius;
    
    private Vector3 targetEnemy;
    private Transform m_Transform;
    private Collider[] rangeCheck;


    private bool isMoving = false;
    private bool isAttack = false;

    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
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

    protected bool haveTarget = false;
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
        this.currentState.ChangeState(new IdleState());
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        this.UpdateCharacterState();
    }


    virtual protected void OnInit()
    {
        isMoving = false;
        isAttack = false;
        currentAnimName = "Idle";
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
        anim.ResetTrigger(currentAnimName);
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

    private void FieldOfViewCheck()
    {
        rangeCheck = Physics.OverlapSphere(playerBody.position, radius, targetMask);

        haveTarget = false;
        if (rangeCheck.Length == 0)
            return;

        haveTarget = true;
        float distance = 1000f;
        float temporaryDistance;
        
        for (int i = 0; i < rangeCheck.Length; i++)
        {
            Debug.DrawLine(playerBody.position, rangeCheck[i].transform.position, Color.red);

            temporaryDistance = Vector3.Distance(playerBody.position, rangeCheck[i].transform.position);
            if (distance > temporaryDistance)
            {
                distance = temporaryDistance;
                targetEnemy = rangeCheck[i].transform.position;
            }
        }
    }

    ///=======================================================================\


    ///=======================================================================\
    /// Attack
    ///=======================================================================\
    
    internal void Attack()
    {
        if (IsAttack)
            return;

        currentState.ChangeState(new AttackState());
    }

    ///=======================================================================\
}
