using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider mySlider;
    private playerhealth ph;  // 선언만 하기

    void Start()
    {
        // Start에서 초기화
        ph = gameObject.GetComponent<playerhealth>();
    }

    private void Update()
    {
        if (ph != null)
        {
            mySlider.value = ph.GetHealthPercent();
        }
    }
}
