using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] int[] holeValues;
    [SerializeField] GameObject basicTile;
    [SerializeField] GameObject greyTile;
    [SerializeField] GameObject hole;
    [SerializeField] GameObject die;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject boardCorner;
    [SerializeField] GameObject boardEdge;
    [SerializeField] int[] diceValues;
    //[SerializeField] TextAsset level;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LevelSelect");
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    // Generates the level's grid from a text file.
    // !! IMPORTANT NOTE !!
    // Please ensure that the level's .txt file is COPY-PASTED INTO the "StreamingAssets" folder.
    // If not, the level will NOT spawn in the build.
    void GenerateGrid()
    {
        int currentX = 0;
        int currentY = 0;
        int dieCounter = 0;
        List<DieBehavior> diceInLevel = new List<DieBehavior>();
        int holeCounter = 0;
        List<HoleBehavior> holesInLevel = new List<HoleBehavior>();
        string path = Path.Combine(Application.streamingAssetsPath, SceneManager.GetActiveScene().name + ".txt");
        StreamReader reader = new StreamReader(path);
        string line = "";
        while (!reader.EndOfStream)
        {
            currentX = 0;
            line = reader.ReadLine();
            for (int i = 0; i < line.Length; i++)
            {
                
                GameObject go;
                // spawn a tile
                if (line[i] == '.')
                {
                    // chessboard pattern
                    // NOTE: If the grid size changes, should not be using currentX and currentY
                    if (currentX % 2 == 0)
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(basicTile, transform);
                        }
                        else
                        {
                            go = Instantiate(greyTile, transform);
                        }
                    }
                    else
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(greyTile, transform);
                        }
                        else
                        {
                            go = Instantiate(basicTile, transform);
                        }
                    }
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                            transform.position.z + currentY);
                }
                // spawn a die and a tile
                else if (line[i] == 'D')
                {
                    // spawn tile
                    if (currentX % 2 == 0)
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(basicTile, transform);
                        }
                        else
                        {
                            go = Instantiate(greyTile, transform);
                        }
                    }
                    else
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(greyTile, transform);
                        }
                        else
                        {
                            go = Instantiate(basicTile, transform);
                        }
                    }
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // spawn die, set it's starting position and move limit
                    GameObject go2 = Instantiate(die);
                    DieBehavior dicetemp = go2.transform.GetChild(0).GetComponent<DieBehavior>();
                    dicetemp.SetStartingPosition(new Vector3(transform.position.x + currentX, transform.position.y + 0.5f,
                        transform.position.z + currentY));
                    dicetemp.SetMoveLimit(diceValues[dieCounter]);
                    dieCounter++;
                    diceInLevel.Add(dicetemp);

                    // add the die to eventsystem
                    GameEvent es = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEvent>();
                    es.mDice.Add(dicetemp);
                }
                // spawn a hole
                else if (line[i] == 'H')
                {
                    go = Instantiate(hole, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    HoleBehavior holetemp = go.GetComponent<HoleBehavior>();

                    // set hole values
                    holetemp.SetHoleCount(holeValues[holeCounter]);
                    holeCounter++;
                    holesInLevel.Add(holetemp);

                    // add the hole to eventsystem
                    GameEvent es = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEvent>();
                    es.mHoles.Add(holetemp);
                }
                // spawn a wall
                else if (line[i] == 'W')
                {
                    // spawn tile first
                    if (currentX % 2 == 0)
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(basicTile, transform);
                        }
                        else
                        {
                            go = Instantiate(greyTile, transform);
                        }
                    }
                    else
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(greyTile, transform);
                        }
                        else
                        {
                            go = Instantiate(basicTile, transform);
                        }
                    }
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // spawn wall
                    GameObject go2 = Instantiate(wall);
                    go2.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                }
                else if (line[i] == 'C')
                {
                    // spawn tile first
                    if (currentX % 2 == 0)
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(basicTile, transform);
                        }
                        else
                        {
                            go = Instantiate(greyTile, transform);
                        }
                    }
                    else
                    {
                        if (currentY % 2 == 0)
                        {
                            go = Instantiate(greyTile, transform);
                        }
                        else
                        {
                            go = Instantiate(basicTile, transform);
                        }
                    }
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // spawn coin
                    GameObject go2 = Instantiate(coin);
                    DieBehavior cointemp = go2.transform.GetChild(0).GetComponent<DieBehavior>();
                    cointemp.SetStartingPosition(new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY));
                    cointemp.SetMoveLimit(diceValues[dieCounter]);
                    dieCounter++;
                    diceInLevel.Add(cointemp);

                    // add the coin to eventsystem
                    GameEvent es = GameObject.FindGameObjectWithTag("GameEvent").GetComponent<GameEvent>();
                    es.mDice.Add(cointemp);
                }
                currentX += 1; // TODO: variable should be the size of the tile, not hardcoded.
            }
            currentY += 1; // TODO: variable should be the size of the tile, not hardcoded
        }
        // Genereate the visual border of the game board
        GenerateBorder(currentX, currentY);

        foreach (DieBehavior d in diceInLevel)
        {
            d.SetGridSize(line.Length, currentY);
        }

        GameObject camPivot = GameObject.Find("Camera Pivot");
        print("CurX: " + currentX + ", CurY: " + currentY);
        if (currentX % 2 == 1)
        {
            currentX -= 1;
        }
        else
        {
            currentX -= 1;
        }

        if (currentY % 2 == 1)
        {
            currentY -= 1;
        }
        else
        {
            currentY -= 1;
        }
        print("CurX: " + currentX + ", CurY: " + currentY);
        camPivot.transform.position = new Vector3((float)currentX / 2f, 0, (float)currentY / 2f);
        if(bg != null) { 
            GameObject background = Instantiate(bg);
            background.transform.position = new Vector3((float)currentX / 2f, -1.2f, (float)currentY / 2f);
        }
        reader.Close();
    }


    // Generates the board's border in each level.
    void GenerateBorder(int boardX, int boardY)
    {
        GameObject objectToSpawn = null;
        Vector3 spawnPoint = new Vector3(0,0,0);
        Quaternion rotationValue = new Quaternion(0, 0, 0, 0);
        for (int y = -1; y < boardY+1; y++){
            for (int x = -1; x < boardX+1; x++)
            {
                objectToSpawn = null;
                if(x == -1)
                {
                    if(y == -1) {
                        objectToSpawn = boardCorner;
                        rotationValue = new Quaternion(-90,180,0,0);
                    } else if (y == boardY) {
                        objectToSpawn = boardCorner;
                        rotationValue =  new Quaternion(-90,0,-90,0);
                    } else {
                        objectToSpawn = boardEdge;
                        rotationValue = new Quaternion(-90, 0, -90, 0);
                    }
                } 
                else if (x == boardX)
                {
                    if (y == -1) {
                        objectToSpawn = boardCorner;
                        rotationValue = new Quaternion(-90, 0, 90, 0);
                    } else if (y == boardY) {
                        objectToSpawn = boardCorner;
                        rotationValue = new Quaternion(-90, 0, 0, 0);
                    } else {
                        objectToSpawn = boardEdge;
                        rotationValue = new Quaternion(-90, 0, 90, 0);
                    }
                } 
                else
                {
                    if (y == -1) {
                        objectToSpawn = boardEdge;
                        //rotationValue = new Quaternion(-90,180,0,0);
                    } else if (y == boardY) {
                        objectToSpawn = boardEdge;
                        rotationValue = new Quaternion(-90, 0, 0, 0);
                    }
                }
                spawnPoint = new Vector3(transform.position.x + x, -0.25f, transform.position.y + y);
                if(objectToSpawn != null) { 
                    print(rotationValue);
                    GameObject go = Instantiate(objectToSpawn, transform);
                    go.transform.position = spawnPoint;
                    go.transform.rotation = Quaternion.Euler(rotationValue.x, rotationValue.y, rotationValue.z);
                    //go.transform.rotation = rotationValue;
                }
            }
        }
    }
}