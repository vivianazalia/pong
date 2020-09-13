using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    Rigidbody2D ballRb;
    CircleCollider2D ballColl;

    public GameObject ballAtCollision;

    void Start()
    {
        ballRb = ball.GetComponent<Rigidbody2D>();
        ballColl = ball.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        bool drawBallAtCollision = false;
        //digunakan untuk menampung nilai vektor titik tumbukan + vektor normal tumbukan.
        //merupakan tempat ballAtCollision digambar.
        Vector2 offsetHitPoint = new Vector2();

        //mencari titik-titik tumbukan dengan collider selain bola, disimpan dalam array
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRb.position, ballColl.radius, ballRb.velocity.normalized);

        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            //untuk setiap titik tumbukan, jika terjadi tumbukan dengan object lain (bukan dengan bola)
            if(circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                //titik tumbukan (dipinggir bola)
                Vector2 hitPoint = circleCastHit2D.point;

                //vector normal di titik tumbukan
                Vector2 hitNormal = circleCastHit2D.normal;

                offsetHitPoint = hitPoint + hitNormal * ballColl.radius;
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    //vektor datang 
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    //vektor keluar 
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    //menghitung vector dot, digunakan agar garis lintasan tidak digambar ketika terjadi tumbukan
                    float outDot = Vector2.Dot(outVector, hitNormal);

                    if(outDot > -1 && outDot < 1)
                    {
                        //gambar lintasan pantulan
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 20);
                        
                        //bola bayangan di prediksi titik tumbukan
                        drawBallAtCollision = true;
                    }
                }

                if (drawBallAtCollision)
                {
                    ballAtCollision.transform.position = offsetHitPoint;
                    ballAtCollision.SetActive(true);
                }
                else
                {
                    ballAtCollision.SetActive(false);
                }

                break;
            }
        }
    }
}
