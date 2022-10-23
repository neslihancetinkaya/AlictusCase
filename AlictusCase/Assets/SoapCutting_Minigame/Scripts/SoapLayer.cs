using UnityEngine;

namespace SoapCutting_Minigame.Scripts
{
    public class SoapLayer : MonoBehaviour
    {
        private Material _material;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _material = GetComponent<Renderer>().material;
        }

        public void SetClipValue(Vector3 targetPos)
        {
            float value = transform.InverseTransformPoint(targetPos).x;
            _material.SetFloat("_ClipValue", value);
        }

        public void Finish()
        {
            Destroy(gameObject);
        }
    }
}