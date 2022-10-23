using UnityEngine;

namespace RingStack.Scripts.LevelElements
{
    
    public class RingController : MonoBehaviour
    {
        [SerializeField] private ColorType RingColor;
        
        // Public Functions
        public ColorType GetColor()
        {
            return RingColor;
        }
       
    }
}