using System.Collections.Generic;
using UnityEngine;

namespace Script.Game.Entity
{
    public class EntityManager : MonoBehaviour
    {
        // Dictionnaire des entités vivantes (id -> LivingEntity)
        private Dictionary<string, LivingEntity> livingEntities = new Dictionary<string, LivingEntity>();

        /// <summary>
        /// Met à jour les entités vivantes à partir de la liste reçue du serveur.
        /// </summary>
        /// <param name="entitiesFromServer">Liste à jour envoyée par le serveur.</param>
        public void ProcessLivingEntitiesFromServer(List<LivingEntity> entitiesFromServer)
        {
            HashSet<string> seenIds = new HashSet<string>();

            // Mise à jour ou ajout des entités
            foreach (var serverEntity in entitiesFromServer)
            {
                seenIds.Add(serverEntity.Id);

                if (livingEntities.ContainsKey(serverEntity.Id))
                {
                    // Met à jour les champs de l'entité existante
                    UpdateEntity(livingEntities[serverEntity.Id], serverEntity);
                }
                else
                {
                    // Ajoute une nouvelle entité
                    livingEntities[serverEntity.Id] = serverEntity;
                    Debug.Log($"[EntityManager] Nouvelle entité ajoutée : {serverEntity.Name} (ID: {serverEntity.Id})");
                }
            }

            // Supprime les entités qui ne sont plus sur le serveur
            List<string> toRemove = new List<string>();
            foreach (var existingId in livingEntities.Keys)
            {
                if (!seenIds.Contains(existingId))
                {
                    toRemove.Add(existingId);
                }
            }

            foreach (var id in toRemove)
            {
                Debug.Log($"[EntityManager] Entité supprimée : ID = {id}");
                livingEntities.Remove(id);
            }
        }

        /// <summary>
        /// Met à jour les champs d'une entité existante à partir des données du serveur.
        /// </summary>
        private void UpdateEntity(LivingEntity localEntity, LivingEntity serverEntity)
        {
            localEntity.Name = serverEntity.Name;
            localEntity.Health = serverEntity.Health;
            localEntity.MaxHealth = serverEntity.MaxHealth;
            localEntity.Armor = serverEntity.Armor;
            localEntity.MagicResist = serverEntity.MagicResist;
            localEntity.AttackDamage = serverEntity.AttackDamage;
            localEntity.AbilityPower = serverEntity.AbilityPower;
            localEntity.MoveSpeed = serverEntity.MoveSpeed;
            localEntity.Moving = serverEntity.Moving;
            localEntity.PosX = serverEntity.PosX;
            localEntity.PosZ = serverEntity.PosZ;
            localEntity.PosXDesired = serverEntity.PosXDesired;
            localEntity.PosZDesired = serverEntity.PosZDesired;
            localEntity.Team = serverEntity.Team;
        }

        /// <summary>
        /// Accès aux entités pour d'autres scripts (lecture seule).
        /// </summary>
        public IReadOnlyDictionary<string, LivingEntity> GetLivingEntities()
        {
            return livingEntities;
        }
    }
}
