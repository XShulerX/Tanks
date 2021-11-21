using System;
using UnityEngine;

namespace MVC
{
    [Serializable]
    public struct AbilityModel
    {
        [SerializeField] private Elements element;
        [SerializeField] private int _cooldown;
        [SerializeField] private Material _material;
        [SerializeField] private KeyCode _key;

        public int Cooldown { get => _cooldown; }
        public Elements Element { get => element; }
        public Material Material { get => _material; }
        public KeyCode Key { get => _key; }
    }
}