using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates an illusion of and infinite road line
/// </summary>
public class InfiniteRoadLine : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Transform cameraTransform;
    float startDistance;

    /// <summary>
    /// size of the road line
    /// </summary>
    public Vector2 size
    {
        get
        {
            return boxCollider.size;
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        cameraTransform = Camera.main.transform.parent;
        startDistance = cameraTransform.position.y - transform.position.y;
    }

    /// <summary>
    /// Moves and resets the road line base on the camera position
    /// </summary>
    void Update()
    {
        FakeMovement();
        float distance = cameraTransform.position.y - transform.position.y;

        if (Mathf.Abs(distance) >= startDistance + size.y)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = cameraTransform.position.y - startDistance;
            transform.position = newPosition;
        }

    }

    /// <summary>
    /// It's called in the main menu just as an effect of a car moving
    /// </summary>
    void FakeMovement()
    {
        if (!(CarController.instance.velocity.y > 0))
        {
            transform.Translate(Vector3.down * GameManager.instance.data.speed * Time.deltaTime);
        }
    }
}
