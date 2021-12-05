using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgGenerator : MonoBehaviour
{


    //public int xSize;
    //public int zSize;

    public mesh_gen background;

    private mesh_gen instance1;
    private mesh_gen instance2;

    // Start is called before the first frame update
    void Start()
    {
        //background.xSize = xSize;
        //background.zSize = zSize;
        instance1 = Instantiate(background, new Vector3(0, -1.1f, 0), Quaternion.identity);
        //mesh_gen.xOffset = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (instance1.gameObject.transform.position.x <= -100)
        {
            instance2 = instance1;
            instance1 = Instantiate(background, new Vector3(instance2.transform.position.x+199.88f, -1.1f, 0), Quaternion.identity);
            //mesh_gen.xOffset += 60;
        }
    }
}
