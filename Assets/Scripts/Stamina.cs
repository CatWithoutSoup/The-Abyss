using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Stamina : MonoBehaviour


{
    //[SerializeField] private Image staminaUI;
    [SerializeField] private float staminaDuration = 5f;
    [SerializeField] private float currentStamina;
    [SerializeField] private GameObject player;
    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        //staminaUI.fillAmount = 1f; 
        currentStamina = staminaDuration;
        pm = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!pm.wallGrab && currentStamina <= staminaDuration && pm.isGrounded || pm.wallGrab && pm.isGrounded && currentStamina <= staminaDuration)
        {
            currentStamina += Time.deltaTime * 2.5f;
            //staminaUI.fillAmount = currentStamina / staminaDuration;
        }
        else if (pm.wallGrab)
        {
            currentStamina -= Time.deltaTime / 5f;
            //staminaUI.fillAmount = currentStamina / staminaDuration;
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                currentStamina -= Time.deltaTime;
                //staminaUI.fillAmount = currentStamina / staminaDuration;
            }
        }
        if (currentStamina <= 0f)
        {
            pm.wallGrab = false;
        }
    }
}
