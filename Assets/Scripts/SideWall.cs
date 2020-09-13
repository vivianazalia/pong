using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerControl player;

    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Ball")
        {
            player.IncrementScore();

            if(player.Score < gameManager.maxScore)
            {
                //restart game setelah bola mengenai dinding kanan atau kiri
                collision.gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
