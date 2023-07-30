using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XavHelpTo.Get;
using XavHelpTo.Know;

public class NewsAtributteProcessor : MonoBehaviour
{
    public static NewsAtributteProcessor _;
    private List<AttributeScriptableObject> _attributeScriptableObjects;
    [SerializeField] private List<ConclusionScriptableObject> conclusionScriptableObjects;

    public void AddAttributes(AttributeScriptableObject[] currentNewAttributes)
    {
        _attributeScriptableObjects = _attributeScriptableObjects.Concat(currentNewAttributes).ToList();
    }

    public ConclusionScriptableObject GetConclusion()
    {
        var selectedConclusionScriptableObjects = new List<ConclusionScriptableObject>(); 
        foreach (var conclusion in conclusionScriptableObjects)
        {
            if (conclusion.requireds.Contains(_attributeScriptableObjects.ToArray()))
            {
                selectedConclusionScriptableObjects.Add(conclusion);
            }
        }
        var conclusionScriptableObject = Get.Range(selectedConclusionScriptableObjects.ToArray());

        return conclusionScriptableObject;
    }
}