using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Die : MonoBehaviour
{
    public int Moves;
    //public TextMeshPro moveNumText;
    [SerializeField] GameObject mvs;
    [SerializeField] TextMeshPro moveDisplay;
    public int GridSize;

    // Start is called before the first frame update
    void Start()
    {
        //moveDisplay = mvs.GetComponent<TextMeshPro>();
        transform.position = new Vector3(GridSize / 2, 0.5f, GridSize / 2); //0.5f should be half the die size in z direction
    }

    // Update is called once per frame
    void Update()
    {
        //moveDisplay.text = "Moves: " + Moves.ToString();
        if (Moves > 0)
        {
            float verticalMove = 0.0f;
            float horizontalMove = 0.0f;
            // vertical movement
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                // boundary check
                if (transform.position.z + 1 < GridSize)
                {
                    Moves--;
                    verticalMove = 1.0f;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // boundary check
                if (transform.position.z + 1 >= 0   )
                {
                    Moves--;
                    verticalMove = -1.0f;
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + verticalMove);
            // horizontal movement
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                // boundary check
                if (transform.position.x - 1 >= 0)
                {
                    Moves--;
                    horizontalMove = -1.0f;
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                // boundary check
                if (transform.position.x + 1 < GridSize)
                {
                    Moves--;
                    horizontalMove = 1.0f;
                }
            }
            transform.position = new Vector3(transform.position.x + horizontalMove, transform.position.y, transform.position.z);
        }

        // Show numbers on the die
        // NOTE - Amari: I can do this, yes

        // Add system (if collide with another die, destroy the other and add its moves to current one)
        // NOTE - Amari: I think instead of destroying dice, the other die simply stops at it's last position. 
        // Also in order to do that we'll have have a simple "manager" object that switches the current die.

        // Collision with hole (destroy current object, subtract from hole requirement) (Should be in hole scripts)
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("akdlsdg;j");
        Destroy(gameObject);
    }
}
