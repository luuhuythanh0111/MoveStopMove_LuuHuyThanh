using UnityEngine;
using UnityEditor;

public class MaterialGenerator : MonoBehaviour
{
    [MenuItem("GameObject/Create Material")]

    //public static int MaxColor;
    private void Start()
    {
        CreateMaterial();
    }
    static void CreateMaterial()
    {
        // Create a simple material asset

        for(int i=1; i<=20; i++)
        {
            Material material = new Material(Shader.Find("Standard"));
            AssetDatabase.CreateAsset(material, "Assets/_Game/Scripts/Color/Materials/" + i.ToString() +  ".mat");
            material.color = new Color(Random.Range(0, 255)/255f,
                                         Random.Range(0, 255) / 255f,
                                         Random.Range(0, 255) / 255f,
                                         1.0f);
            Debug.Log(AssetDatabase.GetAssetPath(material));
        }
        

        // Print the path of the created asset
        
    }
}