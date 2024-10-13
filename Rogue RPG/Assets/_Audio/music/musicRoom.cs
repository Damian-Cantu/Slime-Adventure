using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicRoom : MonoBehaviour
{
    public List<GameObject> whosHere;
    public List<GameObject> speakers;
    public Vector3 volume;
    public Vector3 dancerVibe;
    public float recoil;
    public float bpm = 102;
    public float timer = 0;
    public float moveTime;
    public float animPos = 0f;

    // Start is called before the first frame update
    void Start()
    {
        moveTime = (60 / bpm);
        float direction;
        foreach (GameObject dancer in whosHere)
        {
            dancer.GetComponent<Animator>().speed = 0;

            if (Random.value >= 0.5)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            dancer.transform.localScale = new Vector3 (direction, 1,1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= moveTime)
        {
            animPos += 0.2f;
            if(animPos >= 0.9f)
            {
                animPos = 0.2f;
            }
            foreach (GameObject dancer in whosHere)
            {
                

                Animator anim = dancer.GetComponent<Animator>();
                //Get the info about the current animator state
                AnimatorStateInfo animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);

                //Convert the current state name to an integer hash for identification
                int currentState = animatorStateInfo.fullPathHash;

                //Start playing the current animation from wherever the current conductor loop is
                anim.Play(currentState, -1, animPos);
                //Set the speed to 0 so it will only change frames when you next update it
                anim.speed = 0;

                dancer.transform.localScale = new Vector3 ((dancer.transform.localScale.x * -1) * dancerVibe.x, dancerVibe.y, dancerVibe.z);
            }

            foreach (GameObject speaker in speakers)
            {
                speaker.transform.localScale = volume;
            }
            timer = 0;
        }
        else
        {
            foreach (GameObject speaker in speakers)
            {
                speaker.transform.localScale = Vector3.MoveTowards(speaker.transform.localScale, Vector3.one, recoil * Time.deltaTime);
            }

            foreach (GameObject dancer in whosHere)
            {
                float direction;
                if(dancer.transform.localScale.x < 0)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
                dancer.transform.localScale = Vector3.MoveTowards(dancer.transform.localScale, new Vector3(direction, 1, 1), recoil * Time.deltaTime);
            }
        }
    }
}
