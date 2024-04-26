using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public float MaxMovementSpeed;
    private Rigidbody2D rb;
    private Vector2 startingPosition;

    public Rigidbody2D puck;

    public Transform PlayerBoundaryHolder;
    private Boundary playerBoundary;

    public Transform PuckBoundaryHolder;
    private Boundary puckBoundary;

    private Vector2 targetposition;

    private bool isFirstTimeInOpponentsHalf = true;
    private float offsetYFromTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;

        playerBoundary = new Boundary(PlayerBoundaryHolder.GetChild(0).position.y, PlayerBoundaryHolder.GetChild(1).position.y,
            PlayerBoundaryHolder.GetChild(2).position.x, PlayerBoundaryHolder.GetChild(3).position.x);

        puckBoundary = new Boundary(PuckBoundaryHolder.GetChild(0).position.y, PuckBoundaryHolder.GetChild(1).position.y,
            PuckBoundaryHolder.GetChild(2).position.x, PuckBoundaryHolder.GetChild(3).position.x);

        GetComponent<AIScript>().enabled = false;
    }

    private void FixedUpdate()
    {
        if (!PuckScript.WasGoal)
        {
            float movementSpeed;

            if (puck.position.x < puckBoundary.Left)
            {
                if (isFirstTimeInOpponentsHalf)
                {
                    isFirstTimeInOpponentsHalf = false;
                    offsetYFromTarget = Random.Range(-1f, 1f);
                }
                movementSpeed = MaxMovementSpeed * Random.Range(0.1f, 0.3f);
                targetposition = new Vector2(startingPosition.x, Mathf.Clamp(puck.position.y + offsetYFromTarget, playerBoundary.Down, playerBoundary.Up));
            }
            else
            {
                isFirstTimeInOpponentsHalf = true;

                movementSpeed = Random.Range(MaxMovementSpeed * 0.4f, MaxMovementSpeed);
                targetposition = new Vector2(Mathf.Clamp(puck.position.x, playerBoundary.Left, playerBoundary.Right),
                    Mathf.Clamp(puck.position.y, playerBoundary.Down, playerBoundary.Up));

            }

            rb.MovePosition(Vector2.MoveTowards(rb.position, targetposition, movementSpeed * Time.fixedDeltaTime));
        }
    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }
}
