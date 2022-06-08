using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    public Text collectiblesText;
    private int counter = 0;
    private GameObject[] objs;

    // Start is called before the first frame update
    void Start()
    {
        
        objs = GameObject.FindGameObjectsWithTag("Collectible");
    }

    // Update is called once per frame
    void Update()
    {
        collectiblesText.text = counter.ToString() + "/" + objs.Length.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectible")
        {
            Debug.Log("Triggered by Collectible");
            counter += 1;
            Destroy(other.gameObject);
        }
    }
}
