using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nephenthesys
{
	public class ControllerUISelection : MonoBehaviour
	{
		public RectTransform selecao;
		public float duration = 0.2f;
		public float delayAtivacao = 0.1f;
		private GameObject AnteriorSelecao;
		private bool ativo;

		IEnumerator Start()
		{
			yield return new WaitForSeconds(delayAtivacao);
			ativo = true;
		}

		public void Hide()
		{
			ativo = false;
			selecao.gameObject.SetActive(false);
			AnteriorSelecao = null;
		}

		public void Show()
		{
			ativo = true;
		}

        public void SetPositionUI(RectTransform _transform, float _duration)
		{
			DOTween.Complete(selecao);	
			if(_duration > 0){
				DOTween.To(()=>selecao.position,x=>selecao.position = x,_transform.position,duration).SetUpdate(true);
				selecao.DOSizeDelta(_transform.rect.size,duration).SetUpdate(true);
			}else
			{
				selecao.position = _transform.position;
				selecao.sizeDelta  = _transform.rect.size;
			}
		}

		void Update()
		{
			if(!ativo)
			return;

			if(EventSystem.current.currentSelectedGameObject == null || !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
			{
				selecao.gameObject.SetActive(false);
				return;
			}

			if(AnteriorSelecao != EventSystem.current.currentSelectedGameObject)
			{
				if(EventSystem.current.currentSelectedGameObject != null){
					RectTransform _rectTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
					if(_rectTransform != null)
					{
						if(AnteriorSelecao != null)
							SetPositionUI(_rectTransform, duration);
						else
							SetPositionUI(_rectTransform, 0);
					}

					AnteriorSelecao = EventSystem.current.currentSelectedGameObject;
				}
			}

			selecao.gameObject.SetActive(EventSystem.current.currentSelectedGameObject != null);

		}
	}
}
