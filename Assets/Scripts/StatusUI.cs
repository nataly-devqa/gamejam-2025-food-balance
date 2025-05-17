using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public FoodCollector collector;
    public Slider vitaminSlider, calciumSlider, ironSlider, junkSlider;
    public Text vitaminText, calciumText, ironText, junkText;
    void Update()
    {
        if (collector == null)
        {
            Debug.Log("Collector not assigned!");
            return;
        }
      //Debug.Log("Vitamin value: " + collector.vitamin);

        vitaminSlider.value = collector.vitamin;
        calciumSlider.value = collector.calcium;
        ironSlider.value = collector.iron;
        junkSlider.value = collector.junk;

        vitaminText.text = $"{(int)collector.vitamin} / 5";
        calciumText.text = $"{(int)collector.calcium} / 5";
        ironText.text = $"{(int)collector.iron} / 5";
        junkText.text = $"{(int)collector.junk} / 3";
    }
    
}
