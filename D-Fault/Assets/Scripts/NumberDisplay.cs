using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberDisplay : Tile
{

    [SerializeField] SpriteRenderer slot1, slot2, slot3;
    [SerializeField] Sprite[] numberSprites;
    [SerializeField] float maxDuration;
    int currentCount;
    int countTarget;
    bool counting;

    public int UpdateNumber(int count)
    {
        if (count <= 9) {
            slot1.enabled = true;
            slot2.enabled = false;
            slot3.enabled = false;
            slot1.sprite = numberSprites[count];
        } else {
            slot1.enabled = false;
            slot2.enabled = true;
            slot3.enabled = true;
            slot2.sprite = numberSprites[count/10];
            slot3.sprite = numberSprites[count%10];
        }
        return count;
    }

    public IEnumerator CountDown(int currentNum, int targetNum)
    {
        counting = true;
        float timeElapsed = 0f;
        currentCount = currentNum;
        float countLerp = currentNum;
        while(true)
        {
            float t = timeElapsed / maxDuration;
            t = t * t * (3f - 2f * t);
            countLerp = Mathf.Lerp(countLerp, targetNum, t);
            timeElapsed+= Time.deltaTime;
            if (currentCount > targetNum) {
                if(countLerp < currentCount) { 
                    currentCount = UpdateNumber(Mathf.RoundToInt(countLerp));
                }
                yield return null;
            } else if (currentCount < countTarget) {
                if (countLerp > currentCount) { 
                    currentCount = UpdateNumber(Mathf.RoundToInt(countLerp));
                }
                yield return null;
            } else {
                print("ending");
                counting = false;
                yield break;
            }
            
        }
        
    }

    public bool IsCounting()
    {
        return counting;
    }
}
