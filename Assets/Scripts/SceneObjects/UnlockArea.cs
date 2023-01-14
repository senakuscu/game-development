using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnlockArea : MonoBehaviour
{
    public UnlockData unlockableData;
    public TextMeshPro NameText;
    public TextMeshPro PriceText;
    public List<GameObject> ObjectsToUnlock = new List<GameObject>();
    public GameObject car, train, stickMan, yacht, carUnlock, trainUnlock,yachtUnlock;
    public Stash stash;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnEnable()
    {
        CheckUnlocked();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        ObjectsToUnlock.ForEach((x) => x.SetActive(false));
        //ObjectToUnlock.SetActive(false);
        NameText.text = "UNLOCK " + unlockableData.unlockableName.ToUpper();
        PriceText.text = unlockableData.RemainingPrice.ToString();
        
    }

    public void Pay(Stashable stashable)
    {
        if (unlockableData.RemainingPrice <= 0)
            return;

        unlockableData.CollectedPrice++;
        stashable.PayStashable(transform, PaymentCompleted);
         
    }

    private void PaymentCompleted()
    {
        PriceText.text = unlockableData.RemainingPrice.ToString();

        CheckUnlocked();
    }

    private void CheckUnlocked()
    {
        if (unlockableData.RemainingPrice <= 0)
        {
            ObjectsToUnlock.ForEach((x) =>
            {
                x.transform.parent = null;
                x.SetActive(true);
            });
            gameObject.SetActive(false);

            if (stickMan.activeSelf == true && carUnlock.activeSelf == false)
            {
                car.SetActive(true);
                stash.maxCollectableCount = 10;
                stickMan.SetActive(false);
            }
            else if (car.activeSelf == true && trainUnlock.activeSelf == false)
            {
                train.SetActive(true);
                stash.maxCollectableCount = 20;
                car.SetActive(false);
            }
            else if (train.activeSelf == true && yachtUnlock.activeSelf == false)
            {
                yacht.SetActive(true);
                stash.maxCollectableCount = 30;
                train.SetActive(false);
            }

        }
    }
}
