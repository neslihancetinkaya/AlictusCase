using System;
using UnityEngine;
using Utils.Event;
using Utils.RefValues;

namespace SoapCutting_Minigame.Scripts
{
    public class KnifeController : MonoBehaviour
    {
        [SerializeField] private BoolRef IsSwipe;
        [SerializeField] private GameEvent LayerCompleted;
        [SerializeField] private float CutSpeed;
        [SerializeField] private ParticleSystem SoapFx;
        
        private Vector3 _knifeInitPosition;
        private Vector3 _knifeTargetPosition;
        private float _knifeLerpTime;
        private void Update()
        {
            if (IsSwipe.Value)
            {
                SoapFx.Play();
                transform.localPosition = Vector3.Lerp(_knifeInitPosition, _knifeTargetPosition, _knifeLerpTime);
                _knifeLerpTime += Time.deltaTime * CutSpeed;
                if (_knifeLerpTime >= 1f)
                {
                    _knifeLerpTime = 0f;
                    LayerCompleted.Raise();
                    transform.localPosition = _knifeInitPosition;
                }
            }
            else
            {
                SoapFx.Stop();
            }
        }
        
      
    }
}