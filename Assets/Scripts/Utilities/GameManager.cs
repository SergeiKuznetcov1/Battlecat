using UnityEngine;
using System.Collections.Generic;
    public class GameManager : MonoBehaviour
    {
        private static GameManager _GM;
        private ImageFader _imageFader;
        private Door _door;
        private List<Gem> _gems;
        private void Awake() {
            if (_GM == null) {
                _GM = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                Destroy(gameObject);
            }

            _gems = new List<Gem>();
        }

        public static void RegisterImageFader(ImageFader imageFader) {
            if (_GM == null) 
                return;
            _GM._imageFader = imageFader;
        }

        public static void ManagerLoadLevel(int index) {
            if (_GM == null) 
                return;
            _GM._imageFader.SetLevel(index);
        }

        public static void ManagerRestartLevel() {
            if (_GM == null) 
                return;
            _GM._gems.Clear();
            _GM._imageFader.RestartLevel();
        }

        public static void RegisterDoor(Door door) {
            if (_GM == null) 
                return;
            _GM._door = door;
        }

        public static void RegisterGem(Gem gem) {
            if (_GM == null) 
                return;
            if (!_GM._gems.Contains(gem)) {
                _GM._gems.Add(gem);
            }
        }

        public static void RemoveGemFromList(Gem gem) {
            if (_GM == null) 
                return;
            _GM._gems.Remove(gem);
            // if (_GM._gems.Count == 0)
            //     _GM._door.UnlockDoor();
        }
    }

