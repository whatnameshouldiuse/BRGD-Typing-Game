using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [Header("Spawn Range")]
    public int XMin = -5;
    public int YMin = -5;
    [Space]
    public int XMax = 5;
    public int YMax = 5;

    [Header("Pop-Up Game Objects")]
    public GameObject PopUpContainer;
    public GameObject[] PopUpList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Test
        if (Input.GetMouseButtonDown(0))
        {
            CreatePopUp();
        }
    }

    private void CreatePopUp()
    {
        GameObject spawningObject_prefab = PopUpList[Random.Range(0, PopUpList.Length)];

        GameObject spawningObject = Instantiate(spawningObject_prefab);
        spawningObject.transform.SetParent(PopUpContainer.transform);
        spawningObject.transform.position = new Vector3(Random.Range(XMin, XMax), Random.Range(YMin, YMax), 0);
    }
}
