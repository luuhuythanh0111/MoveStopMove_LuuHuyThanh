using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public Skin skin;
    public Skin pantSkin; ///this for pant because we change only material of pant 

    private Vector3 localPosition;
    private Quaternion localRotation;
    public void ChangeWeapon(int WeaponIndex, Character character)
    {
        if (skin != null)
            skin.OnDespawn();
        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetWeaponSkin(WeaponIndex));
        skin.OnInit(character);

        localPosition = skin.Transform.localPosition;
        localRotation = skin.Transform.localRotation;

        skin.transform.SetParent(transform);
        skin.transform.localPosition = localPosition;
        skin.transform.localRotation = localRotation;

    }
    public void ChangeHead(int ButtonIndex, Character t)
    {
        if (skin != null)
            skin.OnDespawn();
        if (ButtonIndex >= LevelManager.Instance.skinScriptableObject.headSkin.Length || ButtonIndex < 0)
        {
            return;
        }
        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetHeadSkin(ButtonIndex));
        skin.OnInit(t);

        localPosition = skin.Transform.localPosition;
        localRotation = skin.Transform.localRotation;

        skin.transform.SetParent(transform);
        skin.transform.localPosition = localPosition;
        skin.transform.localRotation = localRotation;
    }

    public void ChangePant(int PantIndex, Character t)
    {
        pantSkin.skinnedMeshRenderer.material = LevelManager.Instance.skinScriptableObject.GetPantMaterial(PantIndex);

        if (skin != null)
            skin.OnDespawn();
        if (PantIndex >= LevelManager.Instance.skinScriptableObject.pantSkin.Length || PantIndex < 0)
        {
            if (skin != null)
                skin.OnDespawn();
            return;
        }

        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetPantSkin(PantIndex));
        skin.OnInit(t);
        skin.gameObject.SetActive(false);
    }

    public void ChangeArmo(int ButtonIndex, Character t)
    {
        if (skin != null)
            skin.OnDespawn();
        if (ButtonIndex >= LevelManager.Instance.skinScriptableObject.armoSkin.Length || ButtonIndex < 0)
        {
            return;
        }
        
        skin = SimplePool.Spawn<Skin>(LevelManager.Instance.skinScriptableObject.GetArmoSkin(ButtonIndex));
        skin.OnInit(t);

        localPosition = skin.Transform.localPosition;
        localRotation = skin.Transform.localRotation;

        skin.transform.SetParent(transform);
        skin.transform.localPosition = localPosition;
        skin.transform.localRotation = localRotation;
    }
}
