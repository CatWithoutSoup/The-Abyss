using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StaminaChangeEvent : UnityEvent<float> { }
public class Stamina : MonoBehaviour
{
    [SerializeField] private float staminaDuration = 5f;
    [SerializeField] public float currentStamina;
    public StaminaChangeEvent onStaminaChanged;
    private PlayerMovement pm;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentStamina = staminaDuration;
        pm = GetComponent<PlayerMovement>();
        onStaminaChanged.Invoke(currentStamina / staminaDuration);
    }

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
                else if (Input.GetKeyDown(KeyCode.Z))
            {
                    currentStamina -= 1f;
            }
            }
        if (currentStamina < 1.5f && !isBlinking)
        {
            StartCoroutine(BlinkRed());
        }
        if (currentStamina <= 0f)
        {
            pm.wallGrab = false;
        }
        currentStamina = Mathf.Clamp(currentStamina, 0f, staminaDuration);
        onStaminaChanged.Invoke(currentStamina / staminaDuration);
    }
    IEnumerator BlinkRed()
    {
        isBlinking = true;
        while (currentStamina < 1.5f)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);

        }
        isBlinking = false;
    }
}
