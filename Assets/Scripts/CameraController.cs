using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        public Transform target;
        public float distance = 3.0f;
        public float height = 3.0f;
        public float damping = 5.0f;
        public bool smoothRotation = true;
        public bool followBehind = true;
        public float rotationDamping = 10.0f;

        void Awake() {
                if(target == null)
                {
                        target = transform.parent;
                }
        }

        void FixedUpdate () {
               Vector3 wantedPosition;
               if(followBehind)
                       wantedPosition = target.TransformPoint(0, height, -distance);
               else
                       wantedPosition = target.TransformPoint(0, height, distance);

               transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

               if (smoothRotation) {
                       Quaternion wantedRotation = Quaternion.LookRotation(new Vector3(target.position.x, target.position.y + 3, target.position.z) - transform.position, target.up);
                       transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
               }
               else transform.LookAt (target, target.up);
         }
}