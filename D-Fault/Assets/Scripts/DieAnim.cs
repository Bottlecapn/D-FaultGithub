using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnim : MonoBehaviour
{
    bool canMove;
    Animator anim;
    public int Moves;
    public int GridSize;
    GameObject dieParent;
    Vector3 storedMove;
    Vector3 storedVector;
    // Start is called before the first frame update
    void Start()
    {
        dieParent = transform.parent.gameObject;
        anim = GetComponent<Animator>();
        canMove = true;
        dieParent.transform.position = new Vector3(GridSize / 2, 0.5f, GridSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: WHAT IF BOTH PRESSED AT THE SAME TIME?
        ///
        ///
        anim.SetBool("Move", false);
        if (Moves > 0 && canMove)
        {
            float verticalMove = 0.0f;
            float horizontalMove = 0.0f;
            // vertical movement
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // boundary check
                if (dieParent.transform.position.z + 1 < GridSize)
                {
                    Moves--;
                    verticalMove = 1.0f;
                    anim.SetBool("Move", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // boundary check
                if (dieParent.transform.position.z - 1 >= 0)
                {
                    Moves--;
                    verticalMove = -1.0f;
                    anim.SetBool("Move", true);
                }
            }
            //dieParent.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + verticalMove);
            // horizontal movement
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                // boundary check
                if (dieParent.transform.position.x - 1 >= 0)
                {
                    Moves--;
                    horizontalMove = -1.0f;
                    anim.SetBool("Move", true);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                // boundary check
                if (dieParent.transform.position.x + 1 < GridSize)
                {
                    Moves--;
                    horizontalMove = 1.0f;
                    anim.SetBool("Move", true);
                }
            }
            storedVector = new Vector3(horizontalMove, 0, verticalMove); 
            storedMove = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z + verticalMove);
            //dieParent.transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z + verticalMove);
        }
    }

    void SetCanMove(int tf)
    {
        if(tf == 0)
        {
            canMove = false;
            dieParent.transform.rotation = Quaternion.LookRotation(storedVector.normalized, Vector3.up);

        } else
        {
            canMove = true;
            dieParent.transform.position = storedMove;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            anim.SetTrigger("Score");
        }
    }

    void SelfDestruct()
    {
        Destroy(dieParent);
    }
}
