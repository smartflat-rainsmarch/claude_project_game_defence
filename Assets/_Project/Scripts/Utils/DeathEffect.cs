using UnityEngine;

namespace LastLineDefense.Utils
{
    public class DeathEffect : MonoBehaviour
    {
        [SerializeField] private float expandSpeed = 3f;
        [SerializeField] private float fadeSpeed = 4f;
        [SerializeField] private float lifetime = 0.4f;

        private SpriteRenderer sr;
        private Color startColor;
        private float timer;

        public static void Create(Vector3 position, Color color)
        {
            var go = new GameObject("DeathEffect");
            go.transform.position = position;
            go.transform.localScale = Vector3.one * 0.3f;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = FindWhiteSprite();
            sr.color = color;
            sr.sortingOrder = 50;

            var effect = go.AddComponent<DeathEffect>();
            effect.sr = sr;
            effect.startColor = color;
        }

        private static Sprite FindWhiteSprite()
        {
            var tex = new Texture2D(8, 8);
            var pixels = new Color[64];
            for (int i = 0; i < 64; i++) pixels[i] = Color.white;
            tex.SetPixels(pixels);
            tex.Apply();
            return Sprite.Create(tex, new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 8);
        }

        private void Update()
        {
            timer += Time.deltaTime;

            transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

            if (sr != null)
            {
                float alpha = Mathf.Lerp(1f, 0f, timer / lifetime);
                sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            }

            if (timer >= lifetime)
                Destroy(gameObject);
        }
    }
}
