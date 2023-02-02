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
    private bool MarioIsLive = true;

    private ContactPoint[] contacts;
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
            if (isDangDungTrenSan)
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
        if(collision.gameObject.tag == "LuckyBox")
        {
            Vector3 vitri = collision.GetContact(0).normal;
            Debug.Log(vitri.y);
            
            if(vitri.y < 0)
            {
                Animator AnimBox = collision.transform.GetChild(0).gameObject.GetComponent<Animator>();
                AnimBox.Play("LenXuongCaiHop");
                
                Debug.Log(collision.transform.GetChild(0));
                StartCoroutine(VienGachDaVeChoCuNoiTinhYeuChungTaBatDau(collision));
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        die = collision.transform.parent.GetComponent<Animator>();
        if(MarioIsLive)
        {
            if (collision.gameObject.tag == "CayNamLeft" || collision.gameObject.tag == "CayNamRight")
            {

                //Debug.Log(die.gameObject.transform.GetChild(2).gameObject.tag);   
                MarioIsLive = false;
                animator.SetBool("chetTrongLong", true);
                PlaySounds("Sounds/Die");
                ((CayNam)(collision.transform.parent.gameObject.GetComponent<CayNam>())).speed = 0;
                rigidbody2D.AddForce(new Vector2(0, 200));
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            }
            else if (collision.gameObject.tag == "CayNamTop" )
            {

                die.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                die.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                PlaySounds("Sounds/Kick");
                die.SetBool("NamIsDie", true);
                die.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(die));

            }
        }    
        
    }
    private IEnumerator CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(Animator enemy)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(enemy.gameObject);
        

    }

    private IEnumerator VienGachDaVeChoCuNoiTinhYeuChungTaBatDau(Collision2D collision2)
    {
        yield return new WaitForSeconds(0.2f);
        collision2.transform.GetChild(0).gameObject.SetActive(false);
        collision2.transform.GetChild(1).gameObject.SetActive(true);
        collision2.transform.GetChild(2).gameObject.SetActive(true);
        

    }
   
    public void PlaySounds(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }    
}
