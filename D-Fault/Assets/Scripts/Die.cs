using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Die : MonoBehaviour
{
    public int Moves;
    //public TextMeshPro moveNumText;
    [SerializeField] GameObject mvs;
    [SerializeField] TextMeshPro moveDisplay;
    public int GridSize;

    // check selected
    bool mIsSelected = false;
    public Material red, white;

    // Start is called before the first frame update
    void Start()
    {
        //moveDisplay = mvs.GetComponent<TextMeshPro>();
        transform.position = new Vector3(GridSize / 2, 0.5f, GridSize / 2); //0.5f should be half the die size in z direction
        this.GetComponent<Renderer>().material = white;
    }

    // Update is called once per frame
    void Update()
    {
        //moveDisplay.text = "Moves: " + Moves.ToString();
        if (Moves > 0 && mIsSelected)
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

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("akdlsdg;j");
        
        if (!mIsSelected)
        {
            Die otherDie = other.gameObject.GetComponent<Die>();
            otherDie.Moves += Moves;
            Destroy(gameObject);
        }
        
    }*/

    private void OnMouseDown()
    {
        // select if unselected, vice versa
        SetSelection(!mIsSelected);
        // if selected the current die, unselect all other dice
        if (mIsSelected)
        {
            // find all dice
            GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
            // for each die, if it is not the current die, unselect it.
            foreach (var d in dice) 
            {
                Die die = d.GetComponent<Die>();
                if (die != this) 
                {
                    die.SetSelection(false);
                }
            }
        }
    }

    public void SetSelection(bool selected)
    {
        mIsSelected = selected;
        if (mIsSelected)
        {
            this.GetComponent<Renderer>().material = red;
        }
        else
        {
            this.GetComponent<Renderer>().material = white;
        }
    }
}
