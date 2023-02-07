using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;
    private new Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "matdat")
        {
            rigidbody2D.velocity = new Vector2(speed, Mathf.Abs(speed));
            
        }
    }
    public void setSpeed(float s)
    {
        speed = s;
    }
}
