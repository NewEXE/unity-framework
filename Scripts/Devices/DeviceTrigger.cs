using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
	[SerializeField] private GameObject[] affectTargets;

	private void OnTriggerEnter(Collider other) {
		foreach (GameObject target in this.affectTargets) {
			target.SendMessage("Activate");
		}
	}

	private void OnTriggerExit(Collider other) {
		foreach (GameObject target in this.affectTargets) {
			target.SendMessage("Deactivate");
		}
	}
}
