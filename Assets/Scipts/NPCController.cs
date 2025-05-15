using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{ 
    public GameObject[] AllChairs;
    public GameObject NearestChair;
    float distance;
    float nearestDistance = 50;
    public Animator animator;
    public Animator jumpAnimator;

    bool startMoving;
    public bool inChair;
    private PlayerController playerController;

    Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        jumpAnimator = GetComponentInChildren<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.startTimer < 0)
        {
            if (startMoving)
            {
                var step = 2.8f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, NearestChair.transform.position, step);
                if (!inChair)
                {
                    if (NearestChair.GetComponent<Stolica>().taken)
                    {
                        for (int i = 0; i < AllChairs.Length; i++)
                        {
                            if (!AllChairs[i].GetComponent<Stolica>().taken)
                            {
                                NearestChair = AllChairs[i];
                            }
                        }
                    }
                }
            }
            Vector3 currentPos = transform.position;

            Vector3 lastPos = previousPosition;

            Vector3 direction = (currentPos - lastPos).normalized;

            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);

            previousPosition = currentPos;
            animator.SetFloat("Speed", (currentPos - lastPos).magnitude / Time.deltaTime);
        }
    }

    public void GoToChair()
    {
        AllChairs = GameObject.FindGameObjectsWithTag("Chair");


        for (int i = 0; i < AllChairs.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, AllChairs[i].transform.position);

            if (distance < nearestDistance)
            {
                NearestChair = AllChairs[i];
                nearestDistance = distance;
            }
        }
        StartCoroutine("AnimStart");
    }
    IEnumerator AnimStart()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        startMoving = true;
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>().enabled = false;
    }
}
