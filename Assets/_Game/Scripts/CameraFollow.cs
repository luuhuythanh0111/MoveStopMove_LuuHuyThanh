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
    
    private Vector3 targetPosition;

    //Start is called before the first frame update
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
}
