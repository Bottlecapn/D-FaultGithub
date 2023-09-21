using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieBehavior : MonoBehaviour
{
    bool canMove;
    public Animator anim;
    public int Moves;
    public int GridSize;
    public GameObject dieParent;
    Vector3 storedMove;
    Vector3 storedVector;
    [SerializeField] MeshRenderer cubeRenderer;
    public TextMeshProUGUI moveNumber;
    AudioSource sfx;
    [SerializeField] AudioClip addSound, moveSound;

    public float x;
    public float y;
    public float z;

    bool mIsSelected = false;
    public Material red, white;
    // Start is called before the first frame update
    void Start()
    {
        //dieParent = transform.parent.gameObject;
        moveNumber.text = Moves.ToString();
        anim = GetComponent<Animator>();
        canMove = true;
        storedMove = new Vector3 (x, y, z);
        dieParent.transform.position = new Vector3(x, y, z);
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mIsSelected)
            moveNumber.text = Moves.ToString();
        //NOTE: WHAT IF BOTH PRESSED AT THE SAME TIME?
        ///
        ///
        anim.SetBool("Move", false);
        if (Moves > 0 && canMove && mIsSelected)
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
                DieBehavior die = d.GetComponent<DieBehavior>();
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
            cubeRenderer.material = red;
        }
        else
        {
            cubeRenderer.material = white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("AAAAAAAAAA");
        if (!mIsSelected && other.CompareTag("Dice"))
        {
            DieBehavior otherDie = other.gameObject.GetComponent<DieBehavior>();
            otherDie.Moves += Moves;
            otherDie.anim.SetTrigger("Add");
            SelfDestruct();
        }

        if (other.CompareTag("Hole"))
        {
            anim.SetTrigger("Score");
        }
    }

    void SelfDestruct()
    {
        Destroy(dieParent);
    }

    void PlaySFX(int sound)
    {
        if (sound == 0)
        {
            sfx.PlayOneShot(moveSound);
        } else if (sound == 1)
        {
            sfx.PlayOneShot(addSound);
        }
    }
}
