using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace Adopt_Jas
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // event += method to call
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the save file is loaded.</summary>
        /// <param name="sender">The event sender (nullable).</param>
        /// <param name="e">The event data (nullable).</param>
        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs? e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            if (!string.IsNullOrEmpty(Game1.player.spouse))
            {
                this.Monitor.Log($"{Game1.player.Name} is married to {Game1.player.spouse}", LogLevel.Debug);
            }
        }
    }
}
