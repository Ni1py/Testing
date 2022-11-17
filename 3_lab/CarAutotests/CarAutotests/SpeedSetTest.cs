using Xunit;

namespace CarAutotests
{
    public class SpeedSetTest
    {
        private static Car _car;

        [Fact]
        public void InitializeTheCarAndTurnOnTheEngine()
        {
            _car = new Car();
            _car.TurnOnEngine();
        }

        [Fact]
        public void The_speed_will_not_set_if_the_engine_is_on_and_installed_speed_is_zero_because_it_is_impossible_to_increase_the_speed_at_0_gear()
        {
            //Act
            bool isSetSpeed = _car.SetSpeed(10);

            //Assert
            Assert.False(isSetSpeed);
            Assert.Equal(0, _car.GetSpeed());
        }

        [Fact]
        public void The_speed_will_not_set_if_the_engine_is_on_because_the_speed_is_negative()
        {
            //Act
            bool isSetSpeed = _car.SetSpeed(-30);

            //Assert
            Assert.False(isSetSpeed);
            Assert.Equal(0, _car.GetSpeed());
        }

        [Fact]
        public void The_speed_will_set_and_the_direction_is_forward_if_the_engine_is_on_and_the_gear_is_2_because_the_speed_is_max_for_1_gear()
        {
            //Arrange
            _car.SetGear(1);
            _car.SetSpeed(20);
            _car.SetGear(2);

            //Act
            bool isSetSpeed = _car.SetSpeed(50);

            //Assert
            Assert.True(isSetSpeed);
            Assert.Equal(50, _car.GetSpeed());
            Assert.Equal(Car.Direction.forward, _car.GetDirection());
        }
    }
}
