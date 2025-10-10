using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        moveSpeed = startSpeed;
        fixedSpeed = startSpeed;

    }

    private float fixedSpeed;

    void Update()
    {
        int steps = Mathf.FloorToInt((TimeManager.Instance.GetDay - 1) / 3f);

        startSpeed = fixedSpeed - steps * 0.1f;

        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / slowdownDuration);
        moveSpeed = Mathf.Lerp(startSpeed, minSpeed, t);

       
        float leftX = Input.GetAxisRaw("Horizontal");
        float rightX = Input.GetAxisRaw("RightStickHorizontal");
        if (Mathf.Abs(leftX) < 0.2f) leftX = 0f;
        if (Mathf.Abs(rightX) < 0.2f) rightX = 0f;

        // รวมค่าจอยซ้ายและขวา
        moveInput.x = Mathf.Abs(leftX) > Mathf.Abs(rightX) ? leftX : rightX;
        moveInput.y = 0;

        // กลับด้านตัวละคร
        if (moveInput.x > 0) sr.flipX = false;
        else if (moveInput.x < 0) sr.flipX = true;

        anim.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * startSpeed, rb.velocity.y);
    }
}