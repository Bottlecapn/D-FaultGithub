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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
            currentX += 1;
        }

        if (currentY % 2 == 1)
        {
            currentY -= 1;
        }
        else
        {
            currentY += 1;
        }
        camPivot.transform.position = new Vector3(currentX / 2, 0, currentY / 2);
        reader.Close();
    }
}
