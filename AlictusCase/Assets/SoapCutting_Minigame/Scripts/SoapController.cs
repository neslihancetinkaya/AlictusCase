using System;
using UnityEngine;
using Utils.Event;
using Utils.RefValues;

namespace SoapCutting_Minigame.Scripts
{
    public class SoapController : MonoBehaviour
    {
        [SerializeField] private BoolRef IsSwipe;
        [SerializeField] private float MaxSwipeAmount;
        [SerializeField] private GameEvent LevelCompleted;
        private Vector2 _startPos = Vector2.zero;
        private Vector2 _endPos = Vector2.zero;
        private bool _isHandleInput = true;


        
        [SerializeField] private SoapLayer[] SoapLayers;
        [SerializeField] private Transform KnifeTransform;
        [SerializeField] private float CutSpeed;
        [SerializeField] private ParticleSystem SoapFx;
        private int _soapLayerIndex;
        private Vector3 _knifeInitPosition;
        private Vector3 _knifeTargetPosition;
        private float _knifeLerpTime;

        private void Start()
        {
            Initialize();
        }

        private void HandleInput()
        {
            if (!_isHandleInput)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                _startPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                _endPos = Input.mousePosition;
                IsSwipe.Value = GetMoveType(_startPos, _endPos);
            }

        }
        private void Update()
        {
            //HandleInput();
            if (Input.GetMouseButtonDown(0))
            {
                SoapFx.Play();
            }
            else if (Input.GetMouseButton(0))
            {
                KnifeTransform.position = Vector3.Lerp(_knifeInitPosition, _knifeTargetPosition, _knifeLerpTime);
                _knifeLerpTime += Time.deltaTime * CutSpeed;
                if (_knifeLerpTime >= 1f)
                {
                    _soapLayerIndex++;
                    _knifeLerpTime = 0f;
                    SetLayer();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SoapFx.Stop();
            }
        }
        
        private void Initialize()
        {
            _soapLayerIndex = 0;
            _knifeInitPosition = KnifeTransform.position;
            _knifeTargetPosition = KnifeTransform.position - KnifeTransform.right * 1.2f;
            SetLayer();
        }

        

        private bool GetMoveType(Vector2 startPos, Vector2 endPos)
        {
            Vector2 currentSwipe = (endPos - startPos).normalized;
            
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                return true;

            return false;
        }

        public void SetLayer()
        {
            _isHandleInput = true;
            
            if (_soapLayerIndex >= SoapLayers.Length)
            {
                LevelCompleted.Raise();
                return;
            }
            KnifeTransform.position = _knifeInitPosition;
        }
    }
}