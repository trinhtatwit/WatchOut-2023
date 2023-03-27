using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameObject npc;
    static int min = 1;
    static int maxX = 36;
    static int maxY = 20;
    static int diffX = maxY - min;
    static int diffY = maxX - min;
    float randX, randY;
    Vector2 spawn;
    int npccount = 5;
    bool[,] squares = new bool[diffX, diffY];
    //public Collision2D collision;
    bool free = true;

    // Start is called before the first frame update
    void Start()
    {
        int total = 0;
        while (total < npccount) {
            int tempx = Random.Range(min,maxY);
            int tempy = Random.Range(min,maxX);

            int sx = tempx - 1;
            int sy = tempy - 1;

            free = true;
            bool vertFree = true;
            bool horizFree = true;
            bool diagFree = true;
            

            // check if selected square is free
            if (squares[sx, sy] == true) free = false;
            //if (collision.gameObject.name == "Wall") free = false;

            // check if square(s) in vertical direction are free
            if (sy-1 >= 0)
                if (squares[sx, sy-1] == true) vertFree = false;
            
            if (sy+1 < diffY)
                if (squares[sx, sy+1] == true) vertFree = false;

            // check if square(s) in horizontal direction are free
            if (sx-1 >= 0)
                if (squares[sx-1, sy] == true) horizFree = false;
            
            if (sx+1 < diffX)
                if (squares[sx+1, sy] == true) horizFree = false;

            // check if square(s) in diagonal direction are free
            if (sx-1 >= 0 & sy-1 >= 0)
                if (squares[sx-1, sy-1] == true) diagFree = false;

            if (sx+1 < diffX & sy+1 < diffY)
                if (squares[sx+1, sy+1] == true) diagFree = false;

            if (sx-1 >= 0 & sy+1 < diffY)
                if (squares[sx-1, sy+1] == true) diagFree = false;
            
            if (sx+1 < diffX & sy-1 >= 0)
                if (squares[sx+1, sy-1] == true) diagFree = false;

            if (vertFree & horizFree & diagFree & free) {
                total++;
                squares[tempx-1, tempy-1] = true;

                randX = -8.75f;
                randY = 4.75f;

                for (int x = 1; x < tempy; x++) randX = randX + 0.5f;
                for (int x = 1; x < tempx; x++) randY = randY - 0.5f;

                spawn = new Vector2(randX, randY);
                Instantiate(npc, spawn, Quaternion.identity);
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.name == "Wall") free = false;
    }
}
