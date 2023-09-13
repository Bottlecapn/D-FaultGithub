using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    [SerializeField] int gridSizeX, gridSizeY;
    [SerializeField] GameObject basicTile;
    [SerializeField] Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        tiles = new Tile[gridSizeX*gridSizeY];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        int counter = 0;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                GameObject go = Instantiate(basicTile, transform);
                go.transform.position = new Vector3(transform.position.x + x, transform.position.y, 
                    transform.position.z + y);
                Tile t = go.GetComponent<Tile>();
                t.SetPosition(x,y);
                tiles[counter] = t;
                counter++;
            }
        }
    }
}
