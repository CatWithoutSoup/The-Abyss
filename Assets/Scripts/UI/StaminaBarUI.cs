using UnityEngine;
using UnityEngine.UI;


public class StaminaBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] public CanvasGroup visibleStaminaBar;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float staminaDuration = 5f;
    [SerializeField] private float currentStamina;

    public void Awake()
    {
        SetVisibility(false);
        if (player == null)
            player = GetComponentInParent<PlayerMovement>();
        if (player == null)
            Debug.Log("PlayerMovement не найден");
    }
    public void UpdateStaminaBar(float normalizedValue)
    {
        if (slider != null)
        {
            slider.value = normalizedValue;
            currentStamina = normalizedValue * staminaDuration;
        }
    }
    private void Update()
    {
        if (player.wallGrab)
        {
            //isVisibleFlag = !isVisibleFlag;
            SetVisibility(true);
        }
        if (currentStamina >= staminaDuration)
        {
            SetVisibility(false);
        }
        
    }
    private void SetVisibility(bool isVisible)
    {
        if (visibleStaminaBar != null)
        {
            visibleStaminaBar.alpha = isVisible ? 1 : 0;
            visibleStaminaBar.interactable = isVisible;
            visibleStaminaBar.blocksRaycasts = isVisible;

        }
    }
    private void LateUpdate()
    {
        
    }
}
