using Guardians;
using PlayerSystem;
using SceneLoadSystem;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Game/Settings")]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField]
        public GameBattleSettings Battle { get; private set; }

        [SerializeField, ScenePath] private string _mapScene;
        public string MapScene => _mapScene;

        [SerializeField, ScenePath] private string _editorScene;
        public string EditorScene => _editorScene;
        
        [field: SerializeField] public Player Player { get; set; }
        [field: SerializeField] public Guardian[] Guardians { get; set; }
    }
}