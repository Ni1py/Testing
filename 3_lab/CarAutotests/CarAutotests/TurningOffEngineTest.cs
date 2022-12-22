using Xunit;

namespace CarAutotests
{
    public class TurningOffEngineTest
    {
        [Fact]
        public void The_engine_will_not_turn_off_if_engine_is_turn_off() //двигатель не выключится так как он уже выключен
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.TurnOffEngine();

            //Act
            bool isTurnOff = car.TurnOffEngine();

            //Assert
            Assert.False(isTurnOff);
            Assert.True(!car.IsTurnedOn());
        }

        [Fact]
        public void The_engine_will_not_turn_off_if_the_speed_is_non_zero() //двигатель не выключится так как скорость не 0
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
        public void The_engine_will_not_turn_off_if_the_gear_is_non_zero() //двигатель не выключится так как передача не 0
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
