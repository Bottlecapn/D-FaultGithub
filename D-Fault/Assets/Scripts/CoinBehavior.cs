using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinBehavior : DieBehavior
{
    // Update is called once per frame
    protected override void Update()
    {
        // reset the animation boolean parameter, to ensure that die do not play move anim multiple times. 
        anim.SetBool("Move", false);

        if (Moves > 0 && mCanMove && mIsSelected)
        {
            float verticalMove = 0.0f;
            float horizontalMove = 0.0f;
            // vertical movement
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // boundary check
                if (dieParent.transform.position.z + 2.0f < GridSizeY)
                {
                    Moves--;
                    verticalMove = 2.0f;
                    anim.SetBool("Move", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // boundary check
                if (dieParent.transform.position.z - 2.0f >= 0.0f)
                {
                    Moves--;
                    verticalMove = -2.0f;
                    anim.SetBool("Move", true);
                }
            }

            // horizontal movement
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                // boundary check
                if (dieParent.transform.position.x - 2.0f >= 0.0f)
                {
                    Moves--;
                    horizontalMove = -2.0f;
                    anim.SetBool("Move", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                // boundary check
                if (dieParent.transform.position.x + 2.0f < GridSizeX)
                {
                    Moves--;
                    horizontalMove = 2.0f;
                    anim.SetBool("Move", true);
                }
            }

            // mStoredRotationVector sets rotation of dieParent to align animation with direction of movement.
            // mStoredMove sets the position of where the dieParent will move to after animation ends
            // (the "real" position of dieParent doesn't update until end of animation).
            mStoredRotationVector = new Vector3(horizontalMove, 0, verticalMove);
            mStoredMove = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z + verticalMove);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }
}
