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
        
        [SerializeField] private Vector3 EndPos;
        [SerializeField] private float CutSpeed;
        [SerializeField] private ParticleSystem SoapFx;
        private Vector3 _knifeInitPosition;
        private Vector3 _knifeTargetPosition;
        private float _knifeLerpTime;

        private void Start()
        {
            Initialize();
        }

       
        private void Update()
        {
            if (IsSwipe.Value)
            {
                SoapFx.Play();
                transform.position = Vector3.Lerp(_knifeInitPosition, _knifeTargetPosition, _knifeLerpTime);
                _knifeLerpTime += Time.deltaTime * CutSpeed;
                if (_knifeLerpTime >= 1f)
                {
                    _knifeLerpTime = 0f;
                    LayerCompleted.Raise();
                }
            }
            
            else
            {
                SoapFx.Stop();
            }
        }
        
        private void Initialize()
        {
            _knifeInitPosition = transform.position;
            _knifeTargetPosition = EndPos;
            SetLayer();
        }

        public void SetLayer()
        {
            transform.position = _knifeInitPosition;
        }

        public void OnLevelCompleted()
        {
            IsSwipe.Value = false;
            SoapFx.Stop();
        }
      
    }
}