/*****************************************************************************
// File Name : InsanityController.cs
// Author : Bryson Welch
// Creation Date : March 29, 2025
//
// Brief Description : Adjusts the value for the insanity meter, performing
effects such as monster spawning if high enough
*****************************************************************************/
using UnityEngine;

public class InsanityController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject insanityMonster;

    private int insanity;

    public int Insanity { get => insanity; set => insanity = value; }

    //private bool drainHealth;

    [SerializeField] private Transform monsterSpawn;

    /// <summary>
    /// Increases insanity based on what caused increase of insanity
    /// </summary>
    /// <param name="insanityIncrease"></param>
    public void IncreaseInsanity(int insanityIncrease)
    {
        Insanity += insanityIncrease;
        InsanityCheck(insanity);
    }

    /// <summary>
    /// Checks insanity before game start
    /// </summary>
    private void Start()
    {
        InsanityCheck(insanity);
    }

    /// <summary>
    /// Decreases the player insanity value depending on what was interacted with
    /// </summary>
    /// <param name="insanityDecrease"></param>
    public void DecreaseInsanity(int insanityDecrease)
    {
        Insanity -= insanityDecrease;
        InsanityCheck(insanity);
    }
    
    /// <summary>
    /// Checks for what the insanity level is, performing various functions depending on the level of insanity
    /// </summary>
    /// <param name="insanity"> the Insanity value of the player</param>
    public void InsanityCheck(int ins)
    {
        if (ins > 1000)
        {
            insanity = 1000;
        }
        if(ins > 900)
        {
            //drainHealth = true;
            SpawnMonster();
        }else if (insanity > 500)
        {
            SpawnMonster();
        } else if (ins < 500)
        {
            insanityMonster.SetActive(false);
        }
        if (ins < 0)
        {
            insanity = 0;
        }
    }
    /// <summary>
    /// When insanity is high enough, the monster spawns
    /// </summary>
    private void SpawnMonster()
    {
        insanityMonster.SetActive(true);
    }
}
