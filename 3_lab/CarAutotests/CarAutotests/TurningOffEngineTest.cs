using Xunit;

namespace CarAutotests
{
    public class TurningOffEngineTest
    {
        //[Fact]
        //public void The_engine_will_not_turn_off_because_1_gear_is_on()
        //{
        //    //Arrange
        //    Car car = new Car();

        //    //Act
        //    car.TurnOnEngine();
        //    car.SetGear(1);

        //    //Assert
        //    Assert.False(car.TurnOffEngine());
        //}

        [Fact]
        public void The_engine_will_not_turn_off_if_the_speed_is_non_zero()
        {
            //Arrange
            Car car = new Car();

            //Act
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);
            car.SetGear(0);

            //Assert
            Assert.False(car.TurnOffEngine());
        }

        [Fact]
        public void The_engine_will_turn_off_if_the_speed_and_gear_are_zero()
        {
            //Arrange
            Car car = new Car();

            //Act
            car.TurnOnEngine();

            //Assert
            Assert.True(car.TurnOffEngine());
        }
    }
}
