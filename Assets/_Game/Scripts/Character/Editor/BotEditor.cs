using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bot))]
public class BotEditor : Editor
{
    private void OnSceneGUI()
    {
        Bot player = (Bot)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(player.playerBody.position, Vector3.up, Vector3.forward, 360, player.radius);
    }
}
