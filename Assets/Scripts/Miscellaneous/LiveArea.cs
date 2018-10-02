using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LiveArea : MonoBehaviour {

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Vector3 direction = (Vector3.zero - other.transform.position).normalized;
            other.transform.position = other.transform.position + direction * 0.75f;
        }else
        {
            Destroy(other.gameObject);
        }
    }
}
