using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileBehaviour : MonoBehaviour
{

    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.parent = transform.parent;
            gameObject.SetActive(false);
            // transform.GetChild(0).gameObject.transform.localPosition = transform.position;
            // transform.GetChild(0).transform.parent = null;
            //  transform.parent = GameObject.FindGameObjectWithTag("3").transform.GetChild(2).transform; 
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FallDetect")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetProjectile()
    {
        transform.localPosition = startPos;
    }
}
