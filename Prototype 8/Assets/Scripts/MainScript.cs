using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainScript : MonoBehaviour
{
    private int[] order = new int[4]; // the customer's order
    private int[] sandwich = new int[4]; // what the player is making
    /*
    index 0 = tomato
    index 1 = cheese
    index 2 = lettuce
    index 3 = meat
    */
    public GameObject[] ingredients;
    public GameObject orderDisp;
    public GameObject[] sandwichOrd;
    public GameObject losePanel;
    public Button orderUp;
    public TMP_Text ordFailed;
    int ordersPassed = 0;
    int angry = 0;
    float dispTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Randomize();
        orderUp.onClick.AddListener(CheckOrder);
        losePanel.SetActive(false);
        ordFailed.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (ordersPassed >= 3)
        {
            LosingAudioManager.instance.PlayLoseSound(); // Play the lose sound
            losePanel.SetActive(true);
            ordFailed.gameObject.SetActive(true);
            ordFailed.text = "Orders Passed: " + angry.ToString();
        }
        //checking what ingredients player wants to add to the sandwich
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {

                if (hit.collider.CompareTag("Tomato"))
                {
                    sandwich[0] = 1;
                    sandwichOrd[0].SetActive(true);
                }
                else if (hit.collider.CompareTag("Cheese"))
                {
                    sandwich[1] = 1;
                    sandwichOrd[1].SetActive(true);
                }
                else if (hit.collider.CompareTag("Lettuce"))
                {
                    sandwich[2] = 1;
                    sandwichOrd[2].SetActive(true);
                }
                else if (hit.collider.CompareTag("Meat"))
                {
                    sandwich[3] = 1;
                    sandwichOrd[3].SetActive(true);
                }
            }
        }
    }

    // randomizing the customer order
    void Randomize() 
    {
        Reset();
        for (int i = 0; i < 4; i++)
        {
            order[i] = Random.Range(0, 2);
        }
        StartCoroutine(DisplayOrder());
    }

    void Reset()
    {
        for (int i = 0; i < 4; i++)
        {
            order[i] = 0;
            sandwich[i] = 0;
            sandwichOrd[i].SetActive(false);
        }
    }

    IEnumerator DisplayOrder()
    {
        orderDisp.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            if (order[i] == 1)
            {
                ingredients[i].SetActive(true);
            }
            else
            {
                ingredients[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(dispTime);
        dispTime = Mathf.Max(dispTime * 0.95f, 0.2f); // Ensure minimum 0.2s
        orderDisp.SetActive(false);

    }

    void CheckOrder()
    {
        bool passedOrder = false;
        for (int i = 0; i < 4; i++)
        {
            if (order[i] == sandwich[i])
            {
                passedOrder = true;
                break;
            }
        }
        if (passedOrder)
        {
            ordersPassed++;
        }
        else angry++;
        Debug.Log("orders passed: " + ordersPassed);
        Debug.Log("orders failed: " + angry);
        Randomize();
    }
}
