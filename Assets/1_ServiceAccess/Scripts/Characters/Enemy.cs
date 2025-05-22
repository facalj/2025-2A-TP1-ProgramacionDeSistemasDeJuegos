using System;
using UnityEngine;

namespace Excercise1
{
    public class Enemy : Character
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private string playerId = "Player";
        private ICharacter _player;
        private string _logTag;

        private void Reset()
            => id = nameof(Enemy);

        private void Awake()
            => _logTag = $"{name}({nameof(Enemy).Colored("#555555")}):";

        protected override void OnEnable()
        {
            base.OnEnable();
            //TODO: Get the reference to the player.
            //verify the existence of the characterservice instance
            if (CharacterService.Instance != null)
            {
                if (!CharacterService.Instance.TryGetCharacter(playerId, out _player))
                {
                    Debug.LogError($"{_logTag} Player not found!");
                }
            }
            else
            {
                Debug.LogError($"{_logTag} CharacterService instance is null!");
            }
        }

        private void Update()
        {
            if (_player == null)
                return;
            var direction = _player.transform.position - transform.position;
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }
    }
}