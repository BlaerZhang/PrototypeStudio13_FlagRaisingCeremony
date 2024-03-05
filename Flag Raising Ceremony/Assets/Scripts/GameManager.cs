using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject pole;
    public float poleLength;
    public Vector2 poleLengthRange;
    public GameObject topCam;
    public float timer;
    public MouseControl player;
    public float notSmoothTimer;
    public float loseThreshold;

    [Header("UI Text")]
    public TextMeshProUGUI instruction;
    public TextMeshProUGUI result;
    public TextMeshProUGUI restart;

    [Header("Audio")]
    public AudioSource anthem;
    public AudioSource ambience;

    public enum GameState
    {
        isPreparing,
        isInGame,
        isGameEnd
    }

    public static GameState gameState;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        gameState = GameState.isPreparing;
        player.gameObject.SetActive(false);
        restart.enabled = false;

        poleLength = Random.Range(poleLengthRange.x, poleLengthRange.y);
        pole.transform.position = new Vector3(pole.transform.position.x, poleLength, 0);
        pole.GetComponent<SpriteRenderer>().size = new Vector2(0.25f, poleLength + 10f);

    }

    void Update()
    {
        PressRReload();

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Screen.fullScreen = true;
        }

        switch (gameState)
        {
            case GameState.isPreparing:
                timer = 0;
                notSmoothTimer = 0;
                topCam.SetActive(false);
                if (Input.GetAxis("Mouse Y") < -0.5f) player.gameObject.SetActive(true);
                break;
            case GameState.isInGame:
                if(!anthem.isPlaying) anthem.Play();
                if (poleLength - player.transform.position.y < 15f) topCam.SetActive(true);
                timer += Time.deltaTime;

                if(timer >= 2f) NotSmoothDetect();
                
                if (timer >= 47.1f)
                {
                    gameState = GameState.isGameEnd;
                    result.text = "HALF-STAFF";
                }
                
                break;
            case GameState.isGameEnd:
                if (player.isTopped)
                {
                    if (timer >= 43.3f && timer <= 44.1f) result.text = "PERFECT";
                    if ((timer >= 42.3f && timer < 43.3f) || (timer > 44.1f && timer <= 45.1f))
                        result.text = "ALL RIGHT";
                    if (timer > 45.1f || timer < 42.3f) result.text = "NOT IDEAL";
                    if (timer <= 35.3f) result.text = "TUG OF WAR";
                }

                instruction.text = "";
                restart.enabled = true;
                if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    void NotSmoothDetect()
    {
        instruction.text = "";

        if (notSmoothTimer > 0) notSmoothTimer -= Time.deltaTime * 0.25f;
        
        if (player.velocity < 0.3f && player.velocity >= 0)
        {
            notSmoothTimer += Time.deltaTime * 1.5f;
            instruction.text = "DON'T STOP!";
        }

        if (player.velocity < 0)
        {
            notSmoothTimer += Time.deltaTime * 2f;
            instruction.text = "WRONG WAY!";
        }

        if (player.acceleration > player.notSmoothThreshold)
        {
            notSmoothTimer += Time.deltaTime + (player.acceleration - player.notSmoothThreshold) * 0.01f;
            instruction.text = "NOT SMOOTH!";
        }

        if (notSmoothTimer > loseThreshold)
        {
            gameState = GameState.isGameEnd;
            result.text = "COMEDIAN";
        }
    }

    void PressRReload()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
