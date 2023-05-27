using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct Scale
{
    public int Level, ScaleSize;
}
[CreateAssetMenu(fileName = "ScaleObject", menuName = "ScriptableObjects/ScaleScriptableObject", order = 4)]
public class ScaleScriptableObject : ScriptableObject
{
    [SerializeField] private Scale[] scales;

    public Scale GetScale(int index)
    {
        return scales[index];
    }
}
