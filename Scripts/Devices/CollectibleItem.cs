using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Management;

namespace Game.Devices
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] private string itemName;

        private void OnTriggerEnter(Collider other) {
            // Managers.Inventory.AddItem(this.itemName);
            Destroy(this.gameObject);
        }
    }

}