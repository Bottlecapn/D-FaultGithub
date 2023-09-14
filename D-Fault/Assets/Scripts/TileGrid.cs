using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileGrid : MonoBehaviour
{
    [SerializeField] int gridSizeX, gridSizeY;
    [SerializeField] GameObject basicTile;
    [SerializeField] GameObject hole;
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
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GenerateGrid()
    {
        int holePos = Random.RandomRange(1, (gridSizeX*gridSizeY) -1);
        int counter = 0;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject go;
                if(counter == holePos) {
                    go = Instantiate(hole, transform);
                    go.transform.position = new Vector3(transform.position.x + x, transform.position.y,
                        transform.position.z + y);
                } else
                {
                    go = Instantiate(basicTile, transform);
                    go.transform.position = new Vector3(transform.position.x + x, transform.position.y,
                        transform.position.z + y);
                }
                
                //Tile t = go.GetComponent<Tile>();
                //t.SetPosition(x, y);
                counter++;
                
                /*tiles[counter] = t;
                counter++;*/
            }
        }
    }
}
