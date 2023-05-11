using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsObject", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private string[] weaponsName;
    [SerializeField] private int[] weaponsValue;
    [SerializeField] private Weapon[] prefabs;
    public string GetWeaponName(int index)
    {
        return weaponsName[index];
    }

    public int GetWeaponValue(int index)
    {
        return weaponsValue[index];
    }
    public Weapon GetWeaponPrefabs(int index)
    {
        return prefabs[index];
    }
}
