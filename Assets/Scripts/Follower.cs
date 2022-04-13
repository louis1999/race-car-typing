using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravalled;

    void Update(){
        distanceTravalled += speed*Time.deltaTime;
        transform.position= pathCreator.path.GetPointAtDistance(distanceTravalled);
    }
}
