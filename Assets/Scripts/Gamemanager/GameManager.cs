using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameObject Player;
    [SerializeField] public PlayerController PlayerController;
    [SerializeField] private SceneButtonTrigger SceneButtonTrigger;
    
    public GameObject GetPlayer => Player;

    private InputAction pressR1;

    private void Awake()
    {
        pressR1 = InputSystem.actions.FindAction("Interact/R1");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerController = Player.GetComponent<PlayerController>();
        }
    }
}
