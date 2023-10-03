using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] int gridSizeX, gridSizeY, holeNumber, dieNumber;
    [SerializeField] GameObject basicTile;
    [SerializeField] GameObject hole;
    [SerializeField] GameObject die;
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
    // Please ensure that the level's txt file is COPY-PASTED INTO the "StreamingAssets" folder.
    // If not, the level will NOT spawn in the build.
    void GenerateGrid()
    {
        int currentX = 0;
        int currentY = 0;
        string pathnew = Path.Combine(Application.streamingAssetsPath, level.name + ".txt");
        string path = "Assets/Levels/"+level.name+".txt";
        StreamReader reader = new StreamReader(pathnew);
        string line;
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
                    DieBehavior dicetemp = go2.transform.GetChild(0).GetComponent<DieBehavior>();
                    dicetemp.SetStartingPosition(new Vector3(transform.position.x + currentX, transform.position.y + 0.5f,
                        transform.position.z + currentY));
                    dicetemp.SetMoveLimit(dieNumber);
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
                currentX += 1; // TODO: variable should be the size of the tile, not hardcoded.
            }
            currentX = 0;
            currentY += 1; // TODO: variable should be the size of the tile, not hardcoded
        }
        reader.Close();
    }
}