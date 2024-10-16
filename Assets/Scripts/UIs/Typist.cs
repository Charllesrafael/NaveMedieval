using System.Collections;
using UnityEngine;
using System;

namespace Nephenthesys
{
    public class Typist : MonoBehaviour
    {
        [SerializeField] private float speed = 0.1f;
        private float speedTemp = 0;
        private bool typing;
        private Action startTyping;
        private Action endTyping;

        public Typist ToType(string _text)
        {
            if (typing)
            {
                Debug.LogWarning("Current text is still being typed.");
                return this;
            }
            StopAllCoroutines();
            StartCoroutine(Typing(_text));
            return this;
        }

        public Typist SetTypingSpeed(float _speed)
        {
            speed = _speed;
            return this;
        }

        public Typist OnStartTyping(Action _startTyping)
        {
            startTyping = _startTyping;
            return this;
        }

        public Typist OnEndTyping(Action _endTyping)
        {
            endTyping = _endTyping;
            return this;
        }

        public void TypeAll()
        {
            speedTemp = 0;
            typing = false;
        }

        public IEnumerator Typing(string _text)
        {
            typing = true;
            startTyping?.Invoke();
            speedTemp = speed;
            string _textTemp = "";
            SetText(_textTemp);

            foreach (var item in _text)
            {
                if (speedTemp == 0)
                    break;

                yield return new WaitForSeconds(speedTemp);
                _textTemp += item;

                SetText(_textTemp);
                if (item != ' ')
                    PlayTypingSound();
            }
            SetText(_text);
            typing = false;
            endTyping?.Invoke();
        }

        public bool IsTyping()
        {
            return typing;
        }

        public virtual void SetText(string _text)
        {

        }

        public virtual void PlayTypingSound()
        {

        }
    }
}
