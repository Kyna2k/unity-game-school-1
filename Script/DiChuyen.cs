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
    private Animator die;
    private AudioSource audioSource;
    //public AudioClip souce_Nam;
    // Start is called before the first frame update
    void Start()
    {
        
        isRight = true;
        rigidbody2D= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vanToc = 0;
        isDangDungTrenSan = true;
        speed = 8f;
        audioSource = GetComponent<AudioSource>();
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
            PlaySounds("Sounds/Jump");
            if(isDangDungTrenSan)
            {
                rigidbody2D.AddForce(new Vector2(0, 400));
                isDangDungTrenSan = false;
            }
            

           
            
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
        if (collision.gameObject.tag == "CayNamLeft" || collision.gameObject.tag == "CayNamRight")
        {
            animator.SetBool("chetTrongLong", true);
            PlaySounds("Sounds/Die");
            rigidbody2D.AddForce(new Vector2(0, 200));
            gameObject.GetComponent<BoxCollider2D>().isTrigger= true;
        }
        if(collision.gameObject.tag == "CayNamTop")
        {
            die = collision.transform.parent.GetComponent<Animator>();
            PlaySounds("Sounds/Kick");
            die.SetBool("NamIsDie", true);
            StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(die));

        }
    }
    private IEnumerator CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(Animator enemy)
    {
        
        yield return new WaitForSeconds(0.3f);
        Destroy(enemy.gameObject);
    }

    public void PlaySounds(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }    
}
