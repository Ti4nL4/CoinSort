using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public GameObject[] listFrame;
    public GameObject[,] childObjects;
    private int nextFrame = 5;

    private void Awake()
    {
        int numberOfFrames = listFrame.Length;
        childObjects = new GameObject[numberOfFrames, 4];

        for (int i = 0; i < numberOfFrames; i++)
        {
            Transform frameTransform = listFrame[i].transform;

            for (int j = 0; j < 4; j++)
            {
                childObjects[i, j] = frameTransform.GetChild(j).gameObject;
            }
            listFrame[i].GetComponent<frameInteract>().OnChildEvent += HandleChildEvent;
        }
    }

    private void Start()
    {
        InitState();
    }

    private void Update()
    {
 
    }

    private void InitState()
    {
        for (int i = 0;i <5;i++)
        {
            OpenFrame(listFrame[i]);
        }

        NextFrameObject(listFrame[5]);

        for (int i = 6; i < listFrame.Length; i++)
        {
            CloseFrame(listFrame[i]);
        }
    }

    private void OpenFrame(GameObject gameObject) {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
    }

    private void CloseFrame(GameObject gameObject)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }
    private void NextFrameObject(GameObject gameObject)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
    }

    void HandleChildEvent(int index)
    {
        this.gameObject.GetComponent<coinManager>().CoinClicked(index);
        if (index == nextFrame)
        {
            OpenFrame(listFrame[nextFrame]);
            Debug.Log(nextFrame);
            nextFrame++;
            if (nextFrame < 15)
            {
                NextFrameObject(listFrame[nextFrame]);
            }
        }
    }

}
