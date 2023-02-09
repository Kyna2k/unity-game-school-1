using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienGach : MonoBehaviour
{
    public GameObject minigach;
    private float x, y;
    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
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

            GameObject mini = Instantiate(minigach);
            Destroy(gameObject);
            mini.transform.position = new Vector2(x,y);
        }    
    }

}
