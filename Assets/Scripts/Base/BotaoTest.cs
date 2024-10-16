using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nephenthesys
{
    public class BotaoTest : Button
    {
        public Action Clicar;
        protected override void Awake()
        {
            base.Awake();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }
    }
}