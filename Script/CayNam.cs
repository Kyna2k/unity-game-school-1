using System.Collections;
using System.Collections.Generic;
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
}
