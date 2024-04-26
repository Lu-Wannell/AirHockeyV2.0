using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal {  get; private set; }
    public float MaxSpeed;

    private Rigidbody2D rb;

    public GameObject AirHockeyPuck;
    public GameObject PlayerHitter;
    public GameObject AiHitter;
    public GameObject PointZone;
    public static bool WasHit {  get; private set; }
    public int AiCollisions = 0;
    public int PlayerCollisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
        WasHit = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal)
        {
            if (other.tag == "AiGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                StartCoroutine(ResetPuck());
                PlayerCollisions = 0;
                AiCollisions = 0;
            }
            else if (other.tag == "PlayerGoal")
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                StartCoroutine(ResetPuck());
                PlayerCollisions = 0;
                AiCollisions = 0;
            }
            else if (other.tag == "PointZone")
            {
                if (PlayerCollisions > 0)
                {
                    ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                }
                else if (AiCollisions > 0)
                {
                    ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                }
                WasGoal = true;
                StartCoroutine(ResetPuckPointZone());
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D hitter)
    {
        if (!WasHit)

        if (hitter.collider == AiHitter.GetComponent<Collider2D>())
        {
            AiCollisions += 1;
            

            if (AiCollisions == 1)
            {
                PlayerCollisions = 0;
            }
            else if (AiCollisions == 2)
            {
                PlayerCollisions = 0;
                ScoreScriptInstance.Decrement(ScoreScript.Score.AiScore);
            }
            else
            {
                AiCollisions = 0;
            }

            WasHit = true;
            StartCoroutine(ResetCollisions());
        }
        else if (hitter.collider == PlayerHitter.GetComponent<Collider2D>())
            {
            PlayerCollisions += 1;

            if (PlayerCollisions == 1)
            {
                AiCollisions = 0;
            }
            else if (PlayerCollisions == 2)
            {
                AiCollisions = 0;
                ScoreScriptInstance.Decrement(ScoreScript.Score.PlayerScore);
                PlayerCollisions = 0;
            }
            else
            {
                PlayerCollisions = 0;
            }

            WasHit = true;
            StartCoroutine(ResetCollisions());
        }
        /*else if (hitter.collider == PointZone.GetComponent<Collider2D>())
        {
            if (PlayerCollisions > 0)
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
            }
            else if (AiCollisions > 0)
            {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
            }
            WasHit = true;
            StartCoroutine(ResetCollisions());
        }*/

    }

   

    private IEnumerator ResetCollisions()
    {
        yield return new WaitForSecondsRealtime(0);
        WasHit = false;

    }

    private IEnumerator ResetPuck()
    {
        WasGoal = false;
        rb.velocity = rb.position = new Vector2(-20, 0);
        yield return new WaitForSecondsRealtime(1);
        rb.velocity = rb.position = new Vector2(0, 0);
    }

    private IEnumerator ResetPuckPointZone()
    {
        WasGoal = false;
        yield return new WaitForSecondsRealtime(1);
        
    }

    public void CenterPuck()
    {
        rb.position = new Vector2(0, 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
