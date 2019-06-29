using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles car movement and users input in gameplay.
/// </summary>
public class CarController : MonoBehaviour, IHealth, ILevel, IFuel, ICoin
{
    public static CarController instance;
    public static Sprite skin;
    public LayerMask layerMask;
    public float limit = 2;

    [Header("Car speed settings")]
    [Tooltip("Acceleration curve")]
    public AnimationCurve curve;
    [Tooltip("Car real max speed in unity")]
    public float realSpeedCap = 5;
    [Tooltip("Car start speed in unity")]
    public float startSpeed = 2.5f;
    [Tooltip("Cars visual max speed in unity")]
    public float maxSpeed = 5;
    [Tooltip("Cars minimum acceleration in unity")]
    public float minAcceleration = 1;
    [Tooltip("Cars maximum acceleration in unity")]
    public float maxAcceleration = 10;
    [Tooltip("Cars speed difference")]
    public float speedDiff = 20f;

    public Text speedText;
    public Image speedometerImage;
    public Image progress;
    public Text currentLevel;
    public Text nextLevel;

    public Animator destroyEffect;
    public GameObject trailHolder;

    Camera cam;
    SpriteRenderer spriteRenderer;
    BoxCollider2D carCollider;
    Rigidbody2D carRigidbody;
    EndLevel endLevel;

    float distance;
    float distanceFromCamera;
    bool cameraFollowing;
    bool isBreaking;
    bool isBlocked = true;
    float startDistance;
    bool levelCompleted;
    public float health;

    public float accelerationLevel
    {
        get
        {
            return GameManager.instance.data.accelerationLevel;
        }
    }

    public float speedLevel
    {
        get
        {
            return 5 * GameManager.instance.data.speedLevel;
        }
    }

    /// <summary>
    /// current carr accelereation based on cars speed
    /// </summary>
    public float acceleration
    {
        get
        {
            float speed = ((maxSpeed + speedLevel) / speedDiff);
            return Mathf.Lerp(minAcceleration + accelerationLevel, maxAcceleration + accelerationLevel, curve.Evaluate(velocity.y / speed));
        }
    }

    /// <summary>
    /// set if camera is following the car or not
    /// </summary>
    public bool isCameraFollwing
    {
        get
        {
            return cameraFollowing;
        }
        set
        {
            cameraFollowing = value;

            if (value)
            {
                distanceFromCamera = position.y - cam.transform.parent.position.y;
            }
        }
    }

