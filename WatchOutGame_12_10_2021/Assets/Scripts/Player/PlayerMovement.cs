using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the player to move and interact with different objects, such as NPCs

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20.0f;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public bool playerCanMove;
    public VectorValue startingPosition;

    public Inventory playerInventory;
    public GameObject inventoryPanel;
    public InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCanMove = true;
        transform.position = startingPosition.initialValue;
    }

    //Constanly checks for various key inputs, I for inventory, E for interacting, checks if the player can move, also updates the player animations
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryPanel.activeInHierarchy)
            {
                inventoryPanel.SetActive(true);
                stopMoving();
            }
            else
            {
                inventoryPanel.SetActive(false);
                startMoving();
            }


        }

        if (!playerCanMove)
        {
            rigidbody2d.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) 
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.1f, lookDirection, 15.0f, LayerMask.GetMask("NPC"));

            if (hit.collider != null) 
            {
                DialogueManager character = hit.collider.GetComponent<DialogueManager>();

                if (character != null) 
                {
                    QuestManager characterQuest = hit.collider.GetComponent<QuestManager>();

                    if (hit.collider.GetComponent<NpcMovement>() != null) {
                        hit.collider.GetComponent<NpcMovement>().stopMoving();
                    }

                    if (hit.collider.CompareTag("Mom")) 
                    {
                        if(!characterQuest.IsActive())
                        {
                            //this is just to stop the npc from doing anything else since there is no more quests
                        }
                        else if(characterQuest.GetQuest().getQuestNum() == 0) 
                        {
                            if (characterQuest.IsActive()) 
                            {
                                if (PlayerStats.getHealth() < 100) 
                                {
                                    characterQuest.startQuest();
                                }
                                else 
                                {
                                    characterQuest.endQuest();
                                }
                            }
                        } 
                        else if(characterQuest.GetQuest().getQuestNum() == 2)
                        {
                            if (playerInventory.hasHandSanitizer())
                            {
                                characterQuest.endQuest();
                            }
                            else
                            {
                                characterQuest.startQuest();
                            }
                        }
                    }

                    if (hit.collider.CompareTag("Dad")) 
                    {
                        if(!characterQuest.IsActive())
                        {
                            //prevent quest repeats
                        }
                        else if(characterQuest.GetQuest().getQuestNum() == 1)
                        {
                            if (!playerInventory.hasDefaultMask() && !playerInventory.hasMaskOn()) 
                            {
                                characterQuest.startQuest();       
                            }
                            else 
                            {

                                characterQuest.endQuest();                         
                            } 
                        }
                    }
                    if (hit.collider.CompareTag("SuperAlex")) {
                        if (!playerInventory.hasN95Mask()) {
                            characterQuest.startQuest();
                        }
                        else {
                            characterQuest.endQuest();                           
                        } 
                    }
                    character.displayDialog();       
                }
            }
        }
    }

    //Makes the movement better
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        if (playerCanMove)
        {
            rigidbody2d.MovePosition(position);
        }
        
    }

    //Stops the player from moving
    public void stopMoving()
    {
        playerCanMove = false;
        animator.SetFloat("Speed", 0);
    }

    //Allows the player to move again
    public void startMoving()
    {
        playerCanMove = true;
    }
}
