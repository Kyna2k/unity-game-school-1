using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class SuperLuckyBox : MonoBehaviour
{
    public float speed;
    public float height;

    private Vector2 originPosition;//vi tri ban dau

    public Sprite EmtyBlock;
    private AudioSource audioSource;
    private bool canChange; // khoi bi va cham hay ch
    private  GameObject item;
    public GameObject item_nam;
    public GameObject item_hoa;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originPosition = transform.position;
        canChange = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canChange) return;
        if (collision.gameObject.tag == "Mario")
        {
            var direction = collision.GetContact(0).normal;
            if (direction.y > 0)
            {
                PlaySounds("Sounds/Item");
                //khoa khoi
                canChange = false;
                //chuyen sang khoi khac
                GetComponent<SpriteRenderer>().sprite = EmtyBlock;
                GetComponent<Animator>().enabled = false;
                //nay len roi xuong
                StartCoroutine(GoUpAndDown());
                //tao vat pham nay len
                if(!collision.gameObject.GetComponent<DiChuyen>().bienLon)
                {
                    item = Instantiate<GameObject>(item_nam);
                    
                }
                else
                {
                    item = Instantiate<GameObject>(item_hoa);
                        
                }
                item.transform.position = originPosition;
                StartCoroutine(ItemGoUp(item));

            }



        }
    }
    public void PlaySounds(string name)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }
    IEnumerator ItemGoUp(GameObject item)
    {
        while (true)
        {
            item.transform.position = new Vector3(
                item.transform.position.x,
                item.transform.position.y + speed * Time.deltaTime
                );
            if (item.transform.position.y > originPosition.y + height )
            {

                break;
            }
            yield return null;
        }
    }

    IEnumerator GoUpAndDown()
    {
        //nay len
        while (true)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime
                );
            if (transform.position.y > originPosition.y + height)
            {

                break;
            }
            yield return null;
        }
        while (true)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - speed * Time.deltaTime
                );
            if (transform.position.y < originPosition.y)
            {
                transform.position = originPosition;
                break;
            }
            yield return null;
        }
    }
}
