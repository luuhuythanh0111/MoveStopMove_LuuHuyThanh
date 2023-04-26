using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    private void OnSceneGUI()
    {
        Player player = (Player)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(player.playerBody.position, Vector3.up, Vector3.forward, 360, player.radius);
    }
}
