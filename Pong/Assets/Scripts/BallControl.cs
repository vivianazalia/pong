using System.Collections;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rbBall;
    public PlayerControl player1;
    public PlayerControl player2;
    public Sprite fireBall;
    private Sprite Ball;
    public GameManager gameManager;

    public float xInitialForce;
    public float yInitialForce;
    public float totalForce;

    //titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    private bool onFireBall = false;

    void Start()
    {
        rbBall = GetComponent<Rigidbody2D>();

        //Mulai game
        //RestartGame();

        trajectoryOrigin = transform.position;
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        rbBall.velocity = Vector2.zero;
    }

    void PushBall()
    {
        totalForce = 2500;
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        xInitialForce = Mathf.Sqrt(totalForce - (yRandomInitialForce * yRandomInitialForce));
        float randomDirection = Random.Range(0, 2);

        if(randomDirection < 1)
        {
            rbBall.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else
        {
            rbBall.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }

    void RestartGame()
    {
        ResetBall();

        //memanggil fungsi PushBall setelah 2 detik
        Invoke("PushBall", 2);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("PullRacket(Clone)"))
        {
            player1.PullRacket();
            player2.PullRacket();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name.Equals("Fire(Clone)"))
        {
            FireBall();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onFireBall)
        {
            if (collision.gameObject.name.Equals("Player1"))
            {
                player2.IncrementScore();
                if (player2.Score < gameManager.maxScore)
                {
                    //restart game setelah bola mengenai player 1
                    gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
                }
            }

            if (collision.gameObject.name.Equals("Player2"))
            {
                player1.IncrementScore();
                if (player1.Score < gameManager.maxScore)
                {
                    //restart game setelah bola mengenai player 2
                    gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
                }
            }
        }
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    IEnumerator OnFireBall()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Ball;
        onFireBall = false;
    }

    public void FireBall()
    {
        Ball = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = fireBall;
        onFireBall = true;
        StartCoroutine(OnFireBall());
    }
}
