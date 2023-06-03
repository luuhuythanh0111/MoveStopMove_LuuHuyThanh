using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;

    private Vector3 destionation;
    private Collider[] rangeSeekEnemy;


    protected override void Start()
    {
        base.Start();
        currentState.ChangeState(new BotIdleState());
        moveSpeed = agent.speed;
        defaultMoveSpeed = moveSpeed;
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        agent.SetDestination(position);
    }

    internal void MoveStop()
    {
        agent.enabled = false;
    }

    internal override void Attack()
    {
        base.Attack();
    }

    internal void SpawnPosition(float distanceRadius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized * distanceRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, distanceRadius, 1))
        {
            finalPosition = hit.position;
        }
        transform.position = finalPosition;
    }

    public override void OnInit()
    {
        base.OnInit();
        SpawnPosition(20f);
        currentState.ChangeState(new BotIdleState());
        moveSpeed = agent.speed;
        defaultMoveSpeed = moveSpeed;
    }
    public override void OnDespawn()
    {
        LevelManager.Instance.BotDespawn(this);
        base.OnDespawn();
        this.MoveStop();
    }

    internal void SeekTarget(ref Transform targetEnemy)
    {
        rangeSeekEnemy = Physics.OverlapSphere(playerBody.position, 50, targetMask);

        if (rangeSeekEnemy.Length <= 1)
            return;
        float distance = 1000f;
        float temporaryDistance;

        for (int i = 0; i < rangeSeekEnemy.Length; i++)
        {
            temporaryDistance = Vector3.Distance(playerBody.position, rangeSeekEnemy[i].transform.position);
            if (temporaryDistance < 0.1f)
                continue;
            if (distance > temporaryDistance)
            {
                distance = temporaryDistance;
                targetEnemy = rangeSeekEnemy[i].transform;
            }
        }
        Debug.DrawLine(playerBody.position, targetEnemy.position, Color.green,3f);
    }
}