using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace Adopt_Jas
{
    internal sealed class ModEntry : Mod
    {
        // Flag to track if Jas is eligible for marriage (adoption)
        private bool jasEligibleForMarriage = false;

        public override void Entry(IModHelper helper)
        {
            // Event triggered when the day starts
            helper.Events.GameLoop.DayStarted += this.OnDayStarted;
        }

        private void OnDayStarted(object? sender, DayStartedEventArgs? e)
        {
            // Ensure the world is ready before performing actions
            if (!Context.IsWorldReady)
                return;

            // Check if the player is married to Shane
            if (!string.IsNullOrEmpty(Game1.player.spouse) && Game1.player.spouse == "Shane")
            {
                // Set Jas's eligibility for marriage (adoption) to true if married to Shane
                jasEligibleForMarriage = true;
                this.Monitor.Log($"Jas is eligible for adoption because {Game1.player.Name} is married to Shane.", LogLevel.Debug);

                // Allow Jas to be "married" (adopted)
                if (jasEligibleForMarriage)
                {
                    // Rename her marriage option to "Adopt"
                    var jas = Game1.getCharacterFromName("Jas");
                    if (jas != null)
                    {
                        // Simulate the "adopt" action by enabling marriage for Jas
                        jas.canMarry = true; // Allow "marriage" to happen, but we will treat it as adoption
                        this.Monitor.Log($"Jas can now be adopted.", LogLevel.Debug);
                    }
                }
            }
            else
            {
                jasEligibleForMarriage = false;
                this.Monitor.Log($"Jas is not eligible for adoption because {Game1.player.Name} is not married to Shane.", LogLevel.Debug);
            }
        }
    }
}
