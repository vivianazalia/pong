using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fire;
    private GameObject fireClone;
    public GameManager gameManager;

    private bool isSpawn = false;

    void Update()
    {
        if (!isSpawn && gameManager.isEnding == false)
        {
            Invoke("InstantiateFire", 15);
            isSpawn = true;

            if (isSpawn)
            {
                Invoke("FireDestroy", 20);
            }
        }
    }

    void InstantiateFire()
    {
        float randomPosX = Random.Range(-14.5f, 11.6f);
        float randomPosY = Random.Range(-7.5f, 7.5f);
        transform.position = new Vector2(randomPosX, randomPosY);
        fireClone = (GameObject)Instantiate(fire, transform.position, fire.transform.rotation);
    }

    void FireDestroy()
    {
        if (isSpawn)
        {
            Destroy(fireClone);
            isSpawn = false;
        }
    }
}
