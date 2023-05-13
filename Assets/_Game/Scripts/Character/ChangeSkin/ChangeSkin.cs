using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public Skin skin;

    private Vector3 localPosition;
    private Quaternion localRotation;
    public void ChangeWeapon(int WeaponIndex)
    {
        if (skin != null)
            skin.OnDespawn();
        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetWeaponSkin(WeaponIndex));
        skin.OnInit();

        localPosition = skin.Transform.localPosition;
        localRotation = skin.Transform.localRotation;

        skin.transform.SetParent(transform);
        skin.transform.localPosition = localPosition;
        skin.transform.localRotation = localRotation;

    }
    public void ChangeHead(int ButtonIndex)
    {
        if (skin != null)
            skin.OnDespawn();
        if(ButtonIndex == -1)
        {
            if(skin!=null)
                skin.OnDespawn();
            return;
        }
        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetHeadSkin(ButtonIndex));
        skin.OnInit();

        localPosition = skin.Transform.localPosition;
        localRotation = skin.Transform.localRotation;

        skin.transform.SetParent(transform);
        skin.transform.localPosition = localPosition;
        skin.transform.localRotation = localRotation;
    }

    public void ChangePant(int PantIndex)
    {
        skin.skinnedMeshRenderer.material = LevelManager.Instance.skinScriptableObject.GetPantMaterial(PantIndex);
    }

    public void ChangeArmo(int ButtonIndex)
    {
        if (skin != null)
            skin.OnDespawn();
        Debug.Log(ButtonIndex);
        if (ButtonIndex >= LevelManager.Instance.skinScriptableObject.armoSkin.Length || ButtonIndex < 0)
        {
            if (skin != null)
                skin.OnDespawn();
            return;
        }
        
        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetArmoSkin(ButtonIndex));
        skin.OnInit();

        localPosition = skin.Transform.localPosition;
        localRotation = skin.Transform.localRotation;

        skin.transform.SetParent(transform);
        skin.transform.localPosition = localPosition;
        skin.transform.localRotation = localRotation;
    }
}
