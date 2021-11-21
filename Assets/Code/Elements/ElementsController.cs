using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class ElementsController: IResetable
    {
        private List<Element> _elements;
        private UnitStorage _unitStorage;  

        public ElementsController(UnitStorage unitStorage)
        {
            _unitStorage = unitStorage;

            _elements = new List<Element>()
            {
                new Element(Elements.Fire,
                    new Dictionary<Elements, int>()
                    {
                        [Elements.Fire] = 1,
                        [Elements.Terra] = 1,
                        [Elements.Water] = 2
                    },
                    Resources.Load("ElementMaterials/Fire") as Material
                ),

                new Element(Elements.Terra,
                    new Dictionary<Elements, int>()
                    {
                        [Elements.Fire] = 2,
                        [Elements.Terra] = 1,
                        [Elements.Water] = 1
                    },
                    Resources.Load("ElementMaterials/Terra") as Material
                ),

                new Element(Elements.Water,
                    new Dictionary<Elements, int>()
                    {
                        [Elements.Fire] = 1,
                        [Elements.Terra] = 2,
                        [Elements.Water] = 1
                    },
                    Resources.Load("ElementMaterials/Water") as Material
                )
            };
        }

        public void UpdateElements()
        {
            var elements = Enum.GetValues(typeof(Elements));
            for (int i = 0; i < _unitStorage.enemies.Count; i++)
            {
                _unitStorage.enemies[i].TankElement = (Elements)UnityEngine.Random.Range(0, elements.Length);
                for (int j = 0; j < _elements.Count; j++)
                {
                    if (_elements[j].element == _unitStorage.enemies[i].TankElement)
                    {
                        var turretMaterial = _elements[j].elementMaterial;
                        _unitStorage.enemies[i].Material = turretMaterial;
                        var materials = _unitStorage.enemies[i].Turret.GetComponent<MeshRenderer>().materials;
                        materials[0] = turretMaterial;
                        _unitStorage.enemies[i].Turret.GetComponent<MeshRenderer>().materials = materials;
                    }
                }
            }      
        }

        public int GetModifer(ITakeDamageEnemy enemy, Elements element)
        {
            int modifer = 1;
            for (int i = 0; i < _elements.Count; i++)
            {
                if(_elements[i].element == enemy.TankElement)
                {
                    _elements[i].elementModifers.TryGetValue(element, out modifer);
                    break;
                }
            }
            return modifer;
        }

        public void Reset()
        {
            UpdateElements();
        }
    }
}
