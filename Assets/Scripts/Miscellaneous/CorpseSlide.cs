using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseSlide : MonoBehaviour {

    public float speed;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
    }
}
