using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointZone : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startingPosition;
    private bool canChangePosition = true;
    private bool visible = true;

    void Start()
    {
        startingPosition = new Vector2(0, 0);
        
        InvokeRepeating("ChangePosition", 0, 4); //calls ChangePosition() every 2 secs
    }

    
    void ChangePosition()
    {
        if (visible)
        {
            transform.position = startingPosition;

            startingPosition = new Vector2(Random.Range(-5, 5), Random.Range(-4, 4));
            visible = false;
        }
        else
        {
            transform.position = startingPosition;

            startingPosition = new Vector2(20, 20);
            visible = true;
        }
       
    }

  /*  private IEnumerator ResetPuck()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-4f, 4f);
        float time = Random.Range(2f, 5f);

        yield return new WaitForSecondsRealtime(time);
        rb.velocity = rb.position = new Vector2(x, y);
        yield return new WaitForSecondsRealtime(time);
        rb.velocity = rb.position = new Vector2(20, 20);
        canChangePosition = false;
    }*/
}
