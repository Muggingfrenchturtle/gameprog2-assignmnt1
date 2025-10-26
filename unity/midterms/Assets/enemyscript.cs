using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class enemyscript : MonoBehaviour
{
    public float colorValue;

    public Vector3 enemyMove;

    public GameObject player;

    public float shootForce;

    public float distanceToPlayer = 99;

    public bool isdetected = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");

        //distanceToPlayer = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - transform.position.x), 2) + Mathf.Pow((player.transform.position.y - transform.position.y), 2)); 
        //run the distance check on spawn. without this, it spawns with a distancetoplayer value of 0, and the player ends up looking at the enemy even though it isnt in range yet
        //fkn nevermind. even here it still starts at 0 before the distance checks kick in.
        //just set it so a stupidly high value by default



        colorValue = UnityEngine.Random.Range(0, 3);

        switch (colorValue)
        {
            case 0:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                }
            case 1:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                }
            case 2:
                {
                    this.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                }
        }



        //find location of player
        enemyMove.x = ((player.transform.position.x - transform.position.x) / Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2))) * shootForce;

        enemyMove.y = ((player.transform.position.y - transform.position.y) / Mathf.Sqrt(Mathf.Pow(player.transform.position.x - transform.position.x, 2) + Mathf.Pow(player.transform.position.y - transform.position.y, 2))) * shootForce;



        Debug.Log(enemyMove);



        
    }

    // Update is called once per frame
    void Update()
    {
        //move to location of player
        transform.position += enemyMove * Time.deltaTime;






        distanceToPlayer = Mathf.Sqrt(Mathf.Pow((player.transform.position.x - transform.position.x), 2) + Mathf.Pow((player.transform.position.y - transform.position.y), 2));


        //game over when come close since uuuuuhhhh doing it like normally dosent work and i have 10 minutes
        if (distanceToPlayer < 0.5)
        {
            GameObject.FindGameObjectWithTag("gameover").transform.position = Vector2.zero; //idk just plop a red carpet over everything and call it game over
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //destroy bullet when color mismatch
        if (collision.CompareTag("bullet"))
        {
            if (collision.gameObject.GetComponent<bulletscript>().colorValue == colorValue)
            {
                Destroy(gameObject); //enemy only gets destroyed if color match. bullet gets destroyed on contact no matter what anyways
            }
        }

        //destroy bullet AND itself when color match
    }
}
