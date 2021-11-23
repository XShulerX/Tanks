using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    [CreateAssetMenu(fileName = "MaterialMap", menuName = "AssociatedMap")]
    public class MaterialColorInfo : ScriptableObject
    {
        [SerializeField] private List<Color> _colorIcons;
        [SerializeField] private List<Material> _materialsOfElement;

        public List<Color> GetColorMap { get => _colorIcons; }
        public List<Material> GetMaterialsOfElement { get => _materialsOfElement; }
    }
}

