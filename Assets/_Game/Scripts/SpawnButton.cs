using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public List<ButtonIndex> skinButtonPrefabs;
    public Transform[] buttonParents;
    public SkinShop skinShop;

    private void Awake()
    {
        
        for(int i=0; i<LevelManager.Instance.skinScriptableObject.headSkin.Length; i++)
        {
            ButtonIndex button = SimplePool.Spawn<ButtonIndex>(skinButtonPrefabs[0]);
            button.OnInit();
            button.transform.SetParent(buttonParents[0]);
            button.ButtonIndexInSO = i;
            button.BackGround.sprite = LevelManager.Instance.skinIconScriptableObject.GetHeadSkinIcon(i);
            skinShop.headButtons.Add(button);
            button.skinShop = skinShop;
        }

        for (int i = 0; i < LevelManager.Instance.skinScriptableObject.pantSkin.Length; i++)
        {
            ButtonIndex button = SimplePool.Spawn<ButtonIndex>(skinButtonPrefabs[1]);
            button.OnInit();
            button.transform.SetParent(buttonParents[1]);
            button.ButtonIndexInSO = i;
            button.BackGround.sprite = LevelManager.Instance.skinIconScriptableObject.GetPantSkinIcon(i);
            skinShop.pantButtons.Add(button);
            button.skinShop = skinShop;
        }

        for (int i = 0; i < LevelManager.Instance.skinScriptableObject.armoSkin.Length; i++)
        {
            ButtonIndex button = SimplePool.Spawn<ButtonIndex>(skinButtonPrefabs[2]);
            button.OnInit();
            button.transform.SetParent(buttonParents[2]);
            button.ButtonIndexInSO = i;
            button.BackGround.sprite = LevelManager.Instance.skinIconScriptableObject.GetArmoSkinIcon(i);
            skinShop.armoButtons.Add(button);
            button.skinShop = skinShop;
        }
    }
}
