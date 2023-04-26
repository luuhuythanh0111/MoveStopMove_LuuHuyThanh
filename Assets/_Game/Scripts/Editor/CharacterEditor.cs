using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    private void OnSceneGUI()
    {
        Character character = (Character)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(character.transform.position, Vector3.up, Vector3.forward, 360, character.radius);
    }
}
