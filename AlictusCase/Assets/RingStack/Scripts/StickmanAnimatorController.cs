using System;
using UnityEngine;

namespace RingStack.Scripts
{
    public class StickmanAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int IsDance = Animator.StringToHash("Dance");
        private static readonly int IsStuck = Animator.StringToHash("Stuck");

        // Private Functions
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        // Public Functions
        public void Dance()
        {
            _animator.SetBool(IsDance, true);
        }

        public void Stuck()
        {
            _animator.SetTrigger(IsStuck);
        }
    }
}