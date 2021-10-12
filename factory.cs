namespace course_work
{
    public abstract class UnitFactory {
        public IUnit GetUnit(cellType UnitT) => createUnit(UnitT);
        public abstract IUnit createUnit(cellType UnitT);
    }

    public class BaseFactory : UnitFactory {
        public override IUnit createUnit(cellType UnitT) {
            switch (UnitT)
                {
                    case cellType.Floor: 
                        return new Floor();
                    case cellType.Wall:
                        return new Wall();
                    default:
                        System.Console.WriteLine("Can't create type " + UnitT);
                        return null;
                }
        }
    }
    public class MortalFactory : UnitFactory {
        public override IUnit createUnit(cellType UnitT) {
            switch (UnitT)
                {
                    case cellType.Warrior: 
                        return new Warrior();
                    case cellType.Player:
                        return new Player();
                    default:
                        System.Console.WriteLine("Can't create type " + UnitT);
                        return null;
                }
        }
    }
}