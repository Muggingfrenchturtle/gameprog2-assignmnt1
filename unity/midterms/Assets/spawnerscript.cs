using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerscript : MonoBehaviour
{
    public GameObject enemy;

    public float timer;

    public float timerMinPossible;
    public float timerMaxPossible;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(1, timerMaxPossible);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if (timer <= 0 ) 
        {
            Instantiate(enemy, transform.position, transform.rotation); //create enemy

            timer = Random.Range(timerMinPossible, timerMaxPossible); //reset timer
        }
    }
}
