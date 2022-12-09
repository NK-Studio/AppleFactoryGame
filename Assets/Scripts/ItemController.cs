using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float Speed { get; set; } = 0.03f;

    private void Update()
    {
        Move();
        OnDeadLine();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * (Speed * Time.deltaTime));
    }

    private void OnDeadLine()
    {
        bool isDeadLine = transform.position.y < -1.0f;

        if (isDeadLine)
            Destroy(gameObject);
    }
}