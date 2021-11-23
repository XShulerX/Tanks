using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MVC
{
    public static class MaterialAssociationMap
    {
        private static List<Color> _colorMapMaterials = Resources.Load<MaterialColorInfo>("MaterialMap").GetColorMap;
        private static List<Material> _materialsForColorMap = Resources.Load<MaterialColorInfo>("MaterialMap").GetMaterialsOfElement;

        private static Dictionary<Material, Color> associationColorMap = Enumerable.Range(0, _materialsForColorMap.Count).ToDictionary(i => _materialsForColorMap[i], i => _colorMapMaterials[i]);
        
        public static Color GetColorForMaterial(Material key)
        {
            return associationColorMap[key];
        }
    }
}

