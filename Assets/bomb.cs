using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    [SerializeField] GameObject original;
    // Start is called before the first frame update
    void Start()
    {
        addBomb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addBomb()
    {
        var position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        Instantiate(original, position, Quaternion.identity);
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        Instantiate(original, position, Quaternion.identity);
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        Instantiate(original, position, Quaternion.identity);
        position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), 0);
        Instantiate(original, position, Quaternion.identity);
    }

}
