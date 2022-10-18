using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Визуализация неуязвимости
/// </summary>
public class ImmortalityView : MonoBehaviour
{
    private const string OUTLINE_KEY = "_OutlineWidth";

    [SerializeField] 
    private SkinnedMeshRenderer meshRenderer;

    [SerializeField] 
    private Material outlineMaterial;

    [SerializeField] 
    private int valueOutline = 10;
    
    private Coroutine delayImortality;
    
    /// <summary>
    /// Установить визаулизацию неуязвимости
    /// </summary>
    /// <param name="state"></param>
    public void SetOutline(bool state)
    {
        List<Material> materials = meshRenderer.materials.ToList();

        Material material =  materials.Find(material => material.shader.name == outlineMaterial.shader.name);
        
        if (state)
           material.SetFloat(OUTLINE_KEY, valueOutline);
        else
        {
            material.SetFloat(OUTLINE_KEY, 0);
        }
    }
}
