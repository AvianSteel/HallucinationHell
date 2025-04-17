/*****************************************************************************
// File Name : MonsterController.cs
// Author : Bryson Welch
// Creation Date : March 29, 2025
//
// Brief Description : Controls the AI of the monster when close to the player,
or far away
*****************************************************************************/
using System;
using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    [SerializeField] int detectionDistance;
    [SerializeField] Transform player;

    private int currentIndex;
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private float speed;
    private bool playSound;

    private Coroutine chaseSoundPlay;

    /// <summary>
    /// Starts the monster on its patrol path
    /// </summary>
    void Start()
    {
        currentIndex = 0;
    }

    /// <summary>
    /// Makes the monster follow the player if it gets too close. 
    /// Otherwise just patrols the points given
    /// </summary>
    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > detectionDistance)
        {
            playSound = false;
            if (Vector3.Distance(transform.position, movePoints[currentIndex].transform.position) < 0.1f)
            {
                currentIndex++;
                StopCoroutine(chaseSoundPlay);
                //Prevents currentIndex from being larger than the array of gameObjects
                if (currentIndex == movePoints.Length)
                {
                    currentIndex = 0;
                }
            }
            //moves the Monster to the point
            transform.position = Vector3.MoveTowards(transform.position, movePoints[currentIndex].transform.position,
                speed * Time.deltaTime);
        }else if (Vector3.Distance(transform.position,player.transform.position) <= detectionDistance)
        {
            playSound = true;
            chaseSoundPlay = StartCoroutine(MonsterSound());
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position,
                speed * Time.deltaTime);
        }
    }
    IEnumerator MonsterSound()
    {
        while (playSound)
        {
            Debug.Log("Working");
            yield return new WaitForSeconds(0.5f);
        }

    }
}
