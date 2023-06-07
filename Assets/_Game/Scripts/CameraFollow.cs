using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject Menu;

    [SerializeField] Vector3 weaponShopPosition;
    [SerializeField] Quaternion weaponShopRotation;

    [SerializeField] Vector3 skinShopPosition;
    [SerializeField] Quaternion skinShopRotation;

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.01f;

    private Vector3 defaultOffset;
    private Vector3 targetPosition;

    private Quaternion defaultQuaternion;

    private void Awake()
    {
        defaultOffset = new Vector3(0, 3.5f, -10);
        defaultQuaternion = new Quaternion(-20/360f, 0, 0, 0);
    }
    private void LateUpdate()
    {
        if (GameManager.Instance.IsState(GameState.MainMenu))
            return;
        if(target == null)
        {
            return;
        }
        Vector3 vel = Vector3.zero;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref vel, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

    public void SetUpCameraForMainMenu()
    {
        transform.position = new Vector3(0, 5, -10);
        transform.rotation = weaponShopRotation;
    }

    public void SetUpCameraForWeaponShop()
    {
        
    }

    public void SetUpCameraForSkinShop()
    {
        transform.position = target.position + skinShopPosition;
        transform.rotation = skinShopRotation;
    }

    public void ChangeOffset()
    {
        offset += offset * 5 / 100f;
    }

    public void ResetOffset()
    {
        transform.position = target.position + defaultOffset;
        transform.rotation = Quaternion.Euler(new Vector3(20,0,0));
    }
}
