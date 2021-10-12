namespace course_work{
    public sealed class gameManager {
        private static gameManager _instance;
        private gameManager() {}
        public static gameManager Instance {
            get {
                if (_instance == null) {
                    _instance = new gameManager();
                }
                return _instance;
            }
        }
        public Grid grid {get; set;}
        public Player player {get; set;}
        public BaseFactory BaseUnitFactory {get; set;}
        public MortalFactory MortalUnitFactory {get; set;}
    }
}
