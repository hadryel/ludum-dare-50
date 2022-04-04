using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caravan : MonoBehaviour
{
    public ResourceHandler ResourceHandler;
    public QuestManager QuestManager;

    private Vector2 _MovementDirection = Vector2.right;
    private Vector2 _InputDirection = Vector2.right;
    public List<GameObject> CaravanElements = new List<GameObject>();

    public float FoodCapacity = 100;
    public float FoodAmount = 100;
    public float FoodDrainRate = 1;

    public int Days = 0;
    public int Steps = 0;

    public Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();

    public GameOverPanel GameOverPanel;

    void Start()
    {
        foreach (var type in System.Enum.GetValues(typeof(ResourceType)))
        {
            Resources.Add((ResourceType)type, 0);
        }

        Time.fixedDeltaTime = 0.1f;
        CaravanElements.Add(gameObject);
    }

    void Update()
    {
        GetInput();
    }

    public void FixedUpdate()
    {
        UpdateCaravanPosition();
        UpdateFoodAmount();
        Steps++;
    }

    void UpdateFoodAmount()
    {
        FoodAmount -= FoodDrainRate;

        if (FoodAmount <= 0)
        {
            Die();
        }
    }

    void UpdateCaravanPosition()
    {
        for (int i = CaravanElements.Count - 1; i > 0; i--)
        {
            CaravanElements[i].transform.position = CaravanElements[i - 1].transform.position;
        }

        if (_InputDirection + _MovementDirection != Vector2.zero)
            _MovementDirection = _InputDirection;

        transform.position = new Vector3(Mathf.Round(transform.position.x) + _MovementDirection.x, Mathf.Round(transform.position.y) + _MovementDirection.y, 0);

        //if (_MovementDirection.x > 0)
        //    transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        //else if (_MovementDirection.x < 0)
        //    transform.GetChild(0).localScale = new Vector3(-1, 1, 1);

    }

    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _InputDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _InputDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _InputDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _InputDirection = Vector2.left;
        }
    }

    public void AddResource(Resource resource, int amount)
    {
        Resources[resource.Type] += amount;
        FoodDrainRate += resource.FoodCostPerStep * amount;
    }

    public void Die()
    {
        //SceneManager.LoadScene("SampleScene");
        GameOverPanel.GameOver();

        enabled = false;
    }

    public void Reset()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Resource")
        {
            ResourceHandler.Collect(collision.GetComponent<Resource>());
            QuestManager.UpdateActiveQuests();
        }
        else if (collision.tag == "Recipe")
        {
            collision.GetComponent<Recipe>().Build(this);
            QuestManager.UpdateActiveQuests();
        }
        else if (collision.tag == "Quest")
        {
            QuestManager.CompleteQuest(collision.GetComponent<QuestDelivery>().Quest);
        }
        else if (collision.tag == "Player" || collision.tag == "Building")
        {
            Die();
        }
    }
}
