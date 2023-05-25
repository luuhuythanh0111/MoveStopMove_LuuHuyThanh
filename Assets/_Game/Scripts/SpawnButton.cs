using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public List<ButtonIndex> skinButtonPrefabs;


    private void Awake()
    {
        
        for(int i=0; i<LevelManager.Instance.skinScriptableObject.headSkin.Length; i++)
        {
            ButtonIndex button = SimplePool.Spawn<ButtonIndex>(skinButtonPrefabs[0]);
            button.OnInit();
            button.ButtonIndexInSO = i;
            button.BackGround.sprite = LevelManager.Instance.skinIconScriptableObject.GetHeadSkinIcon(i);
            Menu.Instance.headButtons.Add(button);
        }

        for (int i = 0; i < LevelManager.Instance.skinScriptableObject.pantSkin.Length; i++)
        {
            ButtonIndex button = SimplePool.Spawn<ButtonIndex>(skinButtonPrefabs[1]);
            button.OnInit();
            button.ButtonIndexInSO = i;
            button.BackGround.sprite = LevelManager.Instance.skinIconScriptableObject.GetPantSkinIcon(i);
            Menu.Instance.pantButtons.Add(button);
        }

        for (int i = 0; i < LevelManager.Instance.skinScriptableObject.armoSkin.Length; i++)
        {
            ButtonIndex button = SimplePool.Spawn<ButtonIndex>(skinButtonPrefabs[2]);
            button.OnInit();
            button.ButtonIndexInSO = i;
            button.BackGround.sprite = LevelManager.Instance.skinIconScriptableObject.GetArmoSkinIcon(i);
            Menu.Instance.armoButtons.Add(button);
        }
    }
}
