using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : GameUnit
{
    [Header("Weapon")]
    [SerializeField] protected ChangeSkin holdWeapon;
    [Header("HeadSkin")]
    [SerializeField] protected ChangeSkin headSkinSpawn;
    [Header("PantSkin")]
    [SerializeField] protected ChangeSkin pantSkinSpawn;
    [Header("ArmoSkin")]
    [SerializeField] protected ChangeSkin armoSkinSpawn;

    [Header("Color")]
    [SerializeField] protected SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Other")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform throwPosition;
    [SerializeField] protected Transform radiusRing;
    [SerializeField] protected LayerMask targetMask;
    [SerializeField] protected WayPointMarker waypointPrefab;

    [SerializeField] internal int characterLevel;
    [SerializeField] internal Weapon weaponPrefab;

    protected Transform targetEnemy;

    public Animator anim;
    public Transform playerBody;
    public float radius;

    public WayPointMarker wayPointMarker;

    internal bool isDead = false;
    internal int currentPLayerWeaponIndex;
    internal int currentPLayerHeadIndex;
    internal int currentPlayerPantIndex;
    internal int currentPlayerArmoIndex;
    internal int indexInScaleSO;

    internal float defaultRadius;
    internal float defaultMoveSpeed;

    internal string characterName;

    internal bool isSpecialThrow;

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

    #region StateMachine and Update

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
    }

    virtual protected void Update()
    {
        this.UpdateCharacterState();
    }

    virtual protected void UpdateCharacterState()
    {
        currentState.UpdateState(this);
    }
    #endregion

    #region Animation and Check Animation ended

    public void ChangeAnim(string animName)
    {
        //if (currentAnimName == animName)
        //    return;
        anim.ResetTrigger(currentAnimName);
        currentAnimName = animName;
        anim.SetTrigger(currentAnimName);
    }

    public bool CheckAnimationFinish()
    {
        return (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0));
    }

    #endregion

    #region RangeAttack and Target


    private IEnumerator FOVRoutine(float time)
    {
        
        while (true)
        {
            yield return Cache.GetWFS(time);
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

    #endregion

    #region Attack and LookAtTarget

    virtual internal void Attack()
    {
        if (IsAttack || haveTarget == false)
            return;

        LookAtTarget();
        if(targetEnemy != null)
            Debug.DrawLine(playerBody.position, targetEnemy.position, Color.blue, 2f);
    }

    internal void LookAtTarget()
    {
        playerBody.LookAt(targetEnemy);
    }

    internal void ThrowWeapon()
    {
        Weapon weapon = SimplePool.Spawn<Weapon>(weaponPrefab);

        if (throwPosition == null || targetEnemy == null)
            return;
        weapon.OnInit(throwPosition.position, 
            targetEnemy.position);
        weapon.SetLifeTime(radius);
        weapon.SetOwner(this);
        weapon.SetScale(Transform.localScale);
        weapon.SetSpecialThrow(isSpecialThrow);
        isSpecialThrow = false;
    }

    #endregion


    public override void OnInit()
    {
        isMoving = false;
        isAttack = false;
        isDead = false;
        isSpecialThrow = false;
        currentAnimName = "Idle";
        ChangeAnim("Idle");
        targetEnemy = null;
        defaultRadius = radius;
        characterLevel = 0;
        indexInScaleSO = 0;

        StartCoroutine(FOVRoutine(0.2f));
        ///waypoint
        wayPointMarker = SimplePool.Spawn<WayPointMarker>(waypointPrefab);
        wayPointMarker.OnInit(this);

        skinnedMeshRenderer.material = LevelManager.Instance.colorScriptableObject.GetMaterial();

        //wayPointMarker.SetData()
        //TODO: tinh dong goi
        this.wayPointMarker.arrowImageMaterial.color = skinnedMeshRenderer.material.color;
        this.wayPointMarker.image.color = skinnedMeshRenderer.material.color;
        this.wayPointMarker.nameText.text = LevelManager.Instance.nameScriptableObject.GetName();
        characterName = this.wayPointMarker.nameText.text;
        this.wayPointMarker.nameText.color = skinnedMeshRenderer.material.color;
        ////

        playerBody.gameObject.layer = LayerMask.NameToLayer("Character");
    }

    public void ChangeName(string name)
    {
        this.wayPointMarker.nameText.text = characterName;
    }
    public virtual void SaveData()
    {

    }

    public override void OnInit(Character t)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public override void OnInit(Vector3 spawnPosition, Vector3 targetEnemy)
    {
        throw new System.NotImplementedException();
    }

    #region Change Skin

    public void WeaponClick(int index)
    {
        if (LevelManager.Instance.currentWeaponIndex != index)
        {
            holdWeapon.ChangeWeapon(index, this);
        }
    }

    public void HeadSkinClick(int ButtonIndex)
    {
        headSkinSpawn.ChangeHead(ButtonIndex, this);
    }

    public void PantSkinClick(int ButtonIndex)
    {
        pantSkinSpawn.ChangePant(ButtonIndex, this);
    }

    public void ArmoSkinClick(int ButtonIndex)
    {
        armoSkinSpawn.ChangeArmo(ButtonIndex, this);
    }

    public void ResetSkinWhenXClick()
    {
        HeadSkinClick(currentPLayerHeadIndex);
        PantSkinClick(currentPlayerPantIndex);
        ArmoSkinClick(currentPlayerArmoIndex);
    }

    #endregion


    #region Change Character Buff

    internal void ChangeRadius(float rangeBuff)
    {
        radius += rangeBuff;
        radiusRing.localScale = new Vector3(radius - 0.4f, radius - 0.4f, 1);
    }

    internal void ChangeMoveSpeed(float moveSpeedBuff)
    {
        moveSpeed += moveSpeedBuff;
    }

    internal virtual void ChangeScale(int scale)    
    {
        transform.localScale = transform.localScale + transform.localScale * scale / 100f;
        radius += radius * scale / 100f;
        defaultRadius = radius;

        SoundManager.Instance.PlayEffectSound((int)AudioClipEnum.ScaleUp);
        EffectManager.Instance.PlayScaleUpParticle(playerBody);
    }

    #endregion

    #region Trigger
    public void TriggerWithGift()
    {
        isSpecialThrow = true;
    }

    #endregion

    public void AddLevel(PlayerBody enemy)
    {
        if (enemy.character.characterLevel == 0)
        {
            characterLevel += 1;
            LevelManager.Instance.coin += 1;
        }
        else
        {
            characterLevel += GetLog2(enemy.character.characterLevel);
            LevelManager.Instance.coin += GetLog2(enemy.character.characterLevel);
        }

        if(wayPointMarker != null)
        {
            wayPointMarker.SetLevelText(characterLevel);
        }
        
        if (characterLevel > LevelManager.Instance.scaleScriptableObject.GetScale(indexInScaleSO).Level)
        {
            ChangeScale(LevelManager.Instance.scaleScriptableObject.GetScale(indexInScaleSO).ScaleSize);
            indexInScaleSO++;
        }
    }

    private int GetLog2(int x)
    {
        int ans = 0;
        while ((1 << ans) <= x)
        {
            ans++;
        }
        return ans--;
    }

    internal void Revive()
    {
        isDead = false;
        currentState.ChangeState(new PlayerIdleState());
        ChangeAnim(Cache.GetString("Idle"));
    }

    internal virtual void OnHit()
    {
        SoundManager.Instance.PlayEffectSound((int)AudioClipEnum.GetHit);
    }
}
