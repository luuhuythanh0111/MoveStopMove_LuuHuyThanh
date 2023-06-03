using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorSO", menuName = "ScriptableObjects/ColorScriptableObject", order = 4)]

public class ColorScriptableObject : ScriptableObject
{
    public List<Material> materials;
    public List<Color> colors;

    private bool[] usedMaterials = new bool[20];

    public Material GetMaterial()
    {
        int index = UnityEngine.Random.Range(0, materials.Count);
        int count = 0;

        while (usedMaterials[index] == true)
        {
            if (count > 20) break;
            count++;
            index = UnityEngine.Random.Range(0, materials.Count);
        }

        usedMaterials[index] = true;
        return materials[index];
    }

    public Material GetMat(ColorType colorType)
    {
        return materials[(int)colorType];
    }
    public Color GetColor(ColorType colorType)
    {
        return colors[(int)colorType];
    }
}


public enum ColorType { Red = 0, Blue = 1, Green = 2}