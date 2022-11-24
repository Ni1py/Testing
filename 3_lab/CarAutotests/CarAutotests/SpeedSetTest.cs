using Xunit;

namespace CarAutotests
{
    public class SpeedSetTest
    {
        [Fact]
        public void The_speed_will_not_set_if_installed_gear_is_0() //скорость нельзя переключить на 0 передаче
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isSetSpeed = car.SetSpeed(10);

            //Assert
            Assert.False(isSetSpeed);
            Assert.Equal(0, car.GetSpeed());
        }

        [Fact]
        public void The_speed_will_not_set_when_speed_is_negative() //скорость нельзя переключить так как отрицательная
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isSetSpeed = car.SetSpeed(-30);

            //Assert
            Assert.False(isSetSpeed);
            Assert.Equal(0, car.GetSpeed());
        }

        [Fact]
        public void The_speed_will_be_set_as_the_maximum_for_2nd_gear() //скорость установится так как максимальная для 2 передачи и направление вперед
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);
            car.SetGear(2);

            //Act
            bool isSetSpeed = car.SetSpeed(50);

            //Assert
            Assert.True(isSetSpeed);
            Assert.Equal(2, car.GetGear());
            Assert.Equal(50, car.GetSpeed());
            Assert.Equal(Car.Direction.forward, car.GetDirection());
        }

        [Fact]
        public void The_speed_will_be_set_as_the_maximum_for_back_gear() //скорость установится так как максимальная для задней передачи и направление назад
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);

            //Act
            bool isSetSpeed = car.SetSpeed(20); ;

            //Assert
            Assert.True(isSetSpeed);
            Assert.Equal(20, car.GetSpeed());
            Assert.Equal(Car.Direction.back, car.GetDirection());
            Assert.Equal(-1, car.GetGear());
        }

        [Fact]
        public void The_speed_will_not_be_set_because_it_exceeds_the_limit_for_5th_gear() //скорость не установится так как превышает предел для 5 передачи
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(30);
            car.SetGear(4);
            car.SetSpeed(50);
            car.SetGear(5);

            //Act
            bool isSetSpeed = car.SetSpeed(151); ;

            //Assert
            Assert.False(isSetSpeed);
            Assert.Equal(50, car.GetSpeed());
            Assert.Equal(Car.Direction.forward, car.GetDirection());
            Assert.Equal(5, car.GetGear());
        }
    }
}
