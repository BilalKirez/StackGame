using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float moveSpeed = 2f;
    public bool isMoving = false;
    private Vector3 targetPosition;
    public Animator animator;

    private void Start()
    {
        isMoving = true;
        animator.SetBool("Stop", false);
    }
    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = new Vector3(position.x, 0, position.z);
        isMoving = true;
        animator.SetBool("Stop", false);
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
            animator.SetBool("Stop", true);
        }
    }
}
