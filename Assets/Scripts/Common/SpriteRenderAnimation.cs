using UnityEngine;
namespace Common
{
    class SpriteRenderFadeAnimation
    {
        public float _currentAlpha;
        public bool _completed = false;
        public int alpha = 1;
        public int count = 0;
        SpriteRenderer spriteRenderer;
        public SpriteRenderFadeAnimation(SpriteRenderer _spriteRenderer)
        {
            spriteRenderer = _spriteRenderer;
        }
        public void FadeOut(float speed = 0.03f)
        {
            count++;
            spriteRenderer.color = new Color(255, 255, 255, alpha - (count * speed));
            _currentAlpha = spriteRenderer.color.a;
            if (_currentAlpha <= 0)
            {
                _completed = true;
            }
        }
    }
}