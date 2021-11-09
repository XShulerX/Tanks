using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class Element
    {
        public readonly Elements element;
        public readonly Dictionary<Elements, int> elementModifers;
        public readonly Material elementMaterial; 

        public Element(Elements entityElement, Dictionary<Elements, int> modifers, Material material)
        {
            element = entityElement;
            elementModifers = modifers;
            elementMaterial = material;
        }
    }
}