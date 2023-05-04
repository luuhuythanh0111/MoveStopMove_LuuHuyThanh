using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private Transform orientation;
    [SerializeField] private float moveSpeed = 7f;

    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    protected override void Start()
    {
        base.Start();
        this.currentState.ChangeState(new PlayerIdleState());
    }

    protected override void UpdateCharacterState()
    {
        base.UpdateCharacterState();
        GetMouseInput();
    }

    ///=======================================================================\
    /// Input and Moving
    ///=======================================================================\ <summary>

    private void GetMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            currentState.ChangeState(new PlayerRunState());
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentState.ChangeState(new PlayerIdleState());
        }
    }

    internal void Move()
    {
        verticalInput = floatingJoystick.Vertical;
        horizontalInput = floatingJoystick.Horizontal;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rigidbody.velocity = moveSpeed * Time.deltaTime * moveDirection;

        ChangeDirection();
    }

    private void ChangeDirection()
    {
        Vector3 direct2 = new Vector3(horizontalInput, 0, verticalInput);

        if (Vector3.Distance(direct2, Vector3.zero) > 0.1f)
            playerBody.rotation = Quaternion.LookRotation(direct2);
    }
    ///=======================================================================\

    internal override void Attack()
    {
        base.Attack();
        currentState.ChangeState(new PlayerAttackState());
    }
}