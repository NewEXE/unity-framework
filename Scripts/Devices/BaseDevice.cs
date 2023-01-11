using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float operateDistance = 3.5f;

    private void OnMouseUp()
    {
        Vector3 playerPosition = this.player.transform.position;

        // vertical correction so the direction won't point up or down
        playerPosition.y = this.transform.position.y;

        if (!(Vector3.Distance(this.transform.position, playerPosition) < this.operateDistance)) {
            return;
        }

        Vector3 direction = this.transform.position - playerPosition;
        if (Vector3.Dot(player.transform.forward, direction.normalized) > .5f) {
            this.Operate();
        }
    }

    protected virtual void Operate()
    {
        // behavior of the specific device
    }
}
