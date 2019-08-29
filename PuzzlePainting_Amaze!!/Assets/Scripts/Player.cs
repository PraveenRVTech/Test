using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    int paintCount;
    public int ColorCube;

    //Rigidbody _rb;
    ConstantForce _constforce;

    [SerializeField]
    private float speed;
    
    public GameObject GameOverText;
    public GameObject NextLevelText;
    public GameObject RestartBTN;
    [HideInInspector]
    public bool up, right, down, left;

    int NextLevel;
    public List<GameObject> Levels;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        paintCount = 0;
        NextLevel = 0;
       // _rb = GetComponent<Rigidbody>();
       _constforce = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow)|| up) {
            // transform.Translate(new Vector3(0, 0, 1f * Time.deltaTime * 100f));
            _constforce.force = new Vector3(0, 0, speed);
        }
        if (Input.GetKey(KeyCode.DownArrow)|| down)
        {
            _constforce.force = new Vector3(0, 0, -speed);
        }
        if (Input.GetKey(KeyCode.RightArrow)|| right)
        {
            _constforce.force = new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || left)
        {
            _constforce.force = new Vector3(-speed, 0, 0);
        }

        up = false;
        down = false;
        right = false;
        left = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Color")
        {
            paintCount++;
//            Debug.Log(paintCount + " : TotalPaintDone.");
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (paintCount == ColorCube)
            {
                _constforce.force = new Vector3(0, 0, 0);
                Invoke("GameOver", 2f);
                //GameOverText.SetActive(true);
                NextLevelText.SetActive(true);
            }
        }
    }
            
    void GameOver() {

        paintCount = 0;
        //GameOverText.SetActive(false);
        NextLevelText.SetActive(false);
        transform.GetComponent<Rigidbody>().useGravity = false;
        if (NextLevel < Levels.Count-1)
        {
            Levels[NextLevel].SetActive(false);
            NextLevel++;
            Levels[NextLevel].SetActive(true);
        }
        else {
            GameOverText.SetActive(true);
            RestartBTN.SetActive(true);
            //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        transform.GetComponent<Rigidbody>().useGravity = true;

        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


    public void OnSwipeUp() {
        up = true;
    }
    public void OnSwipeRight()
    {
        right = true;
    }
    public void OnSwipeDown()
    {
        down = true;
    }
    public void OnSwipeLeft()
    {
        left = true;
    }

    public void OnClickRestart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}

