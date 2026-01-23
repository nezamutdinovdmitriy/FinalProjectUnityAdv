namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameMode gameMode)
        {
            GameMode = gameMode;
        }

        public GameMode GameMode { get; }
    }
}