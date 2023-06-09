using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform orientation;
    [SerializeField] private FollowRing followRing;
    [SerializeField] private CameraFollow cameraFollow;

    [SerializeField] internal new Rigidbody rigidbody;
    [SerializeField] internal FloatingJoystick floatingJoystick;

    

    internal GameObject weapon;

    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;

    private int coin;

    protected override void Start()
    {
        base.Start();
        this.OnInit();
        this.currentState.ChangeState(new PlayerIdleState());
        radiusRing.localScale = new Vector3(radius-0.4f, radius-0.4f, 1);

        coin = LevelManager.Instance.coin;
        currentPLayerWeaponIndex = PlayerPrefs.GetInt("currentWeaponIndex");
        currentPLayerHeadIndex = PlayerPrefs.GetInt("currentHeadSkinIndex");
        currentPlayerPantIndex = PlayerPrefs.GetInt("currentPantSkinIndex");
        currentPlayerArmoIndex = PlayerPrefs.GetInt("currentArmoSkinIndex");

        defaultMoveSpeed = moveSpeed;

        /// change skin at start time
        holdWeapon.ChangeWeapon(currentPLayerWeaponIndex, this);
        pantSkinSpawn.ChangePant(currentPlayerPantIndex, this);
        headSkinSpawn.ChangeHead(currentPLayerHeadIndex, this);
        armoSkinSpawn.ChangeArmo(currentPlayerArmoIndex, this);
    }

    protected override void UpdateCharacterState()
    {
        base.UpdateCharacterState();
        GetMouseInput();
    }

    public override void OnInit()
    {
        base.OnInit();
        this.wayPointMarker.target = this.playerBody;
    }

    ///=======================================================================\
    /// Input and Moving
    ///=======================================================================\ <summary>

    private void GetMouseInput()
    {
        if (this.isDead)
            return;

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


    ///=======================================================================\
    /// Attack and TargetRing
    ///=======================================================================\
    internal override void Attack()
    {
        base.Attack();
        currentState.ChangeState(new PlayerAttackState());
    }

    protected override void FieldOfViewCheck()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) == false)
            return;

        if (targetEnemy != null)
        {
            PlayerBody playerBody = Cache.GetPlayerBody(targetEnemy);
            playerBody.DeactiveTargetRing();
        }

        base.FieldOfViewCheck();

        if (targetEnemy != null)
        {
            PlayerBody playerBody = Cache.GetPlayerBody(targetEnemy);
            playerBody.ActiveTargetRing();
        }
    }

    internal override void ChangeScale(int scale)
    {
        base.ChangeScale(scale);
        followRing.ChangeOffset(scale);
        cameraFollow.ChangeOffset();
    }

    public override void OnDespawn()
    {

    }

    ///=======================================================================\


    internal void SetName(string name)
    {
        characterName = name;
        this.ChangeName(name);
    }

}