    /// <summary>
    /// mouse position in world position
    /// </summary>
    public Vector2 inputPosition
    {
        get
        {
            return cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    /// <summary>
    /// car position
    /// </summary>
    public Vector2 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    /// <summary>
    /// car size
    /// </summary>
    public Vector2 size
    {
        get
        {
            return carCollider.size;
        }
    }
    /// <summary>
    /// car velocity
    /// </summary>
    public Vector2 velocity
    {
        get
        {
            return carRigidbody.velocity;
        }
        set
        {
            carRigidbody.velocity = value;
        }
    }

    /// <summary>
    /// current distance from the car to the finish line
    /// </summary>
    public float distanceFromEnd
    {
        get
        {
            return Vector2.Distance(transform.position, endLevel.transform.position);
        }
    }


    void Start()
    {
        cam = Camera.main;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        carCollider = GetComponentInChildren<BoxCollider2D>();
        isCameraFollwing = true;
        carRigidbody = GetComponent<Rigidbody2D>();
        endLevel = FindObjectOfType<EndLevel>();
        startDistance = distanceFromEnd;


        instance = this;
        levelCompleted = false;
        trailHolder.SetActive(false);

        GameScreen gs = MenuController.instance.menus[1] as GameScreen;
        speedText = gs.speedText;
        speedometerImage = gs.speedometerImage;
        progress = gs.progress;
        currentLevel = gs.currentLevel;
        nextLevel = gs.nextLevel;

        currentLevel.text = (GameManager.instance.data.level + 1).ToString();
        nextLevel.text = (GameManager.instance.data.level + 2).ToString();

        if (skin != null)
        {
            SetSkin(skin);
        }
    }

    bool topSpead;
    void FixedUpdate()
    {
        float speed = ((maxSpeed + speedLevel) / speedDiff);
        if (!isBlocked && velocity.y < (speed * multiplier))
        {
            carRigidbody.AddForce(Vector2.up * (acceleration / speedDiff));
            topSpead = false;
            if (velocity.y > (speed * multiplier))
            {
                velocity = Vector2.up * (speed * multiplier);
            }
        }
        else if (!isBlocked && !topSpead)
        {
            topSpead = true;
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
        }
    }

    /// <summary>
    /// Change the skin of the car
    /// </summary>
    /// <param name="sprite">image that represents the car</param>
    public void SetSkin(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        skin = sprite;
    }

    /// <summary>
    /// Handle input and updates gameplay UI
    /// </summary>
    void Update()
    {
        if (levelCompleted)
        {
            return;
        }
        speedText.text = (Mathf.Max(0, Mathf.RoundToInt(velocity.y * speedDiff + health))).ToString();
        progress.fillAmount = 1 - distanceFromEnd / startDistance;
        float speed = ((maxSpeed + speedLevel) / speedDiff);
        speedometerImage.transform.eulerAngles = Vector3.forward * Mathf.Lerp(125, -125, Mathf.Max(0, velocity.y / speed));

        if (!isBreaking && !isBlocked)
        {
            HandleInput();
            CameraFollow();
        }
    }

    /// <summary>
    /// Shows car with a fade it and starts it's engine
    /// </summary>
    public void ShowAndStartCar()
    {
        StartCoroutine(ShowCar(true, 0.1f));
        StartCar();
    }

    /// <summary>
    /// Car starts moving
    /// </summary>
    public void StartCar()
    {
        isBlocked = false;
        trailHolder.SetActive(true);
        health = GameManager.instance.data.health;
        velocity = Vector2.up * GameManager.instance.data.speed;
        if(velocity.y < (startSpeed / speedDiff))
        {
            velocity = Vector2.up * (startSpeed / speedDiff);
        }
        GameScreen gs = MenuController.instance.menus[1] as GameScreen;
        gs.magnet.interactable = GameManager.instance.data.magnet > 0;
        gs.shield.interactable = GameManager.instance.data.shield > 0;
        gs.nitrous.interactable = GameManager.instance.data.nitrous > 0;

    }

    /// <summary>
    /// Handles camera following the car
    /// </summary>
    void CameraFollow()
    {
        if (cameraFollowing)
        {
            Vector3 newPosition = cam.transform.parent.position;
            newPosition.y = position.y - distanceFromCamera;
            cam.transform.parent.position = newPosition;
        }
    }

    /// <summary>
    /// Handles input from the user
    /// </summary>
    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            distance = position.x - inputPosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            if (isBlocked || Settings.isPaused)
            {
                return;
            }
            Vector2 newPosition = position;
            newPosition.x = distance + inputPosition.x;
            Vector2 castSize = size;
            castSize.x *= 1.1f;

            if (newPosition.x > position.x)
            {
                RaycastHit2D hit = Physics2D.BoxCast(position, castSize, 0, Vector2.right, newPosition.x - position.x, layerMask);

                if (hit.collider != null)
                {
                    newPosition.x = hit.transform.position.x - 0.68f;
                    position = newPosition;
                    distance = position.x - inputPosition.x;
                    return;
                }
            }
            else if (newPosition.x < position.x)
            {
                RaycastHit2D hit = Physics2D.BoxCast(position, castSize, 0, Vector2.left, position.x - newPosition.x, layerMask);

                if (hit.collider != null)
                {
                    newPosition.x = hit.transform.position.x + 0.68f;
                    position = newPosition;
                    distance = position.x - inputPosition.x;
                    return;
                }
            }

            if (newPosition.x > limit || newPosition.x < -limit)
            {
                distance = position.x - inputPosition.x;
            }

            newPosition.x = Mathf.Clamp(newPosition.x, -limit, limit);
            position = newPosition;
        }
    }

    /// <summary>
    /// Methond called when a car hits a block
    /// </summary>
    /// <param name="value">amount of health or speed the car will lose</param>
    public void OnBlockHit(int value)
    {
        if (isShielded)
            return;

        float diff = 0;
        if (health > 0)
        {
            health -= value;
            if (health < 0)
            {
                diff = -1 * health;
                health = 0;
                value = Mathf.RoundToInt(diff);
            }
            else
            {
                value = 0;
            }
        }
        GameManager.instance.data.health = health;
        GameManager.instance.data.speed = velocity.y;
        carRigidbody.velocity = velocity - Vector2.up * (value / speedDiff);

        if (velocity.y < 0)
        {
            velocity = Vector2.zero;
            isBlocked = true;
            StartCoroutine(ShowCar(false, 0.5f));
            destroyEffect.Play("CarExplode");
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
            SoundManager.instance.Play("GameOver");
            GameOverScreen.OpenAndUpdate();
        }
        else
        {
            //StartCoroutine(BlockInput());
            SoundManager.instance.Play("Obstacle");
            CameraShake.shakeDuration = 1;
        }
    }

