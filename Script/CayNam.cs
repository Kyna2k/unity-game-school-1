using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class CayNam : MonoBehaviour
{
    public float left, right;
    public float speed;
    private Rigidbody2D rigidbody2D;
    float scaleLocalY;
    float positionLocalY;
    public bool isRight;
    private AudioSource audioSource;
    public GameObject bangdiem;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        scaleLocalY = gameObject.transform.localScale.y;
        positionLocalY = gameObject.transform.position.y;
        audioSource = GetComponent<AudioSource>();
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
        ContactPoint2D[] contacts = new ContactPoint2D[2];

        collision.GetContacts(contacts);
        if(!isDead)
        {
            if (collision.gameObject.tag == "FireBall")
            {
                isDead = true;
                GameObject scope = Instantiate(bangdiem);
                scope.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
                Destroy(scope, 0.5f);
                collision.gameObject.GetComponent<Animator>().SetBool("Kill", true);
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, scaleLocalY * -1);
                if (contacts[0].normal.x > 0)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.5f, positionLocalY + 1f);

                }
                else
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.5f, positionLocalY + 1f);

                }
                gameObject.GetComponent<Collider2D>().isTrigger = true;
                Destroy(collision.gameObject, 0.1f);

                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                PlaySounds("Sounds/Kick");
                StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(gameObject,3));

            }
            else if (collision.gameObject.tag == "Mario")
            {
                if(collision.contacts[0].normal.y < 0)
                {
                    var die = gameObject.GetComponent<Animator>();
                    isDead = true;
                    die.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    die.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    PlaySounds("Sounds/Kick");
                    die.SetBool("NamIsDie", true);
                    GameObject scope = Instantiate(bangdiem);
                    scope.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
                    Destroy(scope, 0.5f);
                    StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(gameObject,0.3f));
                }
            }
            else if (collision.gameObject.tag == "matdat")
            {

            }
            else
            {
                isRight = !isRight;
            }
        }
        
        
    }

    private IEnumerator CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(GameObject enemy, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(enemy);


    }
    public void PlaySounds(string name)
    {

        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

    }

}
