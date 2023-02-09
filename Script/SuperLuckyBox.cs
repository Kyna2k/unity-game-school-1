using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperLuckyBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject item;
    void Start()
    {
        Debug.Log(gameObject.transform.position.x);
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
            Debug.Log("??:??");

            GameObject mini = Instantiate(item);
            mini.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2f);
        }
    }
}
