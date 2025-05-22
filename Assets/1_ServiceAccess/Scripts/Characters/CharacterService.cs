using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public class CharacterService : MonoBehaviour
    {
        private readonly Dictionary<string, ICharacter> _charactersById = new();

        //instance to use the singleton design pattern
        public static CharacterService Instance { get; private set; }

        //singelton at Awake
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        public bool TryAddCharacter(string id, ICharacter character)
            => _charactersById.TryAdd(id, character);
        public bool TryRemoveCharacter(string id)
            => _charactersById.Remove(id);

        //getcharacter to obtain the desire character as output, and bool to determin if its founded
        public bool TryGetCharacter(string id, out ICharacter character)
            => _charactersById.TryGetValue(id, out character);
    }
}
