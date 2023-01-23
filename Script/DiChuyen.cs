using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiChuyen : MonoBehaviour
{
    public float speed;
    public new Rigidbody2D rigidbody2D;
    public bool isRight;
    public Animator animator;
    public float vanToc;
    private bool isDangDungTrenSan;
    // Start is called before the first frame update
    void Start()
    {
        
        isRight = true;
        rigidbody2D= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vanToc = 0;
        isDangDungTrenSan = true;
        speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isDangDungTrenSan", isDangDungTrenSan);
        animator.SetFloat("vanToc", vanToc);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //

            if (!isRight)
            {
                Vector2 scale = transform.localScale;
                scale.x *= scale.x > 0 ? 1 : -1;
                transform.localScale = scale;
                isRight = true;
            }
            transform.Translate(Time.deltaTime * speed, 0, 0);
            //rigidbody2D.velocity= new Vector2(speed,0);
            vanToc = speed;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            vanToc = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isRight)
            {
                Vector2 scale = transform.localScale;
                scale.x *= scale.x > 0 ? -1 : 1;
                transform.localScale = scale;
                isRight = false;
            }
            transform.Translate(-Time.deltaTime * speed, 0, 0);

            vanToc = speed;

        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            vanToc = 0;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(new Vector2(0, 300));
            isDangDungTrenSan = false;
            
        }




    }
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "matdat")
        {
            
            isDangDungTrenSan = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
