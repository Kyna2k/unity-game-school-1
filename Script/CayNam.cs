using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class CayNam : MonoBehaviour
{
    public float left, right;
    public float speed;
    public new Rigidbody2D Rigidbody2D;
    public bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3;
        float x = transform.position.x;
        if (x < left)
        {
            isRight = true;
        }
        if (x > right)
        {
            isRight = false;
        }
        if (isRight)
        {
            vector3 = new Vector3(1, 0, 0);
        }
        else
        {
            vector3 = new Vector3(-1, 0, 0);
        }
        transform.Translate(vector3 * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[2];

        collision.GetContacts(contacts);

        if (collision.gameObject.tag == "matdat")
        {
            //Vector2 info = contacts[0].normal;

            //if (info.y == -1)
            //{
            //    gameObject.GetComponent<Rigidbody2D>().simulated= false;
            //}
        }
    }

}
