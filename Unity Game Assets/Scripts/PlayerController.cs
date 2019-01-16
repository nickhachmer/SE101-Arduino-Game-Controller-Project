using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;  //To flip character
    [HideInInspector] public bool jump = false;        //To jump
    public float moveForce = 365f;                     //Moving force
    public float maxSpeed = 8f;                        //Maximum speed
    public float jumpForce = 1000f;                    //Jumping force
	public GameObject firePoint;

    private bool grounded = false;                     //To check if character is on ground
    private Animator anim;
    private Rigidbody2D rb2d;
	private bool wall = false, wallJump = false;
	private float h = 0;
	private int health = 3;
	private bool invun = false;
	private bool canJump = true;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public Text scoreText;

    // Use this for initialization
    void Awake() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
		h = Input.GetAxis("Horizontal");
		grounded = gameObject.GetComponent<CircleCollider2D> ().IsTouchingLayers (1 << LayerMask.NameToLayer("Ground"));
		wall = gameObject.GetComponent<CircleCollider2D> ().IsTouchingLayers (1 << LayerMask.NameToLayer("Wall"));

		anim.SetFloat("Speed", Mathf.Abs(h));

        if(h > 0 || h < 0)
        {
            gameOverUI.SetActive(false);
        }


		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded && canJump) {
			jump = true;
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && wall && canJump) {
			wallJump = true;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			canJump = true;
		}

		if (gameObject.transform.position.y < -15 || health == 0) {
			gameover ();
		}

		win ();

    }

    void FixedUpdate() {
		if (h < 0) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		} else if (h > 0) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		} else {
			rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
		}
		if (jump && canJump) {
			jump = false;
			canJump = false;
            anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0, jumpForce));
		} else if (wallJump && canJump) {
			wallJump = false;
			canJump = false;
			anim.SetTrigger("Jump");
			rb2d.velocity = new Vector2 (0, 0);
			rb2d.AddForce(new Vector2(0, jumpForce));
		}
    }


    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
		firePoint.transform.Rotate(0, 180, 0);
    }

	void gameover() {
		gameObject.transform.position = new Vector2 (0, -8.4f);
		rb2d.velocity = new Vector2 (0, 0);
		health = 3;
		invun = false;
        gameOverUI.SetActive(true);
        Debug.Log ("Game Over!");
	}

	void win() {
		if (gameObject.transform.position.x > 100 && gameObject.transform.position.y > 80) {
			Debug.Log ("Congradulations, You Win!");
            completeLevelUI.SetActive(true);
		}
	}

	public void Damage() {
		health--;
        scoreText.text = "Health: 3";
		Debug.Log ("Health = " + health);
        scoreText.text = "Health: " + health.ToString();

        if(health == 0)
        {
            scoreText.text = "Health: 3";
        }
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy" && !invun) {
			Damage ();
			StartCoroutine (Invunerable());
		}
	}

	IEnumerator Invunerable() {
		invun = true;
		yield return new WaitForSeconds (2);
		invun = false;
	}
		
}