namespace course_work {
    public interface IUnit
    {
        public byte getID();
    }

    public abstract class Mortal {
        public byte health { get; set; }
    }


    public class Player : Mortal, IUnit {
        private static byte ID = 4;
        private bool canWalk {get; set;}

        public Player () {this.health = 10; this.canWalk = true;}
        public byte getID() => ID;
        public string move(int dir) {
            if (!this.canWalk) {
                return "Game Over";
            }
            Grid g = gameManager.Instance.grid;
            IUnit cell = g.getCellDir(this, dir);
            cellType cellT = Grid.IUnit2cellType(cell);
            switch (cellT)
            {
                case cellType.Floor:
                    g.moveUnit(this, g.getUnitIndex(this) + g.dir2offset(dir));
                    return "";
                case cellType.Wall:
                    return "Не могу туда пойти!";
                case cellType.Warrior:
                    Mortal enemy = (Mortal)cell;
                    this.health -= enemy.health;
                    this.health = (this.health >= 200) ? (byte)0 : this.health;
                    
                    if (this.health == 0) {
                        this.canWalk = false;
                        gameManager.Instance.grid.removeUnit(this);
                        return "Game Over";
                    }
                    g.moveUnit(this, g.getUnitIndex(this) + g.dir2offset(dir));
                    return $"Игрок потерял 2 здоровья, но победил врага";
                default:
                    return "ОшибОчка";
            }
        }
        public override string ToString() => "@";
    }

    public class Warrior : Mortal, IUnit {
        private static byte ID = 2;
        public Warrior () {this.health = 2;}

        public byte getID() => ID;

        public override string ToString() => "!";
    }
    public class Floor : IUnit {
        private static byte ID = 0;
        public Floor () {}
        public byte getID() => ID;

        public override string ToString() => ".";
    }

    public class Wall : IUnit {
        private static byte ID = 1;

        public Wall () {}
        public byte getID() => ID;

        public override string ToString() => "#";
    }

}