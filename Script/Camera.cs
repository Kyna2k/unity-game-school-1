using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    const float vitridot2 = 17.8f;
    const float vitridot3 = 25.3f;
    public GameObject quaidot2;
    public GameObject quaidot3;
    // Start is called before the first frame update
    public float left, right;
    public GameObject Mario;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var CameraX = transform.position.x;
        var CameraY = transform.position.y;
        if (Mario.transform.position.x >= left && Mario.transform.position.x <= right)
        {
            transform.position = new Vector3(Mario.transform.position.x, transform.position.y,
            transform.position.z);
        }
        else
        {
            if(CameraX < left) CameraX= left;
            if(CameraX < right) CameraX= right;
        }
        Debug.Log(CameraX);
        if(transform.position.x >= vitridot2)
        {
            quaidot2.SetActive(true);
        }
        if(transform.position.x >= vitridot3)
        {
            quaidot3.SetActive(true);
        }
    }
}
