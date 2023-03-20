using UnityEngine;

namespace EntitySystem.Shooting
{
    public class Rotator
    {
        private readonly Transform _pivotPoint;

        public Rotator(Transform pivotPoint)
        {
            _pivotPoint = pivotPoint;
        }
        
        public void Rotate(float rotation)
        {
            _pivotPoint.eulerAngles = new Vector3(0f, 0f, rotation);
        }
    }
}