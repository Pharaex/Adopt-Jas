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
            try
            {
                //Attempt to update Jas's eligibility for adoption when a new day starts
                UpdateJasEligibility();
            }
            catch (Exception ex)
            {
                //Log any exceptions if try is not successful
                this.Monitor.Log($"An error occurred while updating Jas's eligibility: {ex.Message}", LogLevel.Error);
            }

            //Update Jas's eligibility for adoption
            void UpdateJasEligibility()
            {
                //Check if the player is married to Shane
                if (Game1.player.spouse == "Shane")
                {
                    // Set Jas's eligibility for marriage (adoption) to true if married to Shane
                    jasEligibleForMarriage = true; //Set Jas's eligibility for adoption to true
                    this.Monitor.Log($"Jas is eligible for adoption because {Game1.player.Name} is married to Shane.", LogLevel.Debug);

                    // Call EnableJasAdoption
                    EnableJasAdoption();
                }
                else
                {
                    jasEligibleForMarriage = false; //False if not married to Shane
                    this.Monitor.Log($"Jas is not eligible for adoption because {Game1.player.Name} is not married to Shane.", LogLevel.Debug);
                }
            }

            //Method to enable Jas's adoption option
            void EnableJasAdoption()
            {
                // Rename her marriage option to "Adopt"
                var jas = Game1.getCharacterFromName("Jas");
                if (jas != null)
                {
                    // Simulate the "adopt" action by enabling marriage for Jas
                    jas.canMarry = true; //Allow "marriage" to happen, treating it as adoption
                    this.Monitor.Log($"Jas can now be adopted.", LogLevel.Debug);
                }
                else
                {
                    this.Monitor.Log("Failed to find Jas.", LogLevel.Error); //Log error if Jas is not found
                }
            }
        }
    }
    }
}
