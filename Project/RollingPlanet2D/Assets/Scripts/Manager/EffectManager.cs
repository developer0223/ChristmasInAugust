﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Manager
{
    public class EffectManager : Manager
    {
        private const float start = 0.0f;
        private const float end = 1.0f;

        public void FadeIn(Text text, float playTime)
        {
            StartCoroutine(ETextFadeIn(text, playTime));
        }

        public void FadeOut(Text text, float playTime)
        {
            StartCoroutine(ETextFadeOut(text, playTime));
        }

        public void FadeIn(Image image, float playTime)
        {
            StartCoroutine(EFadeIn(image, playTime, x => { }));
        }

        public void FadeOut(Image image, float playTime)
        {
            StartCoroutine(EFadeOut(image, playTime, x => { }));
        }

        public void FadeIn(Image image, float playTime, Action<object> callback)
        {
            StartCoroutine(EFadeIn(image, playTime, x => callback(x)));
        }

        public void FadeOut(Image image, float playTime, Action<object> callback)
        {
            StartCoroutine(EFadeOut(image, playTime, x => callback(x)));
        }

        private IEnumerator ETextFadeIn(Text text, float playTime)
        {
            Color color = text.color;
            float time = start;
            while (color.a < end)
            {
                time += Time.deltaTime / playTime;
                color.a = Mathf.Lerp(start, end, time);
                text.color = color;
                yield return null;
            }
            color.a = 1;
            text.color = color;
        }

        private IEnumerator ETextFadeOut(Text text, float playTime)
        {
            Color color = text.color;
            float time = start;
            while (color.a > start)
            {
                time += Time.deltaTime / playTime;
                color.a = Mathf.Lerp(end, start, time);
                text.color = color;
                yield return null;
            }
            color.a = 0;
            text.color = color;
        }

        private IEnumerator EFadeIn(Image image, float playTime, Action<object> fadeIn)
        {
            Color color = image.color;
            float time = start;
            while (color.a < end)
            {
                time += Time.deltaTime / playTime;
                color.a = Mathf.Lerp(start, end, time);
                image.color = color;
                yield return null;
            }
            color.a = 1;
            image.color = color;
            fadeIn(null);
        }

        private IEnumerator EFadeOut(Image image, float playTime, Action<object> fadeOut)
        {
            Color color = image.color;
            float time = start;
            while (color.a >start)
            {
                time += Time.deltaTime / playTime;
                color.a = Mathf.Lerp(end, start, time);
                image.color = color;
                yield return null;
            }
            color.a = 0;
            image.color = color;
            fadeOut(null);
        }
    }
}