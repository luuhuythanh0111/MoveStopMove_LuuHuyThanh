using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask targetMask;

    public Transform playerBody;
    
    public float radius;

    private bool haveTarget = false;
    private bool haveAttackTarget = false;
    private Vector3 targetEnemy;
    private Transform m_Transform;

    protected string currentAnimName;


    public Transform Transform
    {
        get
        {
            if (m_Transform == null)
                m_Transform = transform;
            return m_Transform;
        }
    }


    private Collider[] rangeCheck;

    // Start is called before the first frame update
    virtual internal void Start()
    {
        StartCoroutine(FOVRoutine(0.2f));
    }

    // Update is called once per frame
    virtual internal void Update()
    {
        if(haveTarget)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    ///=======================================================================\
    /// Animation
    ///=======================================================================\
    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            //Debug.Log(currentAnimName + " -> " + animName);
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    private IEnumerator AttackRoutine(float time = 10f)
    {
        Cache.GetWFS(time);
        ChangeAnim("Attack");
        yield return time;
        ChangeAnim("Idle");
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

}
