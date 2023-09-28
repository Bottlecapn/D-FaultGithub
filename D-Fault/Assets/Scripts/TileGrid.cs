using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileGrid : MonoBehaviour
{
    [SerializeField] int gridSizeX, gridSizeY, holeNumber, dieNumber;
    [SerializeField] GameObject basicTile;
    [SerializeField] GameObject hole;
    [SerializeField] GameObject die;
    [SerializeField] TextAsset level;
    //[SerializeField] Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        //tiles = new Tile[gridSizeX*gridSizeY];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GenerateGrid()
    {
        int currentX = 0;
        int currentY = 0;
        string path = "Assets/Levels/"+level.name+".txt";
        StreamReader reader = new StreamReader(path);
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
                    // tile
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    // die
                    GameObject go2 = Instantiate(die);
                    DieBehavior dicetemp = go2.transform.GetChild(0).GetComponent<DieBehavior>();
                    dicetemp.SetStoredMove(new Vector3(transform.position.x + currentX, transform.position.y + 0.5f,
                        transform.position.z + currentY));
                    dicetemp.SetMoveLimit(dieNumber);
                    /*go2.transform.position = new Vector3(transform.position.x + currentX, transform.position.y + 0.5f,
                        transform.position.z + currentY);*/
                }
                // spawn a holes
                else if (line[i] == 'H')
                {
                    go = Instantiate(hole, transform);
                    go.transform.position = new Vector3(transform.position.x + currentX, transform.position.y,
                        transform.position.z + currentY);
                    HoleSprites holetemp = go.GetComponent<HoleSprites>();
                    holetemp.SetHoleCount(holeNumber);
                }
                currentX += 1; // should be the size of the tile
            }
            currentX = 0;
            currentY += 1; // should be the size of the tile
        }
        reader.Close();
        /*int holePos = Random.Range(1, (gridSizeX * gridSizeY) - 1);
        int counter = 0;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject go;
                if (counter == holePos)
                {
                    go = Instantiate(hole, transform);
                    go.transform.position = new Vector3(transform.position.x + x, transform.position.y,
                        transform.position.z + y);
                }
                else
                {
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + x, transform.position.y,
                        transform.position.z + y);
                }

                //Tile t = go.GetComponent<Tile>();
                //t.SetPosition(x, y);
                counter++;

                //tiles[counter] = t;
                //counter++;
            }
        }*/
    }
}
