using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    private int time_g, coin_g, score_g;
    public GameObject bangdiem;
    public bool bienLon;
    public bool coSung;
    public Text coin;
    public Text score;
    public Text time;
    //public AudioClip souce_Nam;
    // Start is called before the first frame update

    //VienDan
    public GameObject FireBall;
    void Start()
    {
        bienLon = false;
        isRight = true;
        coSung = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vanToc = 0;
        isDangDungTrenSan = true;
        speed = 8f;
        audioSource = GetComponent<AudioSource>();
        coin_g = 0; score_g = 0;time_g = 300;

        time.text = time_g + "";
        StartCoroutine(time_ne());
        
    }

    private IEnumerator time_ne()
    {
        while (time_g > 0 && MarioIsLive)
        {
            time_g--;
            time.text = time_g + "";
            yield return new WaitForSeconds(1);
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
        coin.text = coin_g + "";
        score.text = score_g + "";
        animator.SetBool("isDangDungTrenSan", isDangDungTrenSan);
        animator.SetFloat("vanToc", vanToc);
        if(MarioIsLive)
        {
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
                if (isDangDungTrenSan)
                {
                    PlaySounds("Sounds/Jump");

                    rigidbody2D.AddForce(new Vector2(0, 400));
                    isDangDungTrenSan = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.X) && coSung)
            {
                GameObject fb = Instantiate(FireBall);
                fb.transform.position = new Vector3(
                    transform.position.x + (isRight ? 0.8f : -1), transform.position.y);
                fb.GetComponent<FireBall>().setSpeed(isRight ? 5f : -5f);
            }

        }

    }
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "matdat"
            || collision.gameObject.tag == "LuckyBox"
            || collision.gameObject.tag == "VienGach"
            || collision.gameObject.tag == "CaiCong"
            || collision.gameObject.tag == "BatThang")
        {
            
            isDangDungTrenSan = true;

        }
        if(collision.gameObject.tag == "LuckyBox")
        {
            Vector3 vitri = collision.GetContact(0).normal;
            
            if(vitri.y < 0)
            {
                Animator AnimBox = collision.transform.GetChild(0).gameObject.GetComponent<Animator>();
                AnimBox.Play("LenXuongCaiHop");
                if(collision.transform.GetChild(0).gameObject.active)
                {
                    coin_g++;
                    score_g += 200;
                    PlaySounds("Sounds/Coin");
                    StartCoroutine(VienGachDaVoChoCuNoiTinhYeuChungTaBatDau(collision.gameObject));

                }
            }
        }
        if(collision.gameObject.tag == "ThuocTangTruong")
        {
            if(!bienLon)
            {
                bienLon = true;
                animator.SetBool("BienDoi", bienLon);
                animator.SetBool("bienLon", bienLon);
            }    
            
        }
        if(collision.gameObject.tag == "HoaCo" && bienLon)
        {
            if(!coSung)
            {
                coSung = true;
                animator.SetBool("coSung", coSung);
            }
        }
        if (collision.gameObject.tag == "HoaCo")
        {
            coSung = true;
            Destroy(collision.gameObject);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (MarioIsLive)
        {
            
         
                if (collision.gameObject.tag == "CayNamLeft" || collision.gameObject.tag == "CayNamRight")
                {
                    if (bienLon)
                    {
                        bienLon = false;
                        coSung = false;
                        animator.SetBool("BienDoi", bienLon);
                        animator.SetBool("bienLon", bienLon);
                }
                    else
                    {
                        MarioIsLive = false;
                        animator.SetBool("chetTrongLong", true);
                        PlaySounds("Sounds/Die");
                        ((CayNam)(collision.transform.parent.gameObject.GetComponent<CayNam>())).speed = 0;
                        rigidbody2D.AddForce(new Vector2(0, 200));
                        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    }
                    

                }
                else if (collision.gameObject.tag == "CayNamTop")
                {
                    die = collision.transform.parent.GetComponent<Animator>();

                    score_g += 100;
                    die.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    die.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    PlaySounds("Sounds/Kick");
                    die.SetBool("NamIsDie", true);
                    GameObject scope = Instantiate(bangdiem);
                    scope.transform.position = new Vector3(die.gameObject.transform.position.x, die.gameObject.transform.position.y);
                    Destroy(scope, 0.5f);
                    StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(die));

                }
            
        }    
        
    }
    private IEnumerator CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(Animator enemy)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(enemy.gameObject);
        

    }

    private IEnumerator VienGachDaVoChoCuNoiTinhYeuChungTaBatDau(GameObject gameObject)
    {
        
        yield return new WaitForSeconds(0.15f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        

    }
   
    public void PlaySounds(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }    
}
