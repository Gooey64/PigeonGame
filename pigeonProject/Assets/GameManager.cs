// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance; 

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this; 
//             DontDestroyOnLoad(gameObject); 
//         }
//         else
//         {
//             Destroy(gameObject); 
//         }
//     }

//     private void Start()
//     {
//         string currentLevelName = SceneManager.GetActiveScene().name;

//         AudioManager.Instance.PlayLevelMusic(currentLevelName);
//     }

//     private void OnEnable()
//     {
//         SceneManager.sceneLoaded += OnSceneLoaded;
//     }

//     private void OnDisable()
//     {
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }

//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         AudioManager.Instance.PlayLevelMusic(scene.name);
//     }
// }

