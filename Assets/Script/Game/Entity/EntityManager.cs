using System.Collections.Generic;
using UnityEngine;
using Script.Network.response;
using Script.Utils;
using UnityEngine.AI;

namespace Script.Game.Entity
{
    public class EntityManager : MonoBehaviour
    {
        [Header("Prefab à instancier pour chaque entité")]
        public GameObject entityPrefab;

        private Dictionary<string, GameObject> entityGameObjects = new Dictionary<string, GameObject>();
        
        private static EntityManager instance;

        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("EntityManager instance is null! Make sure EntityManager is present in the scene.");
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ProcessLivingEntitiesFromServer(List<LivingEntity> entitiesFromServer)
        {
            HashSet<string> seenIds = new HashSet<string>();

            foreach (var serverEntity in entitiesFromServer)
            {
                seenIds.Add(serverEntity.Id);

                if (entityGameObjects.TryGetValue(serverEntity.Id, out GameObject go))
                {
                    go.GetComponent<EntityComponent>().UpdateFromData(serverEntity);
                }
                else
                {
                    /* IMPORTANTE :
                     * Avant de déplacer la capsule désactiver navmesh agent qui force Y sur le plan, le colider et le rigidbody au cas ou
                     */
                    
                    Log.Info("[EntityManager] Instanciation de l'entité : " + serverEntity.Id + " at " + serverEntity.PosX + ", " + serverEntity.PosY + ", " + serverEntity.PosZ);
                    
                    GameObject newGo = Instantiate(entityPrefab);
                    
                    newGo.GetComponent<Rigidbody>().isKinematic = true;
                    newGo.GetComponent<Collider>().enabled = false;
                    newGo.GetComponent<NavMeshAgent>().enabled = false;
                    
                    newGo.transform.position = new Vector3(serverEntity.PosX, serverEntity.PosY, serverEntity.PosZ);
                    newGo.transform.rotation = Quaternion.Euler(0, serverEntity.RotationY, 0);
                    newGo.GetComponent<EntityComponent>().Initialize(serverEntity);
                    newGo.name = $"Entity_{serverEntity.Id}";
                    
                    newGo.GetComponent<Rigidbody>().isKinematic = serverEntity.Rigidbody_.IsKinematic;
                    newGo.GetComponent<Collider>().enabled = serverEntity.Collider_.Enabled;
                    newGo.GetComponent<NavMeshAgent>().enabled = serverEntity.NavMeshAgent_.Enabled;
                    
                    entityGameObjects[serverEntity.Id] = newGo;

                    Log.Info("[EntityManager] Nouvelle entité instanciée : " + serverEntity.Name + " (ID: " + serverEntity.Id + ")");
                }
            }

            // Supprimer les entités disparues
            List<string> toRemove = new List<string>();
            foreach (var existingId in entityGameObjects.Keys)
            {
                if (!seenIds.Contains(existingId))
                {
                    toRemove.Add(existingId);
                }
            }

            foreach (var id in toRemove)
            {
                Log.Info("[EntityManager] Entité non vue sur le serveur, suppression : " + id);
                Destroy(entityGameObjects[id]);
                entityGameObjects.Remove(id);
            }
        }

        public IReadOnlyDictionary<string, GameObject> GetEntityGameObjects()
        {
            return entityGameObjects;
        }
        
        public void RegisterEntity(string id, GameObject entityGameObject)
        {
            if (!entityGameObjects.ContainsKey(id))
            {
                entityGameObjects[id] = entityGameObject;
                Log.Info("[EntityManager] Entité enregistrée : " + id);
            }
            else
            {
                Log.Warn("[EntityManager] Tentative d'enregistrement d'une entité déjà existante : " + id);
            }
        }
    }
}
