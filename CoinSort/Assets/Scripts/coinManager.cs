using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

public class coinManager : MonoBehaviour
{
    private float[] yPos = { -0.05f, -0.2f, -0.35f, -0.5f, -0.65f, -0.8f, -0.95f, -1.1f, -1.25f, -1.4f};
    private float[] zPos = { 0f ,-0.01f, -0.02f, -0.03f, -0.04f, -0.05f, -0.06f, -0.07f, -0.08f, -0.09f};
    public GameObject[] coinGroup;

    private int selectedListCoin = -1;

    //create list coin prefab
    

    // map name of position with coin value
    private List<Dictionary<int, int>> listCoinPosition = new List<Dictionary<int, int>>(15);

    private int[,] valueOfCoin = new int[15,10];
    // Start is called before the first frame update
    void Start()
    {
        MergeCoin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinClicked(int index)
    {
        Debug.Log($"Selected list coin: {selectedListCoin}");

        if (selectedListCoin == -1)
        {
            SelectedCoint(index);
            selectedListCoin = index;
        }
        else if(selectedListCoin == index)
        {
            UpdateUnselectedCoinPosition(selectedListCoin);
            selectedListCoin = -1;
        }
        else
        {
            UpdateUnselectedCoinPosition(selectedListCoin);
            SelectedCoint(index);
            selectedListCoin = index;
        }
        Debug.Log($"Received event from child: {index}");
    }
    private void SelectedCoint(int listCoinIndex)
    {
        int lastType;
        int indexLastType;
        (lastType, indexLastType) = GetLastTypeAndIndexOfListCoin(listCoinIndex);
        UpdateSelectedCoinPosition(listCoinIndex,indexLastType);
    }

    private (int,int) GetLastTypeAndIndexOfListCoin(int listCoinIndex)
    {
        int lastType = 0;
        int indexLastType = 0;
        for (int i = 0; i < getNumCoinInFrame(listCoinIndex); i++)
        {
            if (lastType != valueOfCoin[listCoinIndex,i])
            {
                lastType = valueOfCoin[listCoinIndex,i];
                indexLastType = i;
            }
        }
        return (lastType, indexLastType);
    }

    private void UpdateSelectedCoinPosition(int listCoinIndex, int indexLastType) {
        for (int i = indexLastType; i< getNumCoinInFrame(listCoinIndex);i++)
        {
            coinGroup[listCoinIndex].transform.GetChild(i).transform.localPosition = new Vector3(0, yPos[i] + 0.3f, zPos[i]);
        }
    }

    private void UpdateUnselectedCoinPosition(int listCoinIndex)
    {
        for (int i = 0; i < getNumCoinInFrame(listCoinIndex); i++)
        {
            coinGroup[listCoinIndex].transform.GetChild(i).transform.localPosition = new Vector3(0, yPos[i], zPos[i]);
        }
    }

    private void MoveCoin(int coinIndexFrom, int coinIndexTo)
    {
        int lastTypeFrom;
        int indexLastTypeFrom;
        (lastTypeFrom, indexLastTypeFrom) = GetLastTypeAndIndexOfListCoin(coinIndexFrom);

        int lastTypeTo;
        int indexLastTypeTo;
        (lastTypeTo, indexLastTypeTo) = GetLastTypeAndIndexOfListCoin(coinIndexTo);
        if (lastTypeFrom == lastTypeTo)
        {
            int numCoinMove = 10 - indexLastTypeTo;
            if(numCoinMove > getNumCoinInFrame(indexLastTypeFrom) - indexLastTypeFrom) { 
                numCoinMove = getNumCoinInFrame(indexLastTypeFrom) - indexLastTypeFrom;
                // move here
                // move numCoinMove from index getNumCoinInFrame(indexLastTypeFrom)-numCoinMove to index getNumCoinInFrame(indexLastTypeTo)
                //for (int i = 0;i < numCoinMove;i++)
                //{
                //    int indexCoinMove = indexLastTypeFrom + i
                //    coinGroup[coinIndexFrom].transform.GetChild(indexCoinMove).transform.localPosition= 
                //}
            }
        }
    }

    private int getNumCoinInFrame(int frameIndex)
    {
        return coinGroup[frameIndex].transform.childCount;
    }

    private void MergeCoin()
    {
        int lastType = 0;
        int indexLastType = 0;
        for (int i = 0;i< coinGroup.Length;i++)
        {
            if (coinGroup[i].transform.childCount == 10 )
            {
                (lastType, indexLastType) = GetLastTypeAndIndexOfListCoin(i);
                if (indexLastType == 0){
                    // Merge here
                    Debug.Log($"Merge list coin index: {i}");
                }
            }
        }
    }

    private void GenerateCoin(int numCoinGen)
    {

    }
}
