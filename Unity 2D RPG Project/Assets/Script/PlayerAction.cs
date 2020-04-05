using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;

    float h;
    float v;

    Rigidbody2D r2d;

    // Start is called before the first frame update
    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        r2d.velocity = new Vector2(h, v) * 2;
    }
}
