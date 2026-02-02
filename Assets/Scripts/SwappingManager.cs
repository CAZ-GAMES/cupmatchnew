using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SwappingManager : MonoBehaviour
{
    [SerializeField]
    SceneSetup ss;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager.OnGameStateChange += FireOff;
        GameManager.OnGameStateChange += Check;
    }



    void Start()
    {
        foreach(GameObject x in ss.inside)
        {
            print(x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChange -= FireOff;
        GameManager.OnGameStateChange -= Check;
    }

    void FireOff(GameState state)
    {
        if(state == GameState.Swap)
        {
            print("Called");
            Swap();
            GameManager.Instance.UpdateGameState(GameState.Trying);
        }
        
    }

    public void Swap()
    {
        // use this as midpoint to create pivot point to revolve objects
        var objDistance = (GameManager.Instance.clickedOn[0].transform.position + GameManager.Instance.clickedOn[1].transform.position) / 2;
        StartCoroutine(RotateTimed(0.5f, GameManager.Instance.clickedOn[0], objDistance));
        StartCoroutine(RotateTimed(.5f, GameManager.Instance.clickedOn[1], objDistance));
        //SwapListPos();
        GameManager.Instance.clickedOn.Clear();
        GameManager.Instance.moves++;
        //GameManager.Instance.UIManager.MoveCountUpdate();
    }

    IEnumerator RotateTimed(float duration, GameObject clicked, Vector3 objDistance)
    {
        float timer = 0f;
        float startAngle = 0f;
        float currentAngle;
        while (timer < duration)
        {
            // clicked.transform.RotateAround(objDistance, Vector3.up, 1440 * Time.deltaTime);
            timer += Time.deltaTime;

            // Lerp rotation progress
            currentAngle = Mathf.Lerp(startAngle, 180f, timer / duration);

            // Rotate difference since last frame
            clicked.transform.RotateAround(objDistance, Vector3.up, currentAngle - startAngle);

            startAngle = currentAngle;

            yield return null;
        }
        // set cups back down
        clicked.transform.position = new Vector3(clicked.transform.position.x, clicked.transform.position.y - 0.25f);
    }

    private void Check(GameState state)
    {
        if(state == GameState.Check)
        {
            print("Checking");
            GameManager.Instance.UpdateGameState(GameState.Trying);
        }
    }
}
