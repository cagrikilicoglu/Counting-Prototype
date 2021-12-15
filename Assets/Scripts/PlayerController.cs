using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] float turnSpeed = 100;
    [SerializeField] Transform barrel;

    [SerializeField] public GameObject gunPrefab;
    [SerializeField] Rigidbody gunRb;
    [SerializeField] Vector3 firingPoint;
    [SerializeField] float gunPower = 10;
    [SerializeField] public int count;

    [SerializeField] Text counterText;
    [SerializeField] Text timerText;
    [SerializeField] float timer;
    [SerializeField] Text reloadText;

    [SerializeField] float maxRotationY = 45;
    [SerializeField] float minRotationY = 0;
    [SerializeField] float rotationBoundZ = 45;

    [SerializeField] float rotationY;
    [SerializeField] float rotationZ;

    [SerializeField] public GameObject gameOverObject;
    [SerializeField] int gunCount;
    [SerializeField] bool isFiringActive;
    [SerializeField] TrailRenderer gunTrail;


    public GameObject gameOverScreen;
    public int highScore;
    public string highScoreUser;


    void Start()
    {
        StartGame();
    }

    // set the variables of the game at the start
    void StartGame()
    {
        Time.timeScale = 1;
        barrel = gameObject.transform.GetChild(1);
        count = 0;
        timer = 90;
        gunCount = 3;
        isFiringActive = true;

        highScore = PersistenceManager.Instance.highScore;
        highScoreUser = PersistenceManager.Instance.highScoreUser;

    }


    void Update()
    {

        // rotate the barrel according to keyboard input between certain degrees
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        rotationY += turnSpeed * verticalInput * Time.deltaTime;
        rotationZ += turnSpeed * horizontalInput * Time.deltaTime;

        rotationY = Mathf.Clamp(rotationY, minRotationY, maxRotationY);
        rotationZ = Mathf.Clamp(rotationZ, -rotationBoundZ, rotationBoundZ);
        var rot = barrel.transform.localEulerAngles;
        rot.y = rotationY;
        rot.z = rotationZ;
        barrel.transform.localEulerAngles = rot;

        // set the timer 
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Round(timer);
        if (timer < 0)
        {
            timerText.text = "Time: 0";
            Time.timeScale = 0;
            GameOver();

        }


        if (isFiringActive)
        {
            StartCoroutine(FireGun());
        }

        StartCoroutine(ReloadTheCannon());
    }

    // fire the gun according to the rotation of the barrel
    private IEnumerator FireGun()
    {
        if (Input.GetKeyDown(KeyCode.Space))

        {
            gunCount -= 1;
            firingPoint = barrel.gameObject.transform.GetChild(5).transform.position;
            Vector3 gunDirection = (firingPoint - barrel.transform.position).normalized;

            GameObject gun = ObjectPool.SharedInstance.GetPooledObject();
            if (gun != null)
            {
                gun.transform.position = firingPoint;
                gun.transform.rotation = gunPrefab.transform.rotation;
                gunRb = gun.GetComponent<Rigidbody>();
                gunTrail = gun.GetComponent<TrailRenderer>();

                gunTrail.enabled = true;
                gunRb.velocity = Vector3.zero;
                gunRb.angularVelocity = Vector3.zero;

                gun.SetActive(true);

                gunRb.AddForce(gunDirection * gunPower, ForceMode.Impulse);

                yield return new WaitForSeconds(7);
                gun.SetActive(false);

            }

        }

    }

    // count the score

    public void ScoreCounter()
    {
        count++;
        counterText.text = "Score: " + count;
    }

    // to prevent spamming the fire key, reload the cannon for each three fires

    IEnumerator ReloadTheCannon()
    {
        if (gunCount == 0)
        {
            isFiringActive = false;
            reloadText.enabled = true;
            gunCount = 3;
            yield return new WaitForSeconds(3);

            reloadText.enabled = false;
            isFiringActive = true;

        }

    }

    // when the time is up show game over screen and save the highscore if it is

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        if (count > highScore)
        {
            PersistenceManager.Instance.highScore = count;
            highScoreUser = PersistenceManager.Instance.nameString;
            PersistenceManager.Instance.highScoreUser = highScoreUser;

            PersistenceManager.Instance.SaveHighScore();
        }
    }




}
