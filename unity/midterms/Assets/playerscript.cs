using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    public GameObject bullet;

    public float cooldownTimer;
    public float cooldownAmount = 1;
    public bool isOverheating = false;

    private float rotationworkable;

    public float shootForce;




    public float colorTicker = 0;
    public float colorTickMax = 3;

    public Color currentColor;

    public float virtualLookHitbox; //the size of the virtual hitbox. just using a value and comparing that to distance between the 2 points effectively creates a radial ""hitbox""


    public GameObject[] enemiesInRange = new GameObject[10]; //up to 10 enemies can be in range. array size limited to make sure the related array code dosent have to go through a bigass array
    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = cooldownAmount;

        OnMouseDown();
    }

    // Update is called once per frame
    void Update()
    {

        //shooting stuff
        shootingStuff();




        lookAtClosestEnemyMechanic(); //the way i did the "look at closest enemy within a range" thing is absolutely stupid and horrible and i should NOT use it as a reference for future work.





    }

    //---------------------------DRIVER FUNCTIONS-------------------------------
    private void shootingStuff()
    {
        if (isOverheating == true)
        {
            cooldownTimer -= 1 * Time.deltaTime;
        }

        if (cooldownTimer <= 0)
        {
            resetGun();
        }

        if (isOverheating == false)
        {
            rotationworkable = transform.eulerAngles.z * Mathf.Deg2Rad; //https://chatgpt.com/s/t_68fdf4ae30588191839d72e78e8d5e64
                                                                        // wierdass rotation stuff. done to convert rotation for the bullet shooting formula

            shootBullet(new Vector2(shootForce * Mathf.Cos(rotationworkable), shootForce * Mathf.Sin(rotationworkable))); //shoots bullet based on player rotation


        }
    }



    private void lookAtClosestEnemyMechanic()
    {
        var enemies = GameObject.FindGameObjectsWithTag("enemy");

        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<enemyscript>().distanceToPlayer <= virtualLookHitbox && enemy.GetComponent<enemyscript>().isdetected == false)
            {
                //assign to enemies in range
                
                
                var freeslot = findemptyslotinarray(enemiesInRange);
                enemy.GetComponent<enemyscript>().isdetected = true;
                enemiesInRange[freeslot] = enemy; //if its within the virtual hitbox, assign the gameobject into an array

                Debug.Log("enemy within virtual hitbox" + enemy.GetComponent<enemyscript>().distanceToPlayer + "<" + virtualLookHitbox);  //if this runs every frame multiple times for one enemy its bad





                //transform.right = findclosestenemy(enemiesInRange).transform.position - transform.position; //somewhat seperate. this finds the closest enemy within the array and looks at it.
                                                                                                            //we only need to run this while enemies are in range.
                                                                                                            //also cuz fundotnullinarray() gets errors if the ENTIRE array is null (the entire array is null only when there is no enemies within range)
                                                                                                            //NVM. doing it like this makes it so it cant look at enemies that are already in its range after it doestroys another enemy
            }
            else if (enemy.GetComponent<enemyscript>().distanceToPlayer > virtualLookHitbox)
            {
                enemy.GetComponent<enemyscript>().isdetected = false;
            }
        }

        if (findnotnullinarray(enemiesInRange) != 99999) //okay. so. the function returns 99999 if it goes through the entire array and all contents are null.
                                                         //so basically, the player will try to look at any enemies within range EVERY FRAME, ONLY IF enemiesinrange HAS contents
                                                         //thus, allowing the player to look at enemies that are already in its range after it doestroys another enemy
            
        {
            transform.right = findclosestenemy(enemiesInRange).transform.position - transform.position;
        }
        


    }

    //---------------------------DRIVER FUNCTIONS-------------------------------



    //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV BLADE FUNCTIONS VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV

    public void shootBullet (Vector2 shootdirection)
    {
        var bulletToSpawn = Instantiate(bullet, transform.position, transform.rotation);
        //simultaneously instantiates and puts it as a variable
        //this does the godot thing of being able to modify values of an instantiating prefab AS it spawns

        bulletToSpawn.GetComponent<bulletscript>().shootDirectionFromGun = shootdirection;

        bulletToSpawn.GetComponent<bulletscript>().GetComponent<SpriteRenderer>().color = currentColor; //changes bullet color. this is kinda funny looking

        bulletToSpawn.GetComponent<bulletscript>().colorValue = colorTicker; //gives a color value for enemy insteractions

        //^^ these getcomponents above modifies the bullet's variables as it instantiates


        //Debug.Log(shootdirection);
        //Debug.Log(transform.eulerAngles.z);


        isOverheating = true;
    }

    public void resetGun () 
    {
        cooldownTimer = cooldownAmount;
        isOverheating = false;
    }

    


    private int findemptyslotinarray(GameObject[] arr)
    {
        int intToReturn = 99999; //pretty much throws an error if this somehow goes through as this number
        for (int i = 0; i < arr.Length; ++i)
        {
            if (arr[i] == null)
            {
                intToReturn = i;
            }
        }

        return intToReturn; //returns slot deemed empty
    }

    private int findnotnullinarray(GameObject[] arr) //okay this is absolutely stupid. im actually making 2 of these functions because i decided to this bullshit array crap for the enemy look at mechanic. this only gets used by findclosestenemy
    {
        int intToReturn = 99999; 
        for (int i = 0; i < arr.Length; ++i)
        {
            if (arr[i] != null)
            {
                intToReturn = i;
            }
        }

        return intToReturn; 
    }

    private GameObject findclosestenemy(GameObject[] enemyarray)
    {
        GameObject currentClosest = enemyarray[findnotnullinarray(enemyarray)]; //finds the first not-null tingy in the array and assigns it as the closest before the loop so it has something to compare to
        GameObject enemyToTest;

        for (int i = 0; i < 10; i++)
        {
            enemyToTest = enemyarray[i];

            if (enemyToTest != null)
            {
                if (enemyToTest.GetComponent<enemyscript>().distanceToPlayer <= currentClosest.GetComponent<enemyscript>().distanceToPlayer)
                {
                    currentClosest = enemyToTest;
                }
            }
            
        }

        return currentClosest;
    }

    //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV BLADE FUNCTIONS VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV





    //XXXXXXXXXXXXXX  COMPONENT FUNCTIONS XXXXXXXXXXXXXXXXXXXXXXXXXXXXX

    private void OnMouseDown() //runs if you click the boxcollider of the player object
    {
        colorTicker += 1;

        if (colorTicker >= colorTickMax)
        {
            colorTicker = 0; //reset colorticker
        }

        switch (colorTicker)
        {
            case 0:
                {
                    currentColor = Color.red;
                    break;
                }
            case 1:
                {
                    currentColor = Color.green;
                    break;
                }
            case 2:
                {
                    currentColor = Color.blue;
                    break;
                }
        }




        this.GetComponent<SpriteRenderer>().color = currentColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("enemy"))
        {
            Debug.Log("game over2");
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("gameover").SetActive(true); //idk just plop a red carpet over everything and call it game over
        }
        */ //for some reason this dosent work lol

    }

    //XXXXXXXXXXXXXX  COMPONENT FUNCTIONS XXXXXXXXXXXXXXXXXXXXXXXXXXXXX


}
