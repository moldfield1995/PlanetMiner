using UnityEngine.UI;
using UnityEngine;

namespace PlanetManagement
{
    public enum OreTypes
    {
        Ore1,
        Ore2,
        Ore3,
        Ore4,
        Ore5,
        Ore6
    }

    public class OreViewHandeler : MonoBehaviour
    {
        private const float ButtonHeight = 30f;
        [SerializeField] private RectTransform scrollTransform;
        [SerializeField] private Heatmap heatmap;

        [SerializeField] private GameObject buttonPrefab;

        // Use this for initialization
        private void Start()
        {
            var newButtonGameObject = Instantiate(buttonPrefab, scrollTransform);
            var newButton = newButtonGameObject.GetComponent<Button>();
            newButton.onClick.AddListener(delegate { SelectOre(0); });
        }

        public void CreateButton()
        {
            int amountOfOres = heatmap.GetAmountOfOres() - 1;
            scrollTransform.sizeDelta = new Vector2(scrollTransform.sizeDelta.x,
                scrollTransform.sizeDelta.y + ButtonHeight);
            var newButtonGameObject = Instantiate(buttonPrefab, scrollTransform);
            var newButton = newButtonGameObject.GetComponent<Button>();
            newButton.onClick.AddListener(delegate { SelectOre(amountOfOres); });
        }

        private void SelectOre(int index)
        {
            heatmap.SwtichOre(index);
        }
    }
}
