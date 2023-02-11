using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienGach : MonoBehaviour
{

    public float speed;
    public float height;
    public GameObject minigach;
    private Vector2 originPosition;
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[2];
        collision.GetContacts(contacts);
        if (contacts[0].normal.y > 0)
        {
            if(collision.gameObject.tag == "Mario")
            {
                if(!collision.gameObject.GetComponent<DiChuyen>().bienLon)
                {
                    StartCoroutine(GoUpAndDown());
                }
                else
                {
                    GameObject mini = Instantiate(minigach);
                    Destroy(gameObject);
                    mini.transform.position = originPosition;
                }    
            }    
            
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
