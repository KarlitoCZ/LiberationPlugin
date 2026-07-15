using System;
using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using InventorySystem;
using InventorySystem.Items;
using LabApi.Features.Wrappers;
using LiberationPlugin.Util;
using MapGeneration;
using PlayerRoles;
using UnityEngine;
using Random = System.Random;

namespace LiberationPlugin;


public sealed class SpawnHandling
{
    public static SpawnHandling Instance { get; } = new SpawnHandling();
    public List<LiberatorPlayer> ActiveLiberationPlayers = new();

    private SpawnHandling() {}

    public int UntilSpawn = 600;

    private List<Player> GetSpawnWavePlayers(int minSpawn = 3, int maxSpawn = 10, float spawnPercentage = 0.4f)
    {
        var totalConnected = Player.ReadyList.Count();

        if (totalConnected == 0)
            return new List<Player>();

        var spectators = Player.ReadyList
            .Where(p => p.Role == RoleTypeId.Spectator)
            .ToList();

        if (spectators.Count == 0)
            return new List<Player>();

        var targetCount = (int)Math.Round(totalConnected * spawnPercentage);

        var playersToSpawnCount = Math.Max(minSpawn, Math.Min(targetCount, maxSpawn));

        playersToSpawnCount = Math.Min(playersToSpawnCount, spectators.Count);

        var selectedPlayers = spectators
            .OrderBy(_ => Guid.NewGuid())
            .Take(playersToSpawnCount)
            .ToList();

        return selectedPlayers;
    }

    private void SpawnPlayer(Player player)
    {
        Room gateARoom = Room.List.FirstOrDefault(r => r.Name == RoomName.EzIntercom);
        if (gateARoom == null) return;
        var random = new Random();

        Vector3 localOffset = new Vector3(-2.5f, -5.0f, 2.7f);
        Vector3 spawnPosition = gateARoom.Transform.TransformPoint(localOffset);
        player.Position = spawnPosition +
                          new Vector3((float)random.NextDouble() - 0.5f, 0.0f, (float)random.NextDouble() - 0.5f);
        player.Rotation = gateARoom.Rotation;
    }

    public void GiveLiberatorRole(Player player, LiberatorRank rank)
    {
        player.Role = RoleTypeId.Tutorial;

        var libPlayer = new LiberatorPlayer(player, rank);
        ActiveLiberationPlayers.Add(libPlayer);

        player.CustomInfo = "L.A.F. - " + rank.Name;

        foreach (var i in rank.Loadout)
        {
            player.Inventory.ServerAddItem(i, ItemAddReason.StartingItem);
        }

        for (int i = 0; i < 2; i++)
        {
            player.Inventory.ServerAddItem(ItemType.Ammo556x45, ItemAddReason.StartingItem);
        }

        KeycardGiver.Instance.GiveKeycard(player, ItemType.KeycardCustomSite02, "Liberator Keycard",
            new Color32(255, 255, 255, 255), 3, 1, 3, new Color32(255, 89, 106, 255), new Color32(168, 34, 54, 255));

        player.SendHint("Arrest and escort everyone. \n Work with SCPs to win. ", duration: 15f);
    }

    public void CleanUpLiberatorRole(LiberatorPlayer player)
    {
        ActiveLiberationPlayers.Remove(player);
        player.Player.CustomInfo = "";
    }

    public bool SpawnWave()
    {
        var players = Player.List;
        if (players.Count <= LiberationPlugin.PluginConfig.TeamCapMin) return false;
        
        var chosenPlayers = GetSpawnWavePlayers(LiberationPlugin.PluginConfig.TeamCapMin, LiberationPlugin.PluginConfig.TeamCapMax);
        if (chosenPlayers.Count <= LiberationPlugin.PluginConfig.TeamCapMin) return false;
        
        foreach (var player in chosenPlayers)
        {
            
            GiveLiberatorRole(player, LiberatorRank.Freeborn);
            SpawnPlayer(player);
        }
        
        Announcer.Message(
            $".g2 .g5 $PITCH_0.97 ATTENTION ALL PERSONNEL. $PITCH_1 .g2 BREACH .g4 DETECTED IN ENTRANCE... ZONE. $PITCH_0.93 INDENTIFIED {chosenPlayers.Count} UNAUTHORIZED $PITCH_1 L A F PERSONNEL $PITCH_1 .g1",
            $"Attention all personnel. Breach detected in Entrance-Zone. Identified {chosenPlayers.Count} unauthorized Liberation. Armed. Forces. personnel.");
        
        return true;
    }
}