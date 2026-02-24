using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float startSpeed = 2f;
    public float minSpeed = 0.3f;
    float slowdownDuration = 30f;

    private float moveSpeed;
    private float elapsedTime = 0f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Vector2 moveInput;

    private InputAction moveAction;

    private float fixedSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        moveAction = InputSystem.actions.FindAction("Move");

        moveSpeed = startSpeed;
        fixedSpeed = startSpeed;
    }

    void Update()
    {
        // ------------------------
        // ระบบความเร็วช้าลงตามวัน
        // ------------------------
        int steps = Mathf.FloorToInt((TimeManager.Instance.GetDay - 1) / 3f);
        startSpeed = fixedSpeed - steps * 0.1f;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / slowdownDuration);
        moveSpeed = Mathf.Lerp(startSpeed, minSpeed, t);

        // ------------------------
        // รับค่าอินพุต (คีย์บอร์ด / จอยซ้าย)
        // ------------------------
        moveInput = moveAction.ReadValue<Vector2>();
        moveInput = Vector2.ClampMagnitude(moveInput, 1f);
        

        // ------------------------
        // กลับด้านตัวละคร (ใช้แค่แกน X)
        // ------------------------
        if (moveInput.x > 0)
            sr.flipX = false;
        else if (moveInput.x < 0)
            sr.flipX = true;

        // ------------------------
        // อนิเมชัน (ใช้คลิปเดียว)
        // ------------------------
        anim.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }
}
