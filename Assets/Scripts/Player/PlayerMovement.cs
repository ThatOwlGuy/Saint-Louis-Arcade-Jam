using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHelper))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    #region Variables
    private InputHelper input;
    private Rigidbody2D rb;
    public bool reactToDamage;
    public float speed;
    #endregion END: Variables

    private void Start()
    {
        input = GetComponent<InputHelper>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Takes in the inptu every frame
    public void Update(){

        //if we're supposed to react to damage and we're in recovery, don'tmove
        if (reactToDamage)
            if (GetComponent<Health>().inRecovery)
                return;

        rb.velocity = GetInputVelocity() * speed;
    }

    public Vector2 GetInputVelocity()
    {
        Vector2 newVelocity = new Vector2();

        if (Input.GetKey(input.up))
            newVelocity += Vector2.up;

        if (Input.GetKey(input.down))
            newVelocity += Vector2.down;

        if (Input.GetKey(input.left))
            newVelocity += Vector2.left;

        if (Input.GetKey(input.right))
            newVelocity += Vector2.right;

        /*if (newVelocity != Vector2.zero)
            print("Pressing buttons!");*/

        return newVelocity;
    }
}
