using UnityEngine;
using Utils.RefValues;

namespace SoapCutting_Minigame.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private BoolRef IsSwipe;
        private Vector2 _startPos = Vector2.zero;
        private Vector2 _endPos = Vector2.zero;
        private bool _isHandleInput = true;
        
        private void Update()
        {
            HandleInput();
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
            if (Input.GetMouseButtonUp(0))
            {
                IsSwipe.Value = false;
            }
        }
        
        private bool GetMoveType(Vector2 startPos, Vector2 endPos)
        {
            Vector2 currentSwipe = (endPos - startPos).normalized;

            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return true;
            }
            return false;
        }
        
        public void OnLevelCompleted()
        {
            _isHandleInput = false;
        }
    }
}