using UnityEngine;
using TMPro;

namespace LastLineDefense.Utils
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private float floatSpeed = 1.5f;
        [SerializeField] private float fadeSpeed = 2f;
        [SerializeField] private float lifetime = 0.8f;

        private TMP_Text text;
        private Color startColor;
        private float timer;

        public static void Create(Vector3 position, int damage, bool isCritical = false)
        {
            var go = new GameObject("DamagePopup");
            go.transform.position = position + new Vector3(Random.Range(-0.3f, 0.3f), 0.5f, 0);

            var tmp = go.AddComponent<TextMeshPro>();
            tmp.text = damage.ToString();
            tmp.fontSize = isCritical ? 6 : 4;
            tmp.color = isCritical ? Color.yellow : Color.white;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.sortingOrder = 100;

            var popup = go.AddComponent<DamagePopup>();
            popup.text = tmp;
            popup.startColor = tmp.color;
        }

        private void Update()
        {
            timer += Time.deltaTime;

            transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            if (text != null)
            {
                float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
                text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            }

            if (timer >= lifetime)
                Destroy(gameObject);
        }
    }
}
