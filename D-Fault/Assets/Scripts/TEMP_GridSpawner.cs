using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMP_GridSpawner : MonoBehaviour
{
    [SerializeField] int holeNumber;
    [SerializeField] GameObject basicTile, hole, die, coin, wall;
    [SerializeField] int[] diceValues;
    [SerializeField] TextAsset level;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        List<TEMP_DieBehavior> diceInLevel = new List<TEMP_DieBehavior>();
        string pathnew = Path.Combine(Application.streamingAssetsPath, level.name + ".txt");
        string path = "Assets/Levels/"+level.name+".txt";
        StreamReader reader = new StreamReader(pathnew);
        string line = "";
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            for (int i = 0; i < line.Length; i++)
            {
                GameObject go;
                // spawn a tile
                if (line[i] == '.')
                {
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                }
                // spawn a die and a tile
                else if (line[i] == 'D')
                {
                    // spawn tile
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // spawn die, set it's starting position and move limit
                    GameObject go2 = Instantiate(die);
                    TEMP_DieBehavior dicetemp = go2.transform.GetChild(0).GetComponent<TEMP_DieBehavior>();
                    dicetemp.SetStartingPosition(new Vector3(transform.position.x + currentX, transform.position.y + 0.5f,
                        transform.position.z + currentY));
                    dicetemp.SetMoveLimit(diceValues[dieCounter]);
                    dieCounter++;
                    diceInLevel.Add(dicetemp);
                }
                // spawn a coin and a tile
                else if (line[i] == 'C')
                {
                    // spawn tile
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // spawn coin, set it's starting position and move limit
                    GameObject go2 = Instantiate(coin);
                    TEMP_DieBehavior cointemp = go2.transform.GetChild(0).GetComponent<TEMP_DieBehavior>();
                    cointemp.SetStartingPosition(new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY));
                    cointemp.SetMoveLimit(diceValues[dieCounter]);
                    dieCounter++;
                    diceInLevel.Add(cointemp);
                }
                // spawn a hole
                else if (line[i] == 'H')
                {
                    go = Instantiate(hole, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    HoleBehavior holetemp = go.GetComponent<HoleBehavior>();
                    holetemp.SetHoleCount(holeNumber);
                }
                // spawn a wall
                else if (line[i] == 'W')
                {
                    // spawn tile first
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // spawn wall
                    GameObject go2 = Instantiate(wall);
                    go2.transform.position = new Vector3(transform.position.x + currentX, transform.position.y + 0.5f,
                        transform.position.z + currentY);
                }
                currentX += 1; // TODO: variable should be the size of the tile, not hardcoded.
            }
            currentX = 0;
            currentY += 1; // TODO: variable should be the size of the tile, not hardcoded
        }

        foreach(TEMP_DieBehavior d in diceInLevel)
        {
            d.SetGridSize(line.Length, currentY);
        }

        reader.Close();
    }
}
