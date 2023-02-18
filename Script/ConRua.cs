using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ConRua : MonoBehaviour
{
    public float left, right;
    public float speed;
    private Rigidbody2D Rigidbody2D;
    public bool isRight = true;
    private bool isDead;
    float scaleLocalY;
    float positionLocalY;

    private AudioSource audioSource;
    public GameObject bangdiem;
    public Sprite ruanam;
    public Animator animator;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {


        isDead = false;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        scaleLocalY = gameObject.transform.localScale.y;
        positionLocalY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        float potitionX = transform.position.x;
        if (potitionX < left)
        {
            isRight = true;
        }
        if (potitionX > right)
        {
            isRight = false;
        }
        Vector3 vector3;
        if (isRight)
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
            vector3 = new Vector3(1, 0, 0);
        }
        else
        {
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;

            vector3 = new Vector3(-1, 0, 0);

        }
        transform.Translate(vector3 * speed * Time.deltaTime);



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[2];

        collision.GetContacts(contacts);
        if (!isDead)
        {
            if (collision.gameObject.tag == "FireBall")
            {
                isDead = true;
                GameObject scope = Instantiate(bangdiem);
                scope.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
                Destroy(scope, 0.5f);
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, scaleLocalY * -1);
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
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

                PlaySounds("Sounds/Kick");
                StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(gameObject, 3));

            }
            else if (collision.gameObject.tag == "Mario")
            {
                if (collision.contacts[0].normal.y < 0)
                {
                    isDead = true;
                    gameObject.GetComponent<SpriteRenderer>().sprite = ruanam;
                    
                    PlaySounds("Sounds/Kick");
                    GameObject scope = Instantiate(bangdiem);
                    gameObject.GetComponent<Animator>().SetBool("Kill", true);
                    scope.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
                    Destroy(scope, 0.5f);
                    StartCoroutine(CayNamChetNhungTinhYeuAnhDanhChoEmVanConDo(gameObject, 0.3f));
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
        enemy.GetComponent<BoxCollider2D>().isTrigger= true;
        yield return new WaitForSeconds(time);
        Destroy(enemy);


    }
    public void PlaySounds(string name)
    {

        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }
}
