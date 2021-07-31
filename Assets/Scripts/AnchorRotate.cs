using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorRotate : MonoBehaviour
{
    public float launchForce;
    public GameObject cubeOB, cube;
    private GameObject projectile;
    private bool launched;
    PlayerController thisBody, mainBody;
    // Start is called before the first frame update
    void Start()
    {
        projectile = transform.GetChild(0).gameObject;
        thisBody = transform.parent.GetComponent<PlayerController>();
        mainBody = projectile.transform.GetChild(0).GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!launched)
        {
            transform.Rotate(Vector3.forward * Input.GetAxis("Mouse X") * -10);
        }

        if (Input.GetMouseButtonDown(0) && !launched)
        {
            Launch();
            cube = Instantiate(cubeOB, transform.position, Quaternion.identity);
            //cube.transform.parent = transform;
        }

        if(!mainBody.stretched || !thisBody.stretched)
        {
            mainBody.stretched = false;
            thisBody.stretched = false;            
            projectile.GetComponent<ProjectileBehaviour>().ResetProjectile();
        }

        if(cube!=null)
        {
            cube.transform.LookAt(projectile.transform);
            cube.transform.localScale = new Vector3(cube.transform.localScale.x, cube.transform.localScale.y, Vector3.Distance(transform.position, projectile.transform.position));
        }
            
    }

    private void OnDisable()
    {
        if (cube != null)
        {
            Destroy(cube);
        }
    }

    void Launch()
    {
        //GetComponent<Rigidbody>().isKinematic = false;
        launched = true;        
        Vector3 dir = projectile.transform.position - transform.position;
        BlobSelector.instance.selectedBlob = projectile;
        //float ang = Vector3.Angle(Vector3.right, dir);
        //projectile.transform.parent = null;
        projectile.GetComponent<Rigidbody>().isKinematic = false;
        projectile.GetComponent<Rigidbody>().useGravity = true;
        projectile.GetComponent<Rigidbody>().AddRelativeForce(dir * launchForce);               

    }
}
