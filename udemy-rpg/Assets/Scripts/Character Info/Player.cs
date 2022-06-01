using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] int soundForMenu       = 5;
    [SerializeField] float moveSpeed        = 5f;
    [SerializeField] float minSpeed         = 0.1f;
    [Tooltip("Read-Only"), SerializeField] string prevLocation    = "";

    Animator anim;
    Rigidbody2D rb2d;
    CircleCollider2D cc2d;

    bool canMenu, isInteracting;

    private void Awake()
    {
        int numPlayer = FindObjectsOfType<Player>().Length;
        if (numPlayer > 1) 
        { 
            Destroy(gameObject); 
        }
        else 
        { 
            DontDestroyOnLoad(gameObject); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        isInteracting = false;
        canMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMenu)
        {
            Menuing();
        }

        if (!isInteracting)
        {
            Interacting();
            Moving();
        }

        else
        {
            StopMoving();
        }
    }

    private void Menuing()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            FindObjectOfType<UIManager>().ActivateGameMenu();

            // TESTING - MENU SFX
            FindObjectOfType<AudioManager>().PlaySFX(soundForMenu);
        }
    }

    private void Interacting()
    {
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            if (cc2d.IsTouchingLayers(LayerMask.GetMask("NPC", "Interactable")) &&
                FindObjectOfType<DialogueManager>().gameObject.activeInHierarchy)
            {
                FindObjectOfType<DialogueManager>().DialogueStart();
            }

            else if (cc2d.IsTouchingLayers(LayerMask.GetMask("Shop")) &&
                FindObjectOfType<ShopMenu>().gameObject.activeInHierarchy)
            {
                FindObjectOfType<ShopMenu>().OpenShopMenu();
            }
        }
    }

    private void Moving()
    {
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");

        Vector2 newVelocity = new Vector2(x, y) * moveSpeed;
        rb2d.velocity = newVelocity;

        anim.SetFloat("IsMovingX", rb2d.velocity.x);
        anim.SetFloat("IsMovingY", rb2d.velocity.y);

        if (Mathf.Abs(x) >= minSpeed 
            || Mathf.Abs(y) >= minSpeed)
        {
            anim.SetFloat("LastMoveX", x);
            anim.SetFloat("LastMoveY", y);
        }

        FlipSpriteHorizontally(x);
    }
    
    private void FlipSpriteHorizontally(float x)
    {
        bool hasHorizontalSpeed = Mathf.Abs(x) >= minSpeed;

        if (hasHorizontalSpeed) 
        { 
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f); 
        }
    }

    private void StopMoving() 
    { 
        rb2d.velocity = Vector2.zero;
    }
    
    public string GetPrevLocation() 
    {
        return prevLocation;
    }
    
    public void SetPrevLocation(string pl) 
    {
        prevLocation = pl; 
    }

    public void SetCanMenu(bool b)
    {
        canMenu = b;
    }

    public void SetIsInteracting(bool b) 
    { 
        isInteracting = b; 
    }

    public bool GetIsInteracting()
    {
        return isInteracting;
    }
}
