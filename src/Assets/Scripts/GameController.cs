using UnityEngine;

namespace Assets.Scripts
{
        public class GameController : MonoBehaviour
        {
                [SerializeField] private Settings settings;
                [SerializeField] private QuestController questController;

                public void OnEnable()
                {
                        if (settings == null)
                        {
                                settings = UnityEngine.Object.FindObjectOfType<Settings>();
                        }
                        if (settings == null)
                        {
                                settings = ScriptableObject.CreateInstance<Settings>();
                        }

                        if (questController == null)
                        {
                                questController = FindObjectOfType<QuestController>();
                        }
                }

                public void Start()
                {
                        // ReSharper disable once UseNullPropagation
                        if (questController != null)
                        {
                                questController.StartQuest(settings.M, settings.N);
                        }
                }


                public void Update()
                {
                }

                public void OnDestroy()
                {
                        // ReSharper disable once UseNullPropagation
                        if (questController != null)
                        {
                                questController.StopQuest();
                        }
                }
        }
}