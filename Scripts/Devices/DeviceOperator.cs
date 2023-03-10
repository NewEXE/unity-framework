using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Devices
{
    public class DeviceOperator : MonoBehaviour
    {
        public float radius = 1.5f;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) {
                return;
            }

            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, this.radius);
            foreach (Collider hitCollider in hitColliders) {
                Vector3 hitPosition = hitCollider.transform.position;

                // vertical correction so the direction won't point up or down
                hitPosition.y = this.transform.position.y;

                Vector3 direction = hitPosition - this.transform.position;
                if (Vector3.Dot(this.transform.forward, direction.normalized) > .5f) {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
