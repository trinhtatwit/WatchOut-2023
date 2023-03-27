using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for spawning the NPCS, uses one NPC object from the scene and spawns a set number of that object, the NPCs create a challenge for the players

public class RandomSpawner : MonoBehaviour
{
    public GameObject npc; //What the spawners creates repeatedly
    public GameObject npcSpawner;
    public float radius = 1; //Size of spawner
    public int numberOfNPC; //Number of objects to spawn
    
    //Spawns the object the number of times desired
    void Start()
    {
        Vector2 position = npcSpawner.transform.position;
        for (int i = 0; i < numberOfNPC; i++) 
        {
            Vector2 randomPos = position + Random.insideUnitCircle * radius ;
            Instantiate(npc, randomPos, Quaternion.identity);
        }
    }

    //Shows an outline of the spawn radius
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
