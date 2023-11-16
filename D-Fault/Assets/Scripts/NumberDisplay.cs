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
    Vector3 originalSpritePosition1, originalSpritePosition2, originalSpritePosition3;
    bool completed;

    private void Start()
    {
        if (useAudio)
        {
            sfx = GetComponent<AudioSource>();
        }
        originalSpritePosition1 = slot1.transform.position;
        originalSpritePosition2 = slot2.transform.position;
        originalSpritePosition3 = slot3.transform.position;
        completed = false;
    }

    private void Update()
    {
        if (completed || counting)
        {
            slot1.transform.position = Vector3.Lerp(slot1.transform.position, originalSpritePosition1, 10*Time.deltaTime);
            slot2.transform.position = Vector3.Lerp(slot2.transform.position, originalSpritePosition2, 10 * Time.deltaTime);
            slot3.transform.position = Vector3.Lerp(slot3.transform.position, originalSpritePosition3, 10 * Time.deltaTime);
        }
    }
    // function that changes the text sprites to match the specified number value.
    // 3 "slots" are different sprite positions: slot1 is for single digits.
    // slot2 and slot3 are for double digit numbers (tens place and ones place, respectively).
    // called by CountDown coroutine as well as DieBehavior to update dice number instantly.
    public int UpdateNumber(int count)
    {
        if (count <= 9) {
            slot1.enabled = true;
            slot2.enabled = false;
            slot3.enabled = false;
            if(count < 0) { 
                count = 0;
                slot1.color = Color.yellow;
            }
            slot1.sprite = numberSprites[count];
        } else {
            slot1.enabled = false;
            slot2.enabled = true;
            slot3.enabled = true;
            slot2.sprite = numberSprites[count/10];
            slot3.sprite = numberSprites[count%10];
        }

        if (counting) { 
            slot1.transform.position = new Vector3(originalSpritePosition1.x, originalSpritePosition1.y - 1f,
                        originalSpritePosition1.z);
            slot2.transform.position = new Vector3(originalSpritePosition2.x, originalSpritePosition2.y - 1f,
                    originalSpritePosition2.z);
            slot3.transform.position = new Vector3(originalSpritePosition3.x, originalSpritePosition3.y - 1f,
                    originalSpritePosition3.z);
        }

        return count;
    }

    // coroutine that performs a lerped count down/up from currentNum to targetNum.
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
                // as far as I know, this never gets used. ignore this.
                if (Mathf.RoundToInt(countLerp) > currentCount) { 
                    currentCount = UpdateNumber(Mathf.RoundToInt(countLerp));
                    PlaySound();
                }
                yield return null;
            } else {
                sfx.pitch = 1f;
                counting = false;
                completed = true;
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
