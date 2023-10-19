using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieBehavior : MonoBehaviour
{   
    public Animator anim;
    public GameObject dieParent;
    public Material red, white;
    [SerializeField] MeshRenderer cubeRenderer;
    AudioSource sfx;
    [SerializeField] AudioClip addSound, moveSound;
    public int Moves;
    public int moveDistance;
    public float defaultHeight;

    protected bool mCanMove;
    protected bool mIsSelected = false;
    protected int GridSizeX, GridSizeY;
    protected Vector3 mStoredMove;
    protected Vector3 mPreviousStoredMove;
    protected Vector3 mStoredRotationVector;

    // Start is called before the first frame update
    protected void Start()
    {
        anim = GetComponent<Animator>();
        mCanMove = true;
        sfx = GetComponent<AudioSource>();
    }

    protected void Awake()
    {
        MoveNumberUpdate(false);
    }

    // Update is called once per frame
    protected virtual void Update()
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
                if (dieParent.transform.position.z + moveDistance < GridSizeY)
                {
                    Moves--;
                    verticalMove = 1.0f;
                    anim.SetBool("Move", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                // boundary check
                if (dieParent.transform.position.z - moveDistance >= 0.0f)
                {
                    Moves--;
                    verticalMove = -1.0f;
                    anim.SetBool("Move", true);
                }
            }
            
            // horizontal movement
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                // boundary check
                if (dieParent.transform.position.x - moveDistance >= 0.0f)
                {
                    Moves--;
                    horizontalMove = -1.0f;
                    anim.SetBool("Move", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                // boundary check
                if (dieParent.transform.position.x + moveDistance < GridSizeX)
                {
                    Moves--;
                    horizontalMove = 1.0f;
                    anim.SetBool("Move", true);
                }
            }

            // mStoredRotationVector sets rotation of dieParent to align animation with direction of movement.
            // mStoredMove sets the position of where the dieParent will move to after animation ends
            // (the "real" position of dieParent doesn't update until end of animation).
            mStoredRotationVector = new Vector3(horizontalMove, 0, verticalMove); 
            mStoredMove = new Vector3(transform.position.x + horizontalMove * moveDistance, defaultHeight, transform.position.z + verticalMove * moveDistance);
        }
    }

    // Called by Animation events in the die's animations, and by the GridSpawner at Start.
    // uses an int parameter instead of a bool because AnimationEvents cannot call bool parameters
    public void SetCanMove(int move)
    {
        if(move == 0)
        {
            // die is prevented from inputting a move (called during any other animation)
            mCanMove = false;
            dieParent.transform.rotation = Quaternion.LookRotation(mStoredRotationVector.normalized, Vector3.up);
            MoveNumberUpdate(true);
        }
        else
        {
            // die is allowed to move (called when the die is in idle animation)
            mCanMove = true;
            MoveToNewPosition();
            MoveNumberUpdate(false);
        }
    }

    protected void OnMouseDown()
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

            // find all coins
            GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
            // for each coin, if it is not the current coin, unselect it.
            foreach (var c in coins)
            {
                DieBehavior coin = c.GetComponent<DieBehavior>();
                if (coin != this)
                {
                    coin.SetSelection(false);
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
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
        else
        {
            cubeRenderer.material = white;
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // If another die collides with this one while not selected, it gets destroyed.
        if (other.CompareTag("Dice"))
        {
            if (!mIsSelected) { 
                DieBehavior otherDie = other.gameObject.GetComponent<DieBehavior>();
                otherDie.Moves += Moves;
                otherDie.anim.SetTrigger("Add");
                //print("Die added");
                SelfDestruct();
            } else if (mIsSelected && gameObject.CompareTag("Coin"))
            {
                anim.SetTrigger("Add");
                DieBehavior otherDie = other.gameObject.GetComponent<DieBehavior>();
                otherDie.Moves += Moves;
                otherDie.anim.SetTrigger("Add");
            }
        }

        if (other.CompareTag("Hole"))
        {
            anim.SetTrigger("Score");
        }

        if (other.CompareTag("Wall"))
        {
            //print("rebound!!");
            anim.SetTrigger("Rebound");
        }

        
        if (!mIsSelected && other.CompareTag("Coin") && gameObject.CompareTag("Coin"))
        {
            DieBehavior otherDie = other.gameObject.GetComponent<DieBehavior>();
            otherDie.Moves += Moves;
            anim.SetTrigger("Add");
            //print("Die added");
        }
        
    }

    protected void SelfDestruct()
    {
        Destroy(dieParent);
    }

    // Called by Animation events in the die's animation (do not call in code).
    protected void PlaySFX(int sound)
    {
        sfx.pitch = 1f;
        if (sound == 0)
        {
            sfx.PlayOneShot(moveSound);
        } else if (sound == 1)
        {
            sfx.PlayOneShot(addSound);
        }
        else if (sound == 2)
        {
            //moveDeny / Rebound sound
            sfx.pitch = 0.5f;
            sfx.PlayOneShot(moveSound);
        }
    }

    // Updates the number sprites on the die to reflect # of Moves
    // by looping through NumberDisplay components in children
    // if "True" is passed through as a parameter, the sprites will lock their rotation
    // to avoid graphical errors.
    protected void MoveNumberUpdate(bool shouldRotationLock)
    {
        Transform dice = transform.GetChild(0);
        foreach (Transform child in dice)
        {
            NumberDisplay numdis;
            Billboard bill;
            if (child.gameObject.TryGetComponent<NumberDisplay>(out numdis))
            {
                numdis.UpdateNumber(Moves);
            }

            if (child.gameObject.TryGetComponent<Billboard>(out bill))
            {
                bill.LockRotation(shouldRotationLock);
            }
        }
    }

    // Sets the starting position of the die. Called by GridSpawner.
    public void SetStartingPosition(Vector3 setmove)
    {
        mStoredMove = setmove;
        SetCanMove(1);
    }

    // Sets the die's number of moves. Called by GridSpawner.
    public void SetMoveLimit(int movelimit)
    {
        print(movelimit);
        Moves = movelimit;
        MoveNumberUpdate(false);
    }

    //Sets the "grid size" so the dice doesn't go off the grid. Called by Grid Spawner.
    public void SetGridSize(int gridX, int gridY)
    {
        GridSizeX = gridX;
        GridSizeY = gridY;
    }

    public void RestorePreviousPosition()
    {
        mStoredMove = mPreviousStoredMove;
        Moves++;
        MoveNumberUpdate(false);
    }

    public void MoveToNewPosition()
    {
        mPreviousStoredMove = mStoredMove;
        dieParent.transform.position = mStoredMove;
    }
}
