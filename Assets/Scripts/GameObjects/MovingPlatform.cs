using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapCollider2D))]
public class MovingPlatform : MonoBehaviour
{
    private float dirX;
    private float speed = 3f;
    private bool movingRight = true;
    void Update()
    {
        if (transform.position.x > 4f)
        {
            movingRight = false;
        }
        else if (transform.position.x < -1f)
        {
            movingRight=true;
        }

        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
}
