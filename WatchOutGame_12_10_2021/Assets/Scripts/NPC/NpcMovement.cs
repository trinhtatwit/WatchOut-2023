using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    internal Transform thisTransform;

    // The movement speed of the object
    public float moveSpeed = 1f;

    // A minimum and maximum time delay for taking a decision, choosing a direction to move in
    public Vector2 decisionTime = new Vector2(1, 4);
    internal float decisionTimeCount = 0;

    //Maximum time that an npc should move before it stops (should be less than the minimum time it chooses to move again)
    public int stopTime = 1;

    // The possible directions that the object can move int, right, left, up, down, and zero for staying in place. I added zero twice to give a bigger chance if it happening than other directions
    internal Vector3[] moveDirections = new Vector3[] { Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.zero, Vector3.zero };
    internal int currentMoveDirection;
    public int previousMoveDirection;
    public bool isMoving;
    public bool npcCanMove;

    private Rigidbody2D rigidBody;
    float horizontal;
    float vertical;
    public DialogueManager dialogManager;
    public DialogueManager dialogManager2;


    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        npcCanMove = true;
        // Cache the transform for quicker access
        thisTransform = this.transform;

        // Set a random time delay for taking a decision ( changing direction, or standing in place for a while )
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();

        //keeps track of the previous direction that the npc moved but 
        //initially it will have a number that simply will never happen so any direction can happen
        previousMoveDirection = -1;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogManager.isBoxActive && !dialogManager2.isBoxActive)
        {
            npcCanMove = true;
        }
        else {
            npcCanMove = false;
        }

        if (!npcCanMove)
        {
            rigidBody.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }
        else
        {

            horizontal = moveDirections[currentMoveDirection].x;
            vertical = moveDirections[currentMoveDirection].y;

            Vector2 move = new Vector2(horizontal, vertical);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }

            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);

            // Move the object in the chosen direction at the set speed
            //thisTransform.position += moveDirections[currentMoveDirection] * Time.deltaTime * moveSpeed;
            
            if (decisionTimeCount > 0)
            {
                decisionTimeCount -= Time.deltaTime;
            }
            else
            {
                // Choose a random time delay for taking a decision ( changing direction, or standing in place for a while )
                decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

                // Choose a movement direction, or stay in place
                ChooseMoveDirection();
            }
        }
    }

    void FixedUpdate() 
    {
        Vector2 position = rigidBody.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;

        rigidBody.MovePosition(position);
    }

    void OnTriggerEnter2D()
    {
        StopMovement();
    }

    //Chooses random direction for the npc to move towards.
    void ChooseMoveDirection()
    {

        // Choose whether to move sideways or up/down
        while(true)
        {
            currentMoveDirection = Mathf.FloorToInt(Random.Range(0, moveDirections.Length));
            if(currentMoveDirection != previousMoveDirection){
                previousMoveDirection = currentMoveDirection;
                break;
            }
        }

        if (currentMoveDirection == 4 || currentMoveDirection == 5)
        {
            isMoving = false;
        }
        else 
        {
            isMoving = true;
        }
        Invoke("StopMovement", stopTime);
    }

    void StopMovement()
    {
        //Sets movement direction to Vector3.zero aka moveDirections[5]
        currentMoveDirection = 5;
        animator.SetFloat("Speed", 0);
    }

    public void stopMoving()
    {
      npcCanMove = false;
    }

     public void startMoving()
    {
      npcCanMove = true;
    }

}
