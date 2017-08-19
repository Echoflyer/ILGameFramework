namespace ILFramework
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {
        public static ILComponent _ILRuntime
        {
            get;
            private set;
        }
        
        private static void InitCustomComponents()
        {
            _ILRuntime = UnityGameFramework.Runtime.GameEntry.GetComponent<ILComponent>();
        }
    }
}
