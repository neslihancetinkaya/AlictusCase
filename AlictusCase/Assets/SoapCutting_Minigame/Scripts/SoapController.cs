using System;
using UnityEngine;
using Utils.Event;
using Utils.RefValues;

namespace SoapCutting_Minigame.Scripts
{
    public class SoapController : MonoBehaviour
    {
        [SerializeField] private BoolRef IsSwipe;
        [SerializeField] private GameEvent LevelCompleted;
        [SerializeField] private GameEvent LayerCompleted;
        
        [SerializeField] private SoapLayer[] SoapLayers;
        private int _soapLayerIndex;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _soapLayerIndex = 0;
            SetLayer();
        }

        private void SetLayer()
        {
            //_isHandleInput = true;
            
            if (_soapLayerIndex >= SoapLayers.Length)
            {
                LevelCompleted.Raise();
                return;
            }
            
        }

        public void OnLayerCompleted()
        {
            SetLayer();
            _soapLayerIndex++;
        }

    }
}