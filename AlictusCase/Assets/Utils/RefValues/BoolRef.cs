using UnityEngine;

namespace Utils.RefValues
{
    [CreateAssetMenu]

    public class BoolRef : ScriptableObject
    {
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
            }
        }

        private bool _value;
    }
}
