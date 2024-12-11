
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class StaminaChangeEvent : UnityEvent<float> { }

public class Stamina : MonoBehaviour


{

    //[SerializeField] private Image staminaUI;
    [SerializeField] private float staminaDuration = 5f;
    [SerializeField] public float currentStamina;
    public StaminaChangeEvent onStaminaChanged;
    private PlayerMovement pm;

    void Start()
    {
        //staminaUI.fillAmount = 1f; 
        currentStamina = staminaDuration;
        pm = GetComponent<PlayerMovement>();
        onStaminaChanged.Invoke(currentStamina / staminaDuration);
    }

    // Update is called once per frame
    private void Update()
    {

        if (!pm.wallGrab && currentStamina <= staminaDuration && pm.isGrounded || pm.wallGrab && pm.isGrounded && currentStamina <= staminaDuration)
            {

                currentStamina += Time.deltaTime * 2.5f;
                
            }
            else if (pm.wallGrab)
            {
                currentStamina -= Time.deltaTime / 5f;
                
                if (Input.GetAxisRaw("Vertical") != 0)
                {
                    currentStamina -= Time.deltaTime;
                    
                }
            }
        if (currentStamina <= 0f)
        {
            pm.wallGrab = false;
        }
        currentStamina = Mathf.Clamp(currentStamina, 0f, staminaDuration);
        onStaminaChanged.Invoke(currentStamina / staminaDuration);
    }
    public void UseStamia(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0f, staminaDuration);

        onStaminaChanged.Invoke(currentStamina / staminaDuration);
    }
}
