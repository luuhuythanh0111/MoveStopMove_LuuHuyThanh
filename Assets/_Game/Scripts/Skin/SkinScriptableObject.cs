using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinObject", menuName = "ScriptableObjects/SkinScriptableObject")]
public class SkinScriptableObject : ScriptableObject
{
    [Header("Weapon Skin")]
    [SerializeField] internal Skin[] weaponSkin;

    [Header("Head Skin")]
    [SerializeField] internal Skin[] headSkin;

    [Header("Part Skin")]
    [SerializeField] internal Skin[] pantSkin;
    [SerializeField] internal Material[] pantSkinMaterial;

    [Header("Armo Skin")]
    [SerializeField] internal Skin[] armoSkin;

    [Header("Set Skin")]
    [SerializeField] internal Skin[] setSkin; ///not update yet

    public Skin GetWeaponSkin(int skinIndex)
    {
        return weaponSkin[skinIndex];
    }

    public Skin GetHeadSkin(int skinIndex)
    {
        return headSkin[skinIndex];
    }

    public Skin GetPantSkin(int skinIndex)
    {
        return pantSkin[skinIndex];
    }

    public Material GetPantMaterial(int skinIndex)
    {
        return pantSkinMaterial[skinIndex];
    }

    public Skin GetArmoSkin(int skinIndex)
    {
        return armoSkin[skinIndex];
    }
}
