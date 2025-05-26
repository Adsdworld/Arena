using System;
using System.IO;
using UnityEngine;
using Script.Utils;

namespace Script.Game.Core
{
    public static class UuidManager
    {
        private static readonly string UuidFilePath = Path.Combine(Application.persistentDataPath, "client_uuid.txt");
        private static string _cachedUuid;
        private static readonly string DirectoryOfFile = Path.GetDirectoryName(UuidFilePath);


        public static string GetUuid()
        {
            if (!string.IsNullOrEmpty(_cachedUuid))
                return _cachedUuid;

            if (File.Exists(UuidFilePath))
            {
                try
                {
                    _cachedUuid = File.ReadAllText(UuidFilePath);
                    if (!string.IsNullOrEmpty(_cachedUuid))
                    {
                        Log.Info($"UUID lu depuis fichier : {_cachedUuid}");
                        return _cachedUuid;
                    }
                }
                catch (Exception ex)
                {
                    Log.Warn($"Erreur lecture UUID : {ex.Message}");
                }
            }

            _cachedUuid = Guid.NewGuid().ToString();
            WriteUuidToFile(_cachedUuid);
            Log.Info($"Nouveau UUID généré et sauvegardé : {_cachedUuid}");
            return _cachedUuid;
        }

        public static void SetUuid(string uuid)
        {
            if (string.IsNullOrEmpty(uuid))
                throw new ArgumentException("UUID ne peut pas être null ou vide", nameof(uuid));

            _cachedUuid = uuid;
            WriteUuidToFile(uuid);
            Log.Info($"UUID forcé et sauvegardé : {uuid}");
        }

        private static void WriteUuidToFile(string uuid)
        {
            try
            {
                if (!Directory.Exists(DirectoryOfFile))
                {
                    Directory.CreateDirectory(DirectoryOfFile);
                    Log.Info("Répertoire créé pour sauvegarde UUID");
                }

                File.WriteAllText(UuidFilePath, uuid);
                Log.Info($"UUID écrit dans le fichier : {uuid}");
            }
            catch (Exception ex)
            {
                Log.Failure($"Erreur écriture UUID : {ex.Message}");
            }
        }
    }
}
