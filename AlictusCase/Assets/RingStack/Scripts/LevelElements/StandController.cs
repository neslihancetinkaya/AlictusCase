using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils.Event;

namespace RingStack.Scripts.LevelElements
{
    public class StandController : MonoBehaviour
    {
        [SerializeField] private List<RingController> Rings;
        [SerializeField] private float OffsetFactor = 1.25f;
        [SerializeField] private float JumpOffset = 4f;
        [SerializeField] private GameEvent StandCompleted;
        [SerializeField] private GhostRingController GhostRing;
        [SerializeField] private int Capacity;
        [SerializeField] private StickmanAnimatorController AnimatorController;
        private List<RingController> _rings;
        private bool _isBusy;
        private RingController _lastSelectedRing;

        // Unity Event Functions
        private void Start()
        {
            Initialize();
        }

        // Private Functions
        private bool CheckStandCompleted()
        {
            var color = _rings[0].GetColor();
            for (int i = 1; i < _rings.Count; i++)
            {
                if (color != _rings[i].GetColor())
                    return false;
            }

            return true;
        }
        private Vector3 GetRingPos()
        {
            return new Vector3(0, OffsetFactor * _rings.Count, 0);
        }

        private void Initialize()
        {
            _rings = new List<RingController>();
            for (int i = 0; i < Rings.Count; i++)
            {
                RingController ring = Instantiate(Rings[i], transform);
                ring.transform.localPosition = GetRingPos();
                _rings.Add(ring);
                SetGhostRingPos();
            }
            ShowGhostRing(false);
        }
        
        private void SetGhostRingPos()
        {
            GhostRing.transform.localPosition = GetRingPos();
        }
        
        // Public Functions
        public RingController SelectLastRing()
        {
            if (_isBusy)
                return null;
            
            if (_rings.Count < 1)
                return null;
            
            _lastSelectedRing = _rings[_rings.Count - 1];
            GhostRing.SetColor(_lastSelectedRing.GetColor());
            _lastSelectedRing.transform.position += Vector3.up * JumpOffset;
            return _lastSelectedRing;
        }

        public RingController GetLastRing()
        {
            if (_rings.Count == 0)
                return null;

            return _rings[_rings.Count - 1];
        }

        public void ReleaseLastRing()
        {
            _isBusy = true;
            Vector3 targetPos = new Vector3(0f, (_rings.Count - 1) * OffsetFactor, 0f);
            _lastSelectedRing.transform.localPosition = new Vector3(0f, targetPos.y + JumpOffset, 0f);
            _lastSelectedRing.transform.DOLocalMove(targetPos, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                _isBusy = false;
            }); 
        }

        public void RemoveRing(RingController ring)
        {
            _rings.Remove(ring);
            SetGhostRingPos();
        }

        public void AddRing(RingController ring)
        {
            _isBusy = true;
            if (Capacity <= _rings.Count)
                return;
            ring.transform.SetParent(transform);
            Vector3 targetPos = GetRingPos();
            ring.transform.localPosition = new Vector3(0f, targetPos.y + JumpOffset, 0f);
            _rings.Add(ring);
            SetGhostRingPos();
            ring.transform.DOLocalMove(targetPos, 0.75f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                _isBusy = false;
                if (Capacity == _rings.Count)
                {
                    if (CheckStandCompleted())
                    {
                        StandCompleted.Raise();
                    }
                }
            });
            
        }

        public void OnLevelCompleted()
        {
            AnimatorController.Dance();
        }
        
        public void ShowGhostRing(bool flag)
        {
            GhostRing.gameObject.SetActive(flag);
        }
        public bool isBusy
        {
            get
            {
                return _isBusy;
            }
        }

    }
}
