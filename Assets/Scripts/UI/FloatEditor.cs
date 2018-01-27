using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class FloatEditor : MonoBehaviour {
    
	//[SerializeField] private TextMeshProUGUI label;
    //[SerializeField] private Text label;
    [SerializeField] private Slider slider;
    [SerializeField] private InputField inputField;
    private float val;
    private float minVal;
    private float maxVal;

    void Awake()
    {
        this.minVal = this.slider.minValue;
        this.maxVal = this.slider.maxValue;
    }

    // Sets the value to newVal if it is within bounds. Updates UI elements to reflect the value
    // afterwards (even if there wasn't necessarily a change to the value). Returns true if
    // newVal was within bounds.
    public bool SetValue(float newVal)
    {
        bool withinBounds = (newVal >= this.minVal && newVal <= this.maxVal);

        if (withinBounds)
        {
            this.val = newVal;
        }

        RefreshUI();

        return withinBounds;
    }

    // Sets the bounds of the value. Clamps the currnet value if needed.
    public void SetBounds(float min, float max)
    {
        this.minVal = min;
        this.maxVal = max;

        this.slider.minValue = this.minVal;
        this.slider.maxValue = this.maxVal;

        if (this.val > this.maxVal)
        {
            SetValue(this.maxVal);
        }
        else if (this.val < this.minVal)
        {
            SetValue(this.minVal);
        }
    }

    public void RefreshUI()
    {
        this.slider.value = this.val;
        this.inputField.text = this.val.ToString();
    }
}
