using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConRua : MonoBehaviour
{
    public float left, right;
    public float speed;
    public new Rigidbody2D Rigidbody2D;
    public bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3;
        float x = transform.position.x;
        if(x < left)
        {
            isRight = true;
        }
        if(x > right) 
        {
            isRight= false;
        }
        if(isRight)
        {
            vector3 = new Vector3(1,0,0);
            quayDaulaHipHop(isRight);


        }
        else
        {
            vector3= new Vector3(-1,0,0);
            quayDaulaHipHop(isRight);

        }
        transform.Translate(vector3 * speed * Time.deltaTime);
        
    }
    private void quayDaulaHipHop(bool value)

    {
        Vector2 scale = transform.localScale;
        if(!value)
        {
            scale.x *= scale.x > 0 ? 1 : -1;

        }
        else
        {
            scale.x *= scale.x > 0 ? -1 : 1;

        }
        transform.localScale = scale;
        Debug.Log(scale.x);
    }
}
