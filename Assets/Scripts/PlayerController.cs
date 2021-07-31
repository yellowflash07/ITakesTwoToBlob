using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpForce;
    private Rigidbody rb;
    public bool focused, grounded;
    public GameObject blob;

    private GameObject bounceBlob, anchor;
    public bool paired, stretched;
    public List<GameObject> pairedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paired)
        {
            Debug.Log("Press F to seperate");
            if (Input.GetKeyDown(KeyCode.F))
            {
                BlobSelector.instance.selectedBlob = pairedObjects[0];
                foreach (var po in pairedObjects)
                {
                    po.transform.parent = null;
                    po.gameObject.SetActive(true);
                }
                Destroy(gameObject);
            }
        }

        if(stretched)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                BlobSelector.instance.selectedBlob = pairedObjects[0];
                foreach (var po in pairedObjects)
                {
                    if(po.GetComponent<PlayerController>().anchor!=null)
                    {
                        po.GetComponent<PlayerController>().anchor.SetActive(false);
                    }
                    po.transform.eulerAngles = Vector3.zero;
                    po.transform.parent = null;
                    po.GetComponent<PlayerController>().stretched = false;
                    po.gameObject.SetActive(true);
                  //  pairedObjects.Remove(po);
                }                             
            }
        }

        if(!stretched)
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
           // transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump(jumpForce);
            }
        }

        
    }

    void Jump(float JumpForce)
    {
        foreach (var rb in transform.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddForce(Vector3.up * JumpForce);
        }
        rb.AddForce(Vector3.up * JumpForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered" + other.name);
        if(other.tag == "bouncepad")
        {
            Jump(200);
        } 
        
        if(other.tag == "FallDetect")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(other.tag == "EndPoint")
        {
            GameManager.instance.win = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Press E to combine");
            if(Input.GetKeyDown(KeyCode.E))
            {
                //paired = true;
                if (other.tag == "2")
                {                

                    bounceBlob = Instantiate(blob, other.transform.position, Quaternion.identity);
                    bounceBlob.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                    gameObject.SetActive(false);
                    other.gameObject.SetActive(false);

                    transform.parent = bounceBlob.transform;
                    other.transform.parent = bounceBlob.transform;
                    PlayerController bb = bounceBlob.GetComponent<PlayerController>();
                    bb.jumpForce *= 2.5f;
                    bb.paired = true;
                    bb.pairedObjects.Add(gameObject);
                    bb.pairedObjects.Add(other.gameObject);                    

                    BlobSelector.instance.selectedBlob = bounceBlob;                  
                                                     
                }
                
                if(other.tag == "3" && bounceBlob==null)
                {                   
                    anchor = other.transform.GetChild(2).gameObject;
                    gameObject.transform.parent = anchor.transform.GetChild(0);
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.SetActive(false);
                    anchor.SetActive(true);
                    other.GetComponent<PlayerController>().stretched = true;
                    stretched = true;

                   // gameObject.GetComponent<Rigidbody>().isKinematic = true;

                    BlobSelector.instance.selectedBlob = other.gameObject;

                    other.GetComponent<PlayerController>().pairedObjects.Add(gameObject);
                    other.GetComponent<PlayerController>().pairedObjects.Add(other.gameObject);
                    pairedObjects.Add(gameObject);
                    pairedObjects.Add(other.gameObject);

                }
            }

            
        }
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ground")
        {
            grounded = false;
        }
    }




}
