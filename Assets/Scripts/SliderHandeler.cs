using UnityEngine;
using UnityEngine.UI;
using PlanetManagement;
public class SliderHandeler : MonoBehaviour {
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Heatmap heatMap;
    public void UpdateFloat() { heatMap.SetIntensity(slider.value); }
}
