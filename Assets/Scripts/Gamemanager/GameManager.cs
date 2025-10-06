using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameObject Player;
    [SerializeField] public PlayerController PlayerController;

    public GameObject GetPlayer => Player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerController = Player.GetComponent<PlayerController>();
        }
    }
}
