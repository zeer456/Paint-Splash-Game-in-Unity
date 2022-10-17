using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotObject : MonoBehaviour
{
    public GameObject Painting;
    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Window");
    }
    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("heat");
        //ContactPoint contact = collision.contacts[0];
        //Vector3 pos = contact.point;


        if (collision.GetComponent<Collider>().name == "Background")
        {
            Debug.Log(collision.GetComponent<Collider>().name);
            Destroy(this.gameObject);
        }
        else if (collision.GetComponent<Collider>().name == "Window")
        {
            Debug.Log(collision.GetComponent<Collider>().name);
            Destroy(this.gameObject);
            //var painting = Instantiate(Painting);
            //painting.transform.parent = canvas.transform;
            //painting.transform.position = pos;
            //painting.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Window")
        {
            Debug.Log("hello");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
