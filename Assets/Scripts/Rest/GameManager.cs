using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    private int playerMoney = 0;

    private void Awake()
    {
        // Making sure there is only one GameManager Instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    // Function will be called to add money to the player
    public void AddMoney(int amount)
    {
        playerMoney += amount;
    }

    // Function will be used to spend / remove Money after it checks if there is enough and returns a boolean to
    public bool SpendMoney(int amount)
    {
        if (playerMoney >= amount)
        {
            playerMoney -= amount;
            return true;
        }
        return false;
    }
}
