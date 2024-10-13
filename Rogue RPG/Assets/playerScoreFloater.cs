using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScoreFloater : MonoBehaviour
{
    public int flySpeed = 2;
    public float liveTime = 2f;

    private void Start()
    {
        StartCoroutine(DestroyFloater());
    }
    void Update()
    {
        transform.Translate((Vector2.up * flySpeed)* Time.deltaTime);
    }

    public IEnumerator DestroyFloater()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
