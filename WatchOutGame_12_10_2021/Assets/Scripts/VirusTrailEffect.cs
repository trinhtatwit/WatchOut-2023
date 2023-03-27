using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* This was an experimental idea and not a part of the game!
* The idea was to make the game more challenging by having the npc's
* that walk around the city leave a bacteria trail that the player would need to avoid
* it was quite buggy and could not get the player to take any damage.
*/
public class VirusTrailEffect : MonoBehaviour
{
    private float timeBetweenSpawn;
    public float startTimeBetweenSpawns;
    public GameObject echo;
    private NpcMovement npc;
    public Transform player;
    private PlayerHpManager playerHP;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        npc = GetComponent<NpcMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (npc.isMoving) 
        {
            if (timeBetweenSpawn <= 0)
            {
                GameObject instance = Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, delay);
                timeBetweenSpawn = startTimeBetweenSpawns;
            } 
            else
            {
                timeBetweenSpawn -= Time.deltaTime;
            }
        }
    
    }
}
