using Xunit;

namespace CarAutotests
{
    public class TurningOffEngineTest
    {
        [Fact]
        public void The_engine_will_not_turn_off_if_the_speed_is_non_zero()
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);

            //Act
            bool isTurnOff = car.TurnOffEngine();

            //Assert
            Assert.False(isTurnOff);
        }

        [Fact]
        public void The_engine_will_not_turn_off_if_the_gear_is_non_zero()
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);

            //Act
            bool isTurnOff = car.TurnOffEngine();

            //Assert
            Assert.False(isTurnOff);
        }

        [Fact]
        public void The_engine_will_turn_off_when_the_motor_turns_on() //двигатель может выключиться после включения
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isTurnOff = car.TurnOffEngine();

            //Assert
            Assert.True(isTurnOff);
        }
    }
}
