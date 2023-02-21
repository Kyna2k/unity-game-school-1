using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class HoaCo : MonoBehaviour
{
    public float speed;
    public float height;
    private GameObject item;
    public GameObject hoa;
    private Vector2 originPosition;//vi tri ban dau

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;

        item = Instantiate<GameObject>(hoa);

        item.transform.position = originPosition;
        StartCoroutine(GoUpAndDown(item));

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    IEnumerator GoUpAndDown(GameObject item)
    {
        while(true)
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
            while (true)
            {
                item.transform.position = new Vector3(
                    item.transform.position.x,
                    item.transform.position.y - speed * Time.deltaTime
                    );
                if (item.transform.position.y < originPosition.y)
                {
                    transform.position = originPosition;
                    break;
                }
                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }
        //nay len
        
    }
}
