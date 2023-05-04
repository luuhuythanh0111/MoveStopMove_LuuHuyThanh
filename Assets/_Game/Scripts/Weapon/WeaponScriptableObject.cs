using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsObject", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private Weapon[] weapons;
}
