using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyAnNau : MonoBehaviour
{
    public float speed;
    public float height;

    private Vector2 originPosition;//vi tri ban dau

    public Sprite EmtyBlock;

    private bool canChange; // khoi bi va cham hay ch
    private GameObject item;
    public GameObject item_nam;
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        canChange = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //gameObject.GetComponent<BoxCollider2D>().isTrigger= true;
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
                gameObject.GetComponent<SpriteRenderer>().enabled = true;

                //khoa khoi
                canChange = false;
                //chuyen sang khoi khac
                GetComponent<SpriteRenderer>().sprite = EmtyBlock;
                GetComponent<Animator>().enabled = false;
                //nay len roi xuong
                StartCoroutine(GoUpAndDown());
                //tao vat pham nay len
                item = Instantiate<GameObject>(item_nam);

                item.transform.position = originPosition;
                StartCoroutine(ItemGoUp(item));

            }



        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    IEnumerator ItemGoUp(GameObject item)
    {
        while (true)
        {
            item.transform.position = new Vector3(
                item.transform.position.x,
                item.transform.position.y + speed * Time.deltaTime
                );
            if (item.transform.position.y > originPosition.y + height)
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
