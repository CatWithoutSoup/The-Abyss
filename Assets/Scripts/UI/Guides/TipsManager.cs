using System;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    public static Action<string> displayTipEvent;
    public static Action disableTipEvent;
    [SerializeField] private TMP_Text messageText;
    private Animator anim;
    private int activeTips;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        displayTipEvent += DisplayTips;
        disableTipEvent += DisableTips;
    }
    private void OnDisable()
    {
        displayTipEvent -= DisplayTips;
        disableTipEvent -= DisableTips;
    }

    private void DisplayTips(string message)
    {
        messageText.text = message;
        anim.SetInteger("state", ++activeTips);
    }
    private void DisableTips()
    {
        anim.SetInteger("state", --activeTips);

    }
}
