using System;
using RingStack.Scripts.LevelElements;
using UnityEngine;
using Utils.Event;

namespace RingStack.Scripts
{
    public class RingStackManager : MonoBehaviour
    {
        [SerializeField] private int TargetStandCount;
        [SerializeField] private StandController[] Stands;
        [SerializeField] private GameEvent LevelCompleted;

        private StandController _selectedStand;
        private StandController _closestStand;
        private RingController _selectedRing;
        private int _completedStandCount;
        private Camera _mainCam;
        private float _zPos;
        private Vector3 _offset;
        private bool _isActive = true;

        // Unity Event Functions

        private void Start()
        {
            Initialize();
        }

        // Private Functions
        private void Update()
        {
            if(!_isActive)
                return;
            if (Input.GetMouseButtonDown(0) && _selectedRing == null)
            {
                TryGetRing();
            }

            if (Input.GetMouseButton(0) && _selectedRing != null)
            {
                MoveRing();
            }
            else if (Input.GetMouseButtonUp(0) && _selectedRing != null)
            {
                DropRing();
                ResetHolders();
            }
            
        }
        private void Initialize()
        {
            _mainCam = Camera.main;
        }
        private Vector3 GetWorldPoint()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _zPos;
            return _mainCam.ScreenToWorldPoint(mousePos);
        }

        private void TryGetRing()
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, 1 << 8))
            {
                _selectedStand = hit.collider.GetComponent<StandController>();
                if (_selectedStand.isBusy)
                {
                    _selectedStand = null;
                    return;
                }
                _selectedRing = _selectedStand.SelectLastRing();
                if (_selectedRing == null)
                {
                    return;
                }
                Vector3 ringPosition = _selectedRing.transform.position;
                _zPos = _mainCam.WorldToScreenPoint(ringPosition).z;
                _offset = ringPosition - GetWorldPoint();
            }
        }

        private void MoveRing()
        {
            _selectedRing.transform.position = _offset + GetWorldPoint();

            _closestStand?.ShowGhostRing(false);
            _closestStand = FindClosestStand();
            RingController closestLastRing = _closestStand.GetLastRing();
            if (closestLastRing == null || closestLastRing.GetColor() == _selectedRing.GetColor())
            {
                _closestStand.ShowGhostRing(true);
            }
        }

        private void DropRing()
        {
            _closestStand?.ShowGhostRing(false);
            RingController closestLastRing = _closestStand.GetLastRing();
            if (closestLastRing == null || _selectedRing.GetColor() == closestLastRing.GetColor())
            {
                _selectedStand.RemoveRing(_selectedRing);
                _closestStand.AddRing(_selectedRing);
            }
            else
            {
                _selectedStand.ReleaseLastRing();
            }
        }

        private void ResetHolders()
        {
            _closestStand = null;
            _selectedRing = null;
            _selectedStand = null;
        }
        
        private StandController FindClosestStand()
        {
            float minDistance = float.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < Stands.Length; i++)
            {
                StandController stand = Stands[i];
                if (_selectedStand == stand)
                {
                    continue;
                }

                float distance = (_selectedRing.transform.position - stand.transform.position).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minIndex = i;
                }
            }
            return Stands[minIndex];
        }
        
        // Public Functions
        public void OnStandCompleted()
        {
            _completedStandCount++;
            if (_completedStandCount == TargetStandCount)
            {
                LevelCompleted.Raise();
            }
        }



        public void SetActive(bool flag)
        {
            _isActive = flag;
        }

    }
}