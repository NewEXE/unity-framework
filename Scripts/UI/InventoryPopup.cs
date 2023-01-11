using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryPopup : MonoBehaviour {
	private string pickedItem;

	public void Refresh() {
		// List<string> itemList = Managers.Inventory.GetItemList();

		// display inventory items...
	}
	
	public void OnItem(string item) {
		this.pickedItem = item;
		this.Refresh();
	}

	public void OnEquip() {
		// Managers.Inventory.EquipItem(this.pickedItem);
		this.Refresh();
	}

	public void OnUse() {
		// Managers.Inventory.ConsumeItem(this.pickedItem);
		if (this.pickedItem == "health") {
			// Managers.Player.ChangeHealth(25);
		}

		this.Refresh();
	}
}
