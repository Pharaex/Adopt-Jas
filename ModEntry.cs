using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;

namespace Adopt_Jas
{
    internal sealed class ModEntry : Mod
    {
        // Flag to track if Jas is eligible for adoption
        private bool jasEligibleForAdoption = false;

        public override void Entry(IModHelper helper)
        {
            // Event triggered when the day starts
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
            // Hook into NPC dialogue and actions
            helper.Events.Player.Warped += this.OnPlayerWarped;
        }

        private void OnDayStarted(object? sender, DayStartedEventArgs? e)
        {
            try
            {
                // Attempt to update Jas's eligibility for adoption when a new day starts
                UpdateJasEligibility();
            }
            catch (Exception ex)
            {
                // Log any exceptions if try is not successful
                this.Monitor.Log($"An error occurred while updating Jas's eligibility: {ex.Message}", LogLevel.Error);
            }
        }

        private void UpdateJasEligibility()
        {
            // Check if the player is married to Shane
            if (Game1.player.spouse == "Shane")
            {
                // Set Jas's eligibility for adoption to true
                jasEligibleForAdoption = true;
                this.Monitor.Log($"Jas is eligible for adoption because {Game1.player.Name} is married to Shane.", LogLevel.Debug);

                // Add a custom flag to Jas's modData
                var jas = Game1.getCharacterFromName("Jas");
                if (jas != null)
                {
                    jas.modData["Adopt_Jas/CanAdopt"] = "true";
                }
            }
            else
            {
                jasEligibleForAdoption = false;
                this.Monitor.Log($"Jas is not eligible for adoption because {Game1.player.Name} is not married to Shane.", LogLevel.Debug);
            }
        }

        // Event to handle player interactions with NPCs
        private void OnPlayerWarped(object? sender, WarpedEventArgs? e)
        {
            var jas = Game1.getCharacterFromName("Jas");
            if (jas != null && jas.modData.TryGetValue("Adopt_Jas/CanAdopt", out string canAdopt) && canAdopt == "true")
            {
                // Check if the player is interacting with Jas and she can be adopted
                if (e.NewLocation.characters.Contains(jas))
                {
                    // Modify Jas's dialogue to reflect adoption availability
                    jas.CurrentDialogue.Push(new Dialogue("Would you like to adopt me?", jas));
                }
            }
        }
    }
}
