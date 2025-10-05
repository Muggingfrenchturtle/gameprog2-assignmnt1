using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class circlescript : MonoBehaviour
{
    public float rotatespeed = 45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var Rotation = transform.rotation;

        Rotation.z += rotatespeed * Time.deltaTime; //rotate the thingy

        transform.rotation = Rotation; //apply rotation
        */

        transform.Rotate(new Vector3(0,0,rotatespeed * Time.deltaTime)); //asfhfjsdhdajsfjh this is how its done in unity lol


        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rotatespeed *= -1; //toggle rotatespeed to become negative to rotate in the other direction
        }
    }

}
