using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10f;
    //batas atas dan bawah game scene
    public float yBoundary = 9f;

    private Rigidbody2D rbRaket;
    private BoxCollider2D collRaket;
    private int score;
    private bool isBig = false;

    //titik tumbukan terakhir bola
    private ContactPoint2D lastContactPoint;

    void Start()
    {
        rbRaket = GetComponent<Rigidbody2D>();
        collRaket = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Vector2 velocity = rbRaket.velocity;
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        } 
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0;
        }

        rbRaket.velocity = velocity;

        Vector3 position = transform.position;
        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        } 
        else if(position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }
        transform.position = position;
    }

    public void PullRacket()
    {
        if (!isBig)
        {
            StartCoroutine(BigRaket());
        }
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get{ return score; }
    }

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    IEnumerator BigRaket()
    {
        Vector3 oldScaleRaket = transform.localScale;
        transform.localScale = new Vector3(1, 2, 1);
        isBig = true;
        yield return new WaitForSeconds(5);
        transform.localScale = oldScaleRaket;
        isBig = false;
    }
}
