using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ObjectScaleLabel : MonoBehaviour
{
    public GameObject scaleTextPrefab;

    private GameObject thisText;

    private void Start()
    {
        Instantiate(scaleTextPrefab, transform);
        thisText = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisText.activeSelf != GameManager.instance.inLevel)
        {
            thisText.SetActive(GameManager.instance.inLevel);
        }

        //assign text of scale to the mass value
        decimal roundedDecimal = decimal.Round((decimal)transform.GetComponent<Rigidbody>().mass, 1);
        thisText.GetComponent<TextMeshProUGUI>().text = roundedDecimal.ToString();
    }

    private void FixedUpdate()
    {
        //print(Camera.main.WorldToScreenPoint(transform.position));
        thisText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
