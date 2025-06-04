using UnityEngine;

namespace Script.Game.Entity
{
    public class SpawnEntity : MonoBehaviour
    {
        [SerializeField] private GameObject entityPrefab;

        public void Spawn(LivingEntity entityData)
        {
            GameObject go = Instantiate(entityPrefab, new Vector3(entityData.PosX, 0, entityData.PosZ), Quaternion.identity);
            go.name = $"Entity_{entityData.Name}_{entityData.Id}";


            // Enregistrer dans EntityManager
            EntityManager.Instance.RegisterEntity(entityData.Id, go);
        }
        //ajouter un game objet identifié par l'id'
        
        // affecter les variables de l'entité à partir de la réponse du serveur
    }
}