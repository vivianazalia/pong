using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullRacket : MonoBehaviour
{
    public GameObject pullRacket;
    private GameObject pullRacketClone;
    public GameManager gameManager;

    private bool isSpawn = false;

    void Update()
    {
        if (!isSpawn && gameManager.isEnding == false)
        {
            Invoke("InstantiatePullRacket", 10);
            isSpawn = true;

            if (isSpawn)
            {
                Invoke("PullRacketDestroy", 15);
            }
        }
    }

    void InstantiatePullRacket()
    {
        float randomPosX = Random.Range(-14.5f, 11.6f);
        float randomPosY = Random.Range(-7.5f, 7.5f);
        transform.position = new Vector2(randomPosX, randomPosY);
        pullRacketClone = (GameObject)Instantiate(pullRacket, transform.position, pullRacket.transform.rotation);
    }

    void PullRacketDestroy()
    {
        if (isSpawn)
        {
            Destroy(pullRacketClone);
            isSpawn = false;
        }
    }
}