    /// <summary>
    /// Method called when a level was succesfully completed
    /// </summary>
    public void OnLevelCompleted()
    {
        isBreaking = true;
        levelCompleted = true;
        progress.fillAmount = 1;
        GameManager.instance.data.health = health;
        GameManager.instance.data.speed = velocity.y;
        GameManager.instance.data.level++;
        iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
        StartCoroutine(LevelDelay());

    }

    /// <summary>
    /// When level is completed small delay for some animations
    /// </summary>
    /// <returns></returns>
    IEnumerator LevelDelay()
    {
        yield return new WaitForSeconds(2);
        CompletedLevelScreen.OpenAndUpdate(speedText.text);
    }

    /// <summary>
    /// Method called when the car collides with fuel
    /// </summary>
    /// <param name="value">value of how much the car will speed up and gain health</param>
    public void OnFuelHit(int value)
    {
        Vector2 newVel = velocity;

        if (health > 0)
        {
            health += value;
        }
        else
        {
            float speed = realSpeedCap + speedLevel;
            if (newVel.y + value / speedDiff > speed / speedDiff)
            {
                float diff = (newVel.y + value / speedDiff) - speed / speedDiff;
                health = diff;
                newVel.y = speed / speedDiff;
            }
            else
            {
                newVel.y += value / speedDiff;
            }
        }

        velocity = newVel;
    }

    /// <summary>
    /// Method called when the car collides with coins
    /// </summary>
    public void OnCoinHit()
    {
        GameManager.instance.data.coins++;
    }

    /// <summary>
    /// Called when attractions boost is activated
    /// </summary>
    public void ActivateAtraction()
    {
        SoundManager.instance.Play("PowerUp");
        StartCoroutine(Atraction());
    }

    public GameObject startEfect;
    public GameObject magent;
    public GameObject nitrous;
    public GameObject shield;

    /// <summary>
    /// Called while the attraction boost is activated
    /// </summary>
    IEnumerator Atraction()
    {
        startEfect.SetActive(true);
        startEfect.GetComponent<Animator>().Play("Effect");
        coinCollector.SetActive(true);
        yield return new WaitForSeconds(0.18f);
        startEfect.SetActive(false);

        magent.SetActive(true);

        yield return new WaitForSeconds(10);
        magent.SetActive(false);
        coinCollector.SetActive(false);
    }

    /// <summary>
    /// Called when nitrous boost is activated
    /// </summary>
    public void ActivateNitrous()
    {
        SoundManager.instance.Play("PowerUp");
        StartCoroutine(Nitrous());
    }

    /// <summary>
    /// Called while the nitrous boost is activated
    /// </summary>
    IEnumerator Nitrous()
    {
        float startAcceleration = maxAcceleration;
        multiplier = 1.5f;
        maxAcceleration *= 2;
        startEfect.SetActive(true);
        startEfect.GetComponent<Animator>().Play("Effect");
        yield return new WaitForSeconds(0.18f);
        nitrous.SetActive(true);
        yield return new WaitForSeconds(10);
        nitrous.SetActive(false);
        maxAcceleration = startAcceleration;
        multiplier = 1;
    }

    /// <summary>
    /// Called when shield boost is activated
    /// </summary>
    public void ActivateShield()
    {
        SoundManager.instance.Play("PowerUp");
        StartCoroutine(Shield());
    }

    float multiplier = 1;
    public bool isShielded;
    public GameObject coinCollector;

    /// <summary>
    /// Called while the shield boost is activated
    /// </summary>
    IEnumerator Shield()
    {
        isShielded = true;
        startEfect.SetActive(true);
        startEfect.GetComponent<Animator>().Play("Effect");
        yield return new WaitForSeconds(0.18f);
        shield.SetActive(true);
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        isShielded = false;
    }

    /// <summary>
    /// Fade in or out animation of the car
    /// </summary>
    /// <param name="show">Car will fade in if true, else it will fade out</param>
    /// <param name="speed">length of the animation in seconds</param>
    /// <returns></returns>
    IEnumerator ShowCar(bool show, float speed)
    {
        float time = 0;
        Color startColor = spriteRenderer.color;
        Color targetColor = spriteRenderer.color;
        targetColor.a = show ? 1 : 0;
        trailHolder.SetActive(show);
        while (time < 1)
        {
            time += Time.unscaledDeltaTime / speed;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, curve.Evaluate(time));
            yield return null;
        }
    }
}
