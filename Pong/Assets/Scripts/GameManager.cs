using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Pemain 1
    public PlayerControl player1;
    private Rigidbody2D player1Rb;

    //Pemain 2
    public PlayerControl player2;
    private Rigidbody2D player2Rb;

    //Bola
    public BallControl ball;
    private Rigidbody2D ballRb;
    private CircleCollider2D ballCollider;

    //Skor Maks
    public int maxScore;

    //Trajectory 
    public Trajectory trajectory;

    private bool isDebugWindowShown = false;
    public bool isEnding;
    public GameObject helpButton;

    void Start()
    {
        player1Rb = player1.GetComponent<Rigidbody2D>();
        player2Rb = player2.GetComponent<Rigidbody2D>();
        ballRb = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
        isEnding = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isEnding)
        {
            helpButton.SetActive(true);
        }
    }

    void OnGUI()
    {
        float maxTop = 35;
        float maxBottom = Screen.height - 90;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 50, Screen.width / 4, Screen.width / 4), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 50, Screen.width / 4, Screen.width / 4), "" + player2.Score);

        //jika tombol restart ditekan
        if(GUI.Button(new Rect(Screen.width/ 2 - 60, maxTop, Screen.width / 16, Screen.width / 36), "START"))
        {
            player1.ResetScore();
            player2.ResetScore();

            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
            helpButton.SetActive(false);
            isEnding = false;
        }

        if(player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            isEnding = true;
        } 
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 10, 2000, 1000), "PLAYER TWO WINS");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            isEnding = true;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 60, maxBottom, Screen.width / 16, Screen.width / 36), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }

        if (isDebugWindowShown)
        {
            GUI.backgroundColor = Color.red;

            //variabel fisika yang akan ditampilkan pada Debug Window
            float ballMass = ballRb.mass;
            Vector2 ballVelocity = ballRb.velocity;
            float ballSpeed = ballRb.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText = "Ball Mass = " + ballMass + "\n" +
                               "Ball Velocity = " + ballVelocity + "\n" +
                               "Ball Speed = " + ballSpeed + "\n" +
                               "Ball Momentum = " + ballMomentum + "\n" +
                               "Ball Friction = " + ballFriction + "\n" +
                               "Last Impulse from P1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                               "Last Impulse from P2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n" + 
                               "Screen Height = " + Screen.height + "\n" +
                               "Screen Width = " + Screen.width; 

            //Tampilan Debug Window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 150), debugText, guiStyle);
        }
    }
}
