using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material DefaultMaterial;
    public Material TransparentMaterial;

    public void ChangeMaterialToTransparent()
    {
        meshRenderer.material = TransparentMaterial;
    }

    public void ChangeMaterialToDefault()
    {
        meshRenderer.material = DefaultMaterial;
    }

}
