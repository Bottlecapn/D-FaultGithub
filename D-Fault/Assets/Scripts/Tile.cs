using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int posX;
    private int posY;

    public void SetPosition(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public int[] GetPosition()
    {
        int[] arr = {posX, posY};
        return arr;
    }

}
