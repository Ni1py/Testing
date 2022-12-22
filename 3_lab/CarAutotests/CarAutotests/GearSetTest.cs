using Xunit;

namespace CarAutotests
{
    public class GearSetTest
    {
        [Fact]
        public void The_speed_will_not_set_if_engine_not_turn_on() //передачу нельзя переключить так как двигатель не включен
        {
            //Arrange
            Car car = new Car();

            //Act
            bool isSeGear = car.SetGear(1);

            //Assert
            Assert.False(isSeGear);
            Assert.False(car.IsTurnedOn());
        }

        [Fact]
        public void The_gear_will_not_set_when_the_shifted_gear_does_not_fit() //передача не переключится так как такой передачи нет
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isSetGear = car.SetGear(10);

            //Assert
            Assert.False(isSetGear);
            Assert.Equal(0, car.GetGear());
        }

        [Fact]
        public void Gear_will_not_shift_to_reverse_after_accelerating_backwards_and_going_to_0_gear() //передача не переключится на заднюю после разгона назад и установки 0 передачи
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);
            car.SetSpeed(10);
            car.SetGear(0);

            //Act
            bool isSetGear = car.SetGear(-1);

            //Assert
            Assert.False(isSetGear);
            Assert.Equal(0, car.GetGear());
            Assert.Equal(Car.Direction.back, car.GetDirection());
        }

        [Fact]
        public void Transmission_and_direction_to_shift_to_reverse_after_starting_the_engine_and_accelerating() //передача и на направление переключаться на задние из нейтралки и последующего разгона
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isSetGear = car.SetGear(-1);
            bool isSetSpeed = car.SetSpeed(10);

            //Assert
            Assert.True(isSetGear);
            Assert.True(isSetSpeed);
            Assert.Equal(-1, car.GetGear());
            Assert.Equal(Car.Direction.back, car.GetDirection());
            Assert.Equal(10, car.GetSpeed());
        }

        [Fact]
        public void The_gear_shifts_to_2_when_the_minimum_possible_speed_is_reached() //передача переключится на 2 когда достигнута минимальная возможная скорость и направление будет вперед
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);

            //Act
            bool isSetGear = car.SetGear(2);

            //Assert
            Assert.True(isSetGear);
            Assert.Equal(2, car.GetGear());
            Assert.Equal(Car.Direction.forward, car.GetDirection());
            Assert.Equal(20, car.GetSpeed());
        }
    }
}
