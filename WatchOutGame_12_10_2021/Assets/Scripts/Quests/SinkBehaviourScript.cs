using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkBehaviourScript : MonoBehaviour
{

    public Transform player;
	public Transform sink;
	public PlayerHpManager playerHealth;
    public AudioSource mp3Sound;
    [SerializeField] private Animator sink_AC;  // sink animation controller
    

    // Start is called before the first frame update
    void Start()
    {
        mp3Sound = GetComponent<AudioSource> ();
        mp3Sound.Stop();
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
		sink = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // distance b/w player and sink
        float distanceFromSink = Vector3.Distance(player.position, sink.position);
        
		if (distanceFromSink < 2  && playerHealth.healthPoints() < 100 )
		{
			playerHealth.sinkHeal();    
		} */
    }

    // called when player collides with sink
    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("collided");
        if (other.collider.tag == "Player" && playerHealth.healthPoints() < 100)
        {
        sink_AC.SetBool("playerNearby", true);
        mp3Sound.Play();
        }
    }

    // called when player stays in sink area to heal 
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && playerHealth.healthPoints() < 100) 
        {
            playerHealth.sinkHeal();
        } 
        else
        {
            sink_AC.SetBool("playerNearby", false);
            mp3Sound.Stop();
        }
    }

    // called when players ends collision with sink
    private void OnCollisionExit2D(Collision2D other) 
    {
        Debug.Log("un-collided");
        if (other.collider.tag == "Player")
        {
        sink_AC.SetBool("playerNearby", false);
        mp3Sound.Stop();
        }
    }
    
}
