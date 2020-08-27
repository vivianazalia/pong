using UnityEngine;

public class AttributeCollider : MonoBehaviour
{
    public PlayerControl player;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            if(gameObject.name == "PullRacket")
            {
                player.PullRacket();
            }

            if(gameObject.name == "Fire")
            {

            }
        }
    }
}
