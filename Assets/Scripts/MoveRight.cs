using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveRight : MonoBehaviour
{

    //[SerializeField]
    //public RectTransform gameOverPanel;

    public int destroyTime = 8;

    private GlobalManager gm;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", destroyTime);
        gm = GameObject.FindObjectOfType<GlobalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale!=0)
        transform.position = new Vector3(transform.position.x - gm.worldSpeed * Time.deltaTime, transform.position.y, transform.position.z) ;
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
