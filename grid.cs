using System.Collections.Generic;

namespace course_work {
    public enum cellType : byte {
        Floor = 0,
        Wall = 1,
        Player = 4,
        Warrior = 2
    }

    public class Grid {
        private List<IUnit> board {get; set;}
        public int width {get; set;}
        public int height {get; set;}
        public Grid(Grid copy) {
            this.width = copy.width;
            this.height = copy.height;
            this.board = new List<IUnit>(copy.board);
        }

        public Grid(byte[] map, int width, int height) {
            this.board = new List<IUnit>();
            this.width = width;
            this.height = height;
            foreach (byte item in map)
            {
                IUnit newUnit;
                switch (item) 
                {
                    case 0 or 1:
                        newUnit = gameManager.Instance.BaseUnitFactory.createUnit((cellType)item);
                        break;
                    case 2 or 4:
                        newUnit = gameManager.Instance.MortalUnitFactory.createUnit((cellType)item);
                        if (newUnit as Player != null) {
                            gameManager.Instance.player = (Player)newUnit;
                        }
                        break;
                    default:
                        newUnit = null;
                        break;
                }
                this.board.Add(newUnit);
            }
        }
        public void draw() {
            for (int i = 0; i < this.height; i++) {
                for (int j = 0; j < this.width; j++){
                    System.Console.Write($"{this.board[Grid.oneDIndex(i, j, this.width)]} ");
                }
                System.Console.WriteLine();
            }
        }
        public IUnit getCellDir(IUnit unit, int dir) {
            int index = this.getUnitIndex(unit);
            IUnit r;
            switch (dir)
            {
                case 1:
                    r = this.board[index-this.width];
                    break;
                case 2:
                    r = this.board[index+1];
                    break;
                case 3:
                    r = this.board[index+this.width];
                    break;
                case 4:
                    r = this.board[index-1];
                    break;
                default:
                    r = new Wall();
                    break;
            };
            return r;
        }
        public void moveUnit(IUnit unit, int i) {
            int index = this.getUnitIndex(unit);
            this.board[index] = new Floor();
            this.board[i] = unit;
        }
        public void addUnit(IUnit unit, int i) => this.board[i] = unit;
        public void removeUnit(IUnit unit) {
            int i = this.board.IndexOf(unit);
            if (i != -1) {
                this.board[i] = new Floor();
            }
        }
        public int dir2offset(int dir) {
            int offset = 0;
            switch (dir)
            { 
                case 1:
                    offset -= this.width;
                    break;
                case 2:
                    offset += 1;
                    break;
                case 3:
                    offset += this.width;
                    break;
                case 4:
                    offset -= 1;
                    break;
                default:
                    break;
            };
            return offset;
        }
        public static int oneDIndex(int x, int y, int w) => (x * w) + y;
        public static cellType IUnit2cellType(IUnit unit) => (cellType)unit.getID();
        public int getUnitIndex(IUnit unit) => this.board.IndexOf(unit);
        public IEnumerable<IUnit> Units
        {
            get {
                for (int i = 0; i < this.board.Count; i++) yield return this.board[i];
            }
        }
        private IEnumerator<IUnit> GetEnumerator()
        {
            for (int i = 0; i < this.board.Count; i++) yield return this.board[i];
        }
    }

}