using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


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
    private bool isPause;
    public GameObject menu;
    public PlayableDirector cutSlongDat;
    private bool dixuongdat = false;
    private bool dilen = false;
    private bool battu = false;
    private Color color_F;
    private SpriteRenderer spriteRenderer;
    public   PlayableDirector cutSBaylennho, cutSBaylenlon, cutSBaylenHP,ending;

    //public AudioClip souce_Nam;
    // Start is called before the first frame update

    //VienDan
    public GameObject FireBall;
    void Start()
    {
        isPause = false;
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
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color_F = spriteRenderer.color ;
        try
        {
            time.text = time_g + "";
            StartCoroutine(time_ne());

        }
        catch
        {

        }
        
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
        try
        {
            coin.text = coin_g + "";
            score.text = score_g + "";
        }
        catch
        {

        }
        
        animator.SetBool("isDangDungTrenSan", isDangDungTrenSan);
        animator.SetFloat("vanToc", vanToc);
        if(battu)
        {
            speed = 2f;
        }
        else
        {
            speed = 8f;
        }
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

                    if (!battu)
                    {

                        rigidbody2D.AddForce(new Vector2(0, 400));
                        
                    }
                    else
                    {
                        rigidbody2D.AddForce(new Vector2(0, 10));
                    }
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if(isPause)
            {
                menu.active = true;
                Time.timeScale = 0;
            }
            else
            {
                menu.active = false;
                Time.timeScale = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && dixuongdat && isDangDungTrenSan)
        {
            if (rigidbody2D.velocity.y > 0.1f && rigidbody2D.velocity.y < 0)
            {
                dixuongdat = false;
            }
            else
            {
                Debug.Log("Dixuong");
                cutSlongDat.Play();
            }
           
        }
        if(Input.GetKeyDown(KeyCode.RightArrow) && dilen && isDangDungTrenSan)
        {
            if (rigidbody2D.velocity.y > 0.1f && rigidbody2D.velocity.y < 0)
            {
                dilen = false;
            }
            else
            {
                Debug.Log("Dixuong");
                if(coSung)
                {
                    cutSBaylenHP.Play();
                }else if(bienLon)
                {
                    cutSBaylenlon.Play();
                }
                else
                {
                    cutSBaylennho.Play();

                }
            }
        }
       

        





    }
    private void FixedUpdate()
    {

        if (rigidbody2D.velocity.y < -1 || rigidbody2D.velocity.y > 0)
        {
            isDangDungTrenSan = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dixuongdat = false;
        dilen = false;
        if (collision.gameObject.tag == "matdat"
            || collision.gameObject.tag == "LuckyBox"
            || collision.gameObject.tag == "VienGach"
            || collision.gameObject.tag == "CaiCong"
            || collision.gameObject.tag == "BatThang"
            || collision.gameObject.tag == "HangNavi"
            || collision.gameObject.tag == "CaiCongThanKi"
            || collision.gameObject.tag == "LuckyBoxCoQua")
        {
            
            isDangDungTrenSan = true;
              

        }
        if (collision.gameObject.tag == "HangNavi")
        {

            dixuongdat = true;
        }
        if(collision.gameObject.tag == "CaiCongThanKi")
        {
            Vector3 vitri = collision.GetContact(0).normal;
            if(vitri.x < 0)
            {
                dilen = true;

            }
        }
        if (collision.gameObject.tag == "LuckyBox")
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
                PlaySounds("Sounds/Flagpole");
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
        if(collision.gameObject.tag == "ConRua")
        {
            if (((collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0) && collision.contacts[0].normal.y <= 0) && !collision.gameObject.GetComponent<ConRua>().isDead)
            {
                if (bienLon)
                {
                    bienLon = false;
                    coSung = false;
                    animator.SetBool("BienDoi", bienLon);
                    animator.SetBool("bienLon", bienLon);
                    StartCoroutine(SieuNhan());
                }
                else
                {
                    MarioIsLive = false;
                    animator.SetBool("chetTrongLong", true);
                    PlaySounds("Sounds/Die");
                    try
                    {
                        ((CayNam)(collision.transform.parent.gameObject.GetComponent<CayNam>())).speed = 0;

                    }
                    catch
                    {

                    }
                    rigidbody2D.AddForce(new Vector2(0, 200));
                    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
           
        }


    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (MarioIsLive && !battu)
        {
            if ((collision.gameObject.tag == "CayNamLeft" || collision.gameObject.tag == "CayNamRight" && !collision.transform.parent.gameObject.GetComponent<CayNam>().isDead ) || collision.gameObject.tag == "CapCap" )
            {
                if (bienLon)
                {
                    bienLon = false;
                    coSung = false;
                    animator.SetBool("BienDoi", bienLon);
                    animator.SetBool("bienLon", bienLon);
                    StartCoroutine(SieuNhan());
                }
                else
                {
                    MarioIsLive = false;
                    animator.SetBool("chetTrongLong", true);
                    PlaySounds("Sounds/Die");
                    try
                    {
                        ((CayNam)(collision.transform.parent.gameObject.GetComponent<CayNam>())).speed = 0;

                    }catch
                    {

                    }
                    rigidbody2D.AddForce(new Vector2(0, 200));
                    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                    

            }
            if (collision.gameObject.tag == "DongTienNgoaiBox")
            {
                coin_g++;
                score_g += 200;
                PlaySounds("Sounds/Coin");
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.tag == "caycot")
            {
                ending.Play();
                StartCoroutine(quamang("MangBoss"));
            }    
            
        }    
        
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

    //Hoi anti unity
    public IEnumerator SieuNhan()
    {
        battu = true;
        color_F.a = 0.5f;
        spriteRenderer.color = color_F;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rigidbody2D.gravityScale = 0f;
        transform.Translate(0, -0.3f, 0);
        yield return new WaitForSeconds(3f);

        color_F.a = 1f;
        spriteRenderer.color = color_F;
        transform.Translate(0, 0, 0);
        rigidbody2D.gravityScale = 1f;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        battu = false;

    }
    public void resumgame()
    {
        isPause = !isPause;
        menu.active = false;
        Time.timeScale = 1;
    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    private IEnumerator quamang(string man)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(man);
    }
}
