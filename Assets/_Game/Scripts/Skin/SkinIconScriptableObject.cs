using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkinIConObject", menuName = "ScriptableObjects/SkinIconScriptableObject")]
public class SkinIconScriptableObject : ScriptableObject
{
    public List<Sprite> headSkinIcon;
    public List<Sprite> pantSkinIcon;
    public List<Sprite> armoSkinIcon;

    public Sprite GetHeadSkinIcon(int index)
    {
        return headSkinIcon[index];
    }
    public Sprite GetPantSkinIcon(int index)
    {
        return pantSkinIcon[index];
    }
    public Sprite GetArmoSkinIcon(int index)
    {
        return armoSkinIcon[index];
    }
}
