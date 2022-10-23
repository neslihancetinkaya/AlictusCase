using UnityEngine;

namespace Utils.RefValues
{
    [CreateAssetMenu]
    public class TransformRef : ScriptableObject
    {
        public Transform Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        private Transform _value;
    }
}