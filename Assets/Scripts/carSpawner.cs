using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    public GameObject[] cars;
    int carNo;
    public float maxPos = 6.0f;
    public float delayTimer = 1.0f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseGame.gameIsPaused)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Vector3 carPos = new Vector3(Random.Range(-6.0f, 6.0f), transform.position.y, transform.position.z);

                carNo = Random.Range(0, 6);

                Instantiate(cars[carNo], carPos, transform.rotation);
                timer = delayTimer;
            }
        }

    }
}
