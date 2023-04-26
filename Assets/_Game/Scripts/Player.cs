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
    

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
    }

    internal override void Update()
    {
        base.Update();
        Move();
    }

    ///=======================================================================\
    /// Input and Moving
    ///=======================================================================\
    private void Move()
    {
        verticalInput = floatingJoystick.Vertical;
        horizontalInput = floatingJoystick.Horizontal;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rigidbody.velocity = moveSpeed * Time.deltaTime * moveDirection;

        if(Mathf.Abs(rigidbody.velocity.x) > 0.1f || Mathf.Abs(rigidbody.velocity.z) > 0.1f)
        {
            Debug.Log(2);
            ChangeAnim("Run");
        }
        else
        {
            Debug.Log(1);
            ChangeAnim("Idle");
        }

        ChangeDirection();
    }

    private void ChangeDirection()
    {
        Vector3 direct2 = new Vector3(-horizontalInput, 0, -verticalInput);

        if (Vector3.Distance(direct2, Vector3.zero) > 0.1f)
            playerBody.rotation = Quaternion.LookRotation(direct2);
    }
    ///=======================================================================\
    

}
