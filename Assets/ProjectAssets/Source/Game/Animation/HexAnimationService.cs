using System;
using DG.Tweening;
using Game.Data;
using Game.Entities;
using Game.Gameplay;
using Game.Rendering;
using UnityEngine;
using Zenject;

namespace Game.Animation
{
    public sealed class HexAnimationService : IInitializable, IDisposable
    {
        private const Ease DefaultEase = Ease.InOutQuad;

        private static Sequence CreateStackAnimation(HexStackComponent stack, int hexCount, float duration, out float hexAnimationDuration)
        {
            hexAnimationDuration = duration / hexCount;
            Sequence animation = DOTween.Sequence();
            animation.SetLink(stack.gameObject);
            return animation;
        }

        private readonly GameConfigAsset gameConfig;
        private readonly GameplayManager gameplayManager;
        private readonly HexGridRenderer hexGridRenderer;

        private void OnAddingNewPlayerStack(GameplayManager manager, HexStackComponent playerStack, float duration)
        {
            if (duration <= 0f) return;
            Transform stackTransform = playerStack.transform;
            Vector3 targetScale = stackTransform.localScale;
            stackTransform.localScale = Vector3.zero;
            float punchScaleDuration = duration * 0.25f;
            Sequence animation = DOTween.Sequence();
            animation.SetLink(playerStack.gameObject);
            animation.Append(stackTransform.DOScale(targetScale, duration - punchScaleDuration).SetEase(DefaultEase));
            animation.Append(stackTransform.DOPunchScale(Vector3.one * 0.2f, punchScaleDuration, 3, 1f));
        }
        
        private void OnMergingHexStack(GameplayManager manager, HexStackComponent sourceStack, HexStackComponent targetStack, int hexCount, float duration)
        {
            if (duration <= 0f) return;
            HexComponent sourceTopHex = sourceStack.TopHex;
            HexComponent targetTopHex = targetStack.TopHex;
            float movingHexHeight = sourceTopHex.Bounds.size.y;
            Vector3 targetHexPosition = targetTopHex.transform.position;
            targetHexPosition.y = targetTopHex.GetTopPoint().y + movingHexHeight * 0.5f;
            Vector3 rotationAxis = targetHexPosition - sourceTopHex.GetTopPoint();
            rotationAxis.y = 0f;
            rotationAxis.Normalize();
            rotationAxis = Vector3.Cross(rotationAxis, Vector3.up);
#if UNITY_LUNA
            rotationAxis = -rotationAxis;
#endif
            var targetRotation = sourceTopHex.transform.rotation * Quaternion.AngleAxis(180f, rotationAxis);
            Sequence animation = CreateStackAnimation(targetStack, hexCount, duration, out float hexAnimationDuration);
            float animationDelay = hexAnimationDuration * 0.25f;
            float jumpHeight = gameConfig.StackingAnimationJumpHeight;
            float startTime = 0f;
            
            foreach (HexComponent hex in sourceStack.GetTopHexes(hexCount))
            {
                Transform hexTransform = hex.transform;
                Quaternion initialRotation = hexTransform.rotation;
                animation.Insert(startTime, hexTransform.DOJump(targetHexPosition, jumpHeight, 1, hexAnimationDuration).SetEase(DefaultEase));
                animation.Insert(startTime, hexTransform.DORotateQuaternion(targetRotation, hexAnimationDuration).SetEase(DefaultEase));
                animation.AppendCallback(() => hexTransform.rotation = initialRotation);
                targetHexPosition.y += movingHexHeight;
                startTime += animationDelay;
            }
            
            animation.timeScale = animation.Duration() / duration;
        }

        private void OnDestroyingStackHexes(GameplayManager manager, HexStackComponent stack, int hexCount, float duration)
        {
            if (duration <= 0f) return;
            Sequence animation = CreateStackAnimation(stack, hexCount, duration, out float hexAnimationDuration);
            
            foreach (HexComponent hex in stack.GetTopHexes(hexCount))
            {
                animation.Append(hex.transform.DOScale(Vector3.zero, hexAnimationDuration)
                    .SetEase(DefaultEase)
                    .OnComplete(() => hex.Renderer.enabled = false));
            }
        }
        
        private void OnHighlightedHexChanged(HexGridRenderer renderer, HexComponent lastHex, HexComponent newHex)
        {
            if (lastHex != null)
            {
                DOVirtual.Color(renderer.HexHighlightColor, renderer.DefaultHexColor, gameConfig.HexHighlightDuration, color => lastHex.Color = color)
                    .SetEase(DefaultEase)
                    .SetLink(lastHex.gameObject);
            }
            
            if (newHex != null)
            {
                DOVirtual.Color(renderer.DefaultHexColor, renderer.HexHighlightColor, gameConfig.HexHighlightDuration, color => newHex.Color = color)
                    .SetEase(DefaultEase)
                    .SetLink(newHex.gameObject);
            }
        }
        
        public HexAnimationService(GameConfigAsset gameConfig, GameplayManager gameplayManager, HexGridRenderer hexGridRenderer)
        {
            this.gameConfig = gameConfig;
            this.gameplayManager = gameplayManager;
            this.hexGridRenderer = hexGridRenderer;
            gameplayManager.AddingNewPlayerStack += OnAddingNewPlayerStack;
            gameplayManager.MergingHexStack += OnMergingHexStack;
            gameplayManager.DestroyingStackHexes += OnDestroyingStackHexes;
            hexGridRenderer.HighlightedHexChanged += OnHighlightedHexChanged;
        }

        public void Initialize()
        {
            DOTween.Init();
        }

        public void Dispose()
        {
            if (gameplayManager != null)
            {
                gameplayManager.AddingNewPlayerStack -= OnAddingNewPlayerStack;
                gameplayManager.MergingHexStack -= OnMergingHexStack;
                gameplayManager.DestroyingStackHexes -= OnDestroyingStackHexes;
            }

            if (hexGridRenderer != null)
            {
                hexGridRenderer.HighlightedHexChanged -= OnHighlightedHexChanged;
            }
        }
    }
}