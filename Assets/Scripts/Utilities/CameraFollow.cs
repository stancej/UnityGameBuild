using System;
using UnityEngine;

namespace Utilities
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] private Transform target;
        [Range(0,1)][SerializeField] private float followSpeed = 0.125f;
        [SerializeField] private Vector3 offset;

        private void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed);
            transform.position = smoothedPosition;
            
            transform.LookAt(target);
        }
    }
}
