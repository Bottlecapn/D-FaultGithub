using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private static GameProgress gameProgress = null;
    private List<bool> levelUnlocked = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        if (gameProgress == null)
        {
            gameProgress = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameProgress != this)
        {
            Destroy(gameObject);
            return;
        }

        // only unlock the first level
        levelUnlocked.Add(true);
        // NOTE: CHANGE THE RANGE IF MORE LEVELS ARE ADDED
        for (int i = 1; i <= 24; i++)
        {
            levelUnlocked.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<bool> GetLevelUnlocked()
    {
        return levelUnlocked;
    }
}
