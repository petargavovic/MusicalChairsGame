using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Animator jumpAnimator;
    public Slider energySlider;
    public float energy = 1;
    public GameObject caci;

    public float timer = 3;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI startTimerText;
    public TextMeshProUGUI energyText;
    public AudioManager audioManager;
    AudioSource s;

    public float speed = 50f;

    float loseTimer = 6f;
    public float startTimer = 3f;
    public int takenCount = 0;

    public Image winScreen;
    public Image loseScreen;
    bool lost = false;
    bool musicStopped = false;
    public GameObject music;
    public NPC npcs;

    Vector2 movement;

    void Start()
    {
        jumpAnimator = GetComponentInChildren<Animator>();
        s = audioManager.gameObject.GetComponents<AudioSource>()[1];
//        audioManager.Play("running");
        npcs = GameObject.Find("NPCSpawner").GetComponent<NPC>();
    }

    void Update()
    {
        startTimerText.text = Mathf.RoundToInt(startTimer).ToString();
        if (startTimer > 0)
            startTimer -= Time.deltaTime;
        else
        {
            if (!musicStopped)
            {
                music.SetActive(true);
                musicStopped = true;
            }
            startTimerText.GetComponent<Animator>().Play("-startTimer");
            startTimerText.text = "Ко не скаче, тај је ћаци!";
            if (timer != 0 && !GameObject.Find("NPCSpawner").GetComponent<NPC>().won && npcs.isMusic)
                timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");

            if (timer < 0)
            {
                timer = 0;
                caci.SetActive(true);
                s.enabled = false;
                animator.Play("Player_Idle");
                audioManager.Play("game_over");
            }
            else if (timer != 0)
            {


                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    float multiplier;
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                        multiplier = 0.7f;
                    else
                        multiplier = 1;
                    transform.position += Vector3.up * Time.deltaTime * multiplier * speed;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    transform.position += Vector3.left * Time.deltaTime * speed;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    float multiplier;
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                        multiplier = 0.7f;
                    else
                        multiplier = 1;
                    transform.position += Vector3.down * Time.deltaTime * multiplier * speed;
                }

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
                    s.enabled = true;
                else
                    s.enabled = false;

                energySlider.value = energy;
                energyText.text = ((int)MathF.Round(energy * 100)).ToString();
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (energy >= 0.1f)
                    {
                        if (!jumpAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
                        {
                            jumpAnimator.Play("Player_Jump");
                            animator.transform.GetChild(0).GetComponent<Animator>().Play("circle_jump");
                            energy -= 0.099999f;
                            timer = 3;
                            audioManager.Play("jump");
                        }
                    }
                    else
                        energySlider.GetComponent<Animator>().Play("slider_empty");
                }
            }
            else
            {
                s.enabled = false;
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);
                animator.SetFloat("Speed", 0);
                loseTimer -= Time.deltaTime;
                if (loseTimer < 3)
                {
                    if (!lost)
                    {
                        loseScreen.gameObject.SetActive(true);
                        lost = true;
                    }
                    if (loseTimer < 0)
                    {
                        GameObject.Find("SpawnerSettings").GetComponent<SpawnerSettings>().npcNumber = 5;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }
    }

    private void GetKeyDown(KeyCode mouse0)
    {
        throw new NotImplementedException();
    }
}
