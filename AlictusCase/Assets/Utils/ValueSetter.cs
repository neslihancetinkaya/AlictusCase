using System;
using UnityEngine;
using Utils.RefValues;

namespace Utils
{
    public class ValueSetter : MonoBehaviour
    {
        [SerializeField] private BoolRef IsSwipe;

        private void Awake()
        {
            IsSwipe.Value = false;
        }
    }

}