using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Die : MonoBehaviour
{
    public int Moves;
    public int GridSize;
    private bool mLastFrameUp = false;
    private bool mLastFrameDown = false;
    private bool mLastFrameLeft = false;
    private bool mLastFrameRight = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(GridSize / 2, 0.5f, GridSize / 2); //0.5f should be half the size in z direction
    }

    // Update is called once per frame
    void Update()
    {
        // if there are no moves left for the current die, destroy it
        if (Moves <= 0)
        {
            Destroy(this.gameObject);
        }

        // move on leading edge
        int verticalMove = 0;
        int horizontalMove = 0;
        if (!mLastFrameUp && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            Moves--;
            verticalMove++;
        }
        if (!mLastFrameDown && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            Moves--;
            verticalMove--;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + verticalMove);

        if (!mLastFrameLeft && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            Moves--;
            horizontalMove--;
        }
        if (!mLastFrameRight && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            Moves--;
            horizontalMove++;
        }
        transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z);

        mLastFrameUp = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
        mLastFrameDown = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
        mLastFrameLeft = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));
        mLastFrameRight = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));

        // Show numbers on the die
        // NOTE - Amari: I can do this, yes

        // border check

        // Add system (if collide with another die, destroy the other and add its moves to current one)
        // NOTE - Amari: I think instead of destroying dice, the other die simply stops at it's last position. 
        // Also in order to do that we'll have have a simple "manager" object that switches the current die.

        // Collision with hole (destroy current object, subtract from hole requirement) (Should be in hole scripts)
    }
}
