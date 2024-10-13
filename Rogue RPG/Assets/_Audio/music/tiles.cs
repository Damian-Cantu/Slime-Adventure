using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles : MonoBehaviour
{
    public float bpm = 101f;
    public float tileChangeTime = 1f;
    public float timer;
    public Color tileColor;
    public Color colorOne = new Color(0.85f, 0.22f, 0.45f);
    public Color colorTwo = new Color(0.22f, 0.83f, 0.30f);
    public Color colorThree = Color.white;
    public Color colorFour = Color.yellow;
    public int colorIndex = 0;

    public bool disableFlashingFloor = false;

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        tileChangeTime = 60 / bpm;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (disableFlashingFloor)
        {
            int ranVal = Random.Range(0, 4);

            switch (ranVal)
            {
                case 0:
                    tileColor = colorOne;
                    break;
                case 1:
                    tileColor = colorTwo;
                    break;
                case 2:
                    tileColor = colorThree;
                    break;
                case 3:
                    tileColor = colorFour;
                    break;
                default:
                    print("Could not find status color");
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableFlashingFloor)
        {
            timer += Time.deltaTime;

            if (timer >= tileChangeTime)
            {
                timer = 0f;

                /*int randomVal = Random.Range(0, 3);

                while(colorIndex == randomVal)
                {
                    randomVal = Random.Range(0, 3);
                }*/

                colorIndex++;

                //colorIndex = randomVal;
                if (colorIndex == 4)
                {
                    colorIndex = 0;
                }

                switch (colorIndex)
                {
                    case 0:
                        tileColor = colorOne;
                        break;
                    case 1:
                        tileColor = colorTwo;
                        break;
                    case 2:
                        tileColor = colorThree;
                        break;
                    case 3:
                        tileColor = colorFour;
                        break;
                    default:
                        print("Could not find status color");
                        break;
                }

                spriteRenderer.color = tileColor;
            }
        }
        
    }
}
