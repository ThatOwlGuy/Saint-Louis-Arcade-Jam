using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveZoneManagement : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Vector3 direction = -other.transform.position;
            direction = direction.normalized;

            other.transform.position += direction;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
