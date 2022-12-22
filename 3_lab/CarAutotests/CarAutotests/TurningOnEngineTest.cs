using Xunit;

namespace CarAutotests
{
    public class TurningOnEngineTest
    {
        [Fact]
        public void The_engine_will_not_turn_on_if_engine_is_turn_on() //двигатель не включится так как он уже включен
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isTurnOff = car.TurnOnEngine();

            //Assert
            Assert.False(isTurnOff);
            Assert.True(car.IsTurnedOn());
        }
    }
}
