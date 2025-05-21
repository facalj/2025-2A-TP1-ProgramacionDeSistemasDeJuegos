using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;

        protected virtual void OnEnable()
        {
            //TODO: Add to CharacterService. The id should be the given serialized field. 
            CharacterService.Instance?.TryAddCharacter(id, this);
        }

        protected virtual void OnDisable()
        {
            //TODO: Remove from CharacterService.
            CharacterService.Instance?.TryRemoveCharacter(id);
        }
    }
}