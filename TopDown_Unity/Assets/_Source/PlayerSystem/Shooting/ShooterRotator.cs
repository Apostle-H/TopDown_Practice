using UnityEngine;

namespace PlayerSystem.Shooting
{
    public class ShooterRotator
    {
        private Transform _pivotPoint;

        public ShooterRotator(Transform pivotPoint)
        {
            _pivotPoint = pivotPoint;
        }
        
        public void Rotate(float rotation)
        {
            _pivotPoint.eulerAngles = new Vector3(0f, 0f, rotation);
        }
    }
}