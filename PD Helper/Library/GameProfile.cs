using Memory;

namespace PD_Helper.Library
{
    /// <summary>
    /// A singleton class that represents the loaded Phantom Dust game profile and its associated arsenals.
    /// </summary>
    /// <remarks>
    /// This also contains a memory handle for reading from and writing to the Phantom Dust game.
    /// </remarks>
    internal sealed class GameProfile
    {
        private static readonly Lazy<GameProfile> lazy = new Lazy<GameProfile>(() => new GameProfile());

        public static GameProfile Instance { get { return lazy.Value; } }

        private GameProfile()
        {
            var gameService = new GameService();
            gameService.LoadGameProfile(this);
        }

        /// <summary>
        /// The memory handle used to read from and write to the Phantom Dust game
        /// </summary>
        public Mem Mem { get; } = new Mem();

        /// <summary>
        /// The process id of the loaded Phantom Dust game
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// List of arsenal names associated with the loaded game profile
        /// </summary>
        public List<string> Arsenals { get; set; } = new List<string>();
    }
}
