using System;
using TMPro;

namespace Nephenthesys
{
    public class TypistTextMeshProUGui : Typist
    {
        public TextMeshProUGUI textmeshprougui;
        public PlayAudio playAudio;

        public override void SetText(string _text)
        {
            textmeshprougui.text = _text;
        }

        public override void PlayTypingSound()
        {
            playAudio?.Play();
        }
    }
}
