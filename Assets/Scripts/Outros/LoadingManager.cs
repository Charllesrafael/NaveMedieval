using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Progress;
using Doozy.Engine.SceneManagement;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Nephenthesys
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager instance;
        public UIView uIView;
        public float transicaoSnapshot = 0.2f;
        public AudioMixerSnapshot loadingSnapshot;
        public AudioMixerSnapshot NOTloadingSnapshot;

        public Progressor sceneloaderGroup;
        public SceneLoader sceneloaderSingle;
        public SceneLoader sceneloaderAdditive;

        public ProgressorGroup progressorGroup;
        public bool loading;

        private Action LoadAction;
        private string activeScene;

        private void Awake()
        {
            if (instance != null)
                Destroy(this.gameObject);

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public static void LoadSceneAdditive(int idScene, string nameSceneAditive)
        {
            LoadSceneAdditive(SceneManager.GetSceneAt(idScene).name, nameSceneAditive);
        }

        public static void LoadSceneAdditive(string nameScene, string nameSceneAditive)
        {
            Validate(() =>
            {
                instance.loadingSnapshot.TransitionTo(instance.transicaoSnapshot);

                instance.progressorGroup.Progressors.Clear();
                instance.progressorGroup.Progressors.Add(instance.sceneloaderSingle.Progressor);
                instance.progressorGroup.Progressors.Add(instance.sceneloaderAdditive.Progressor);

                instance.uIView.Show();
                instance.loading = true;
                instance.LoadAction = () =>
                {
                    instance.sceneloaderSingle.LoadSceneAsyncSingle(nameScene);
                    instance.activeScene = nameScene;
                    instance.sceneloaderAdditive.LoadSceneAsyncAdditive(nameSceneAditive);
                };
            });
        }

        public static void LoadSceneSingle(string nameScene)
        {
            Validate(() =>
            {
                instance.loadingSnapshot.TransitionTo(instance.transicaoSnapshot);

                instance.progressorGroup.Progressors.Clear();
                instance.progressorGroup.Progressors.Add(instance.sceneloaderSingle.Progressor);

                instance.loading = true;
                instance.uIView.Show();
                instance.LoadAction = () =>
                {
                    instance.sceneloaderSingle.LoadSceneAsyncSingle(nameScene);
                };
            });
        }

        public void OnLoadAction()
        {
            LoadAction?.Invoke();
        }

        public void Progreso(float valor)
        {
            if (valor >= 1f && loading)
            {
                uIView.Hide();
                loading = false;

                instance.NOTloadingSnapshot.TransitionTo(instance.transicaoSnapshot);

                if (GameManager.instance != null)
                {
                    GameManager.instance.Playing = true;
                    if (!string.IsNullOrEmpty(activeScene))
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeScene));
                        activeScene = "";
                    }
                    AudioSystem.instance.PlayNextSong();//<<<<<<<<
                }
            }
        }

        public static void Validate(Action action)
        {
            if (instance == null)
            {
                var loadingManager = Resources.Load<LoadingManager>("LoadingManager");
                instance = Instantiate(loadingManager);
            }

            if (instance.loading)
                return;

            instance.sceneloaderGroup.AnimateValue = false;
            instance.sceneloaderSingle.Progressor.InstantSetValue(0);
            instance.sceneloaderAdditive.Progressor.InstantSetValue(0);
            instance.sceneloaderGroup.InstantSetValue(0);
            instance.sceneloaderGroup.AnimateValue = true;

            action?.Invoke();
        }


    }
}
