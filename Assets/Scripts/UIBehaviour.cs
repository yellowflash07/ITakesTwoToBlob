using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIBehaviour : MonoBehaviour
{
    public Text instructText;
    public string[] instructions;
    public List<GameObject> info;

    public float value;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.FindGameObjectsWithTag("Info").ToList();
        instructText = GameObject.FindGameObjectWithTag("InfoText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "1" || other.tag == "2" || other.tag == "3")
        {
            instructText.text = instructions[info.IndexOf(gameObject)];
        }
    }
}
