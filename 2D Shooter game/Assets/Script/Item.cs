using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string type;
    Rigidbody2D r2d;

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        r2d.velocity = Vector2.down * 1.5f;
    }
}
