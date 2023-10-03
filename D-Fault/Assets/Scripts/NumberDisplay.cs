using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberDisplay : Tile
{
    [SerializeField] SpriteRenderer slot1, slot2, slot3; 
    [SerializeField] float maxDuration;
    [SerializeField] bool useAudio;
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioClip countSound;
    [SerializeField] Sprite[] numberSprites;
    int currentCount;
    int countTarget;
    bool counting;

    private void Start()
    {
        if (useAudio)
        {
            sfx = GetComponent<AudioSource>();
        }
    }

    // function that changes the text sprites to match the specified number value.
    // 3 "slots" are different sprite positions: slot1 is for single digits.
    // slot2 and slot3 are for double digit numbers (tens place and ones place, respectively).
    // called by CountDown enumerator as well as DieBehavior to update dice number instantly.
    public int UpdateNumber(int count)
    {
        if (count <= 9) {
            slot1.enabled = true;
            slot2.enabled = false;
            slot3.enabled = false;
            if(count < 0)
                count = 0;
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

    // function that performs a lerped count down/up from currentNum to targetNum.
    // This is to show the effect of the number going down/up in realtime, used on the Hole.
    // (please don't mess with this function) - Amari
    public IEnumerator CountDown(int currentNum, int targetNum)
    {
        counting = true;
        float timeElapsed = 0f;
        currentCount = currentNum;
        float countLerp = currentNum;
        float pitchLerp = Mathf.Clamp(-0.04f*(Mathf.Abs(currentNum-targetNum))+1, 0.75f, 1f);
        while (true)
        {
            float t = timeElapsed / maxDuration;
            t = t * t * (3f - 2f * t);
            countLerp = Mathf.Lerp(countLerp, targetNum, t);
            pitchLerp = Mathf.Lerp(pitchLerp, 1, t);
            sfx.pitch = pitchLerp;
            timeElapsed += Time.deltaTime;
            if (currentCount > targetNum) {
                if(Mathf.RoundToInt(countLerp) < currentCount) { 
                    currentCount = UpdateNumber(Mathf.RoundToInt(countLerp));
                    PlaySound();
                }
                yield return null;
            } else if (currentCount < countTarget) {
                if (Mathf.RoundToInt(countLerp) > currentCount) { 
                    currentCount = UpdateNumber(Mathf.RoundToInt(countLerp));
                    PlaySound();
                }
                yield return null;
            } else {
                print("ending");
                sfx.pitch = 1f;
                counting = false;
                yield break;
            }
            
        }
        
    }

    public bool IsCounting()
    {
        return counting;
    }

    private void PlaySound()
    {
        sfx.Stop();
        sfx.PlayOneShot(countSound);
    }

    /*private void GetSprites()
    {
        foreach (GameObject child in gameObject.transform)
        {
            child.
        }
    }*/
}
