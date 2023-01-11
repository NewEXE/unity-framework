using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : BaseDevice
{
	[SerializeField] private Vector3 dPos;

	private bool isOpen;

	protected override void Operate()
	{
		if (this.isOpen) {
			this.Deactivate();
		} else {
			this.Activate();
		}
	}

	public void Activate()
	{
		if (this.isOpen) {
			return;
		}

		Vector3 pos = this.transform.position + this.dPos;
		this.transform.position = pos;
		this.isOpen = true;
	}
	public void Deactivate()
	{
		if (!this.isOpen) {
			return;
		}

		Vector3 pos = this.transform.position - this.dPos;
		this.transform.position = pos;
		this.isOpen = false;
	}
}
