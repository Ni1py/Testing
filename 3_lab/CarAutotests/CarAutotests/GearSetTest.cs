using Xunit;

namespace CarAutotests
{
    public class GearSetTest
    {
        //[Fact]
        //public void The_gear_will_not_set_to_2_if_the_engine_is_on_and_the_installed_gear_is_1_because_the_speed_is_not_dialed()
        //{
        //    //Arrange
        //    Car car = new Car();
        //    car.TurnOnEngine();
        //    car.SetGear(1);

        //    //Act
        //    bool isSetGear = car.SetGear(2);

        //    //Assert
        //    Assert.False(isSetGear);
        //    Assert.Equal(1, car.GetGear());
        //    Assert.Equal(Car.Direction.standing, car.GetDirection());
        //}

        [Fact]
        public void The_gear_will_not_set_if_the_engine_is_on_and_the_switchable_gear_is_10_because_there_is_no_such_gear()
        {
            //Arrange
            Car car = new Car();
            car.TurnOnEngine();

            //Act
            bool isSetGear = car.SetGear(10);

            //Assert
            Assert.False(isSetGear);
        }

        [Fact]
        public void The_gear_will_not_set_to_back_but_direction_will_set_to_back_if_the_engine_is_on_and_the_installed_gear_is_0_and_installed_speed_is_non_zero()
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

        //[Fact]
        //public void The_gear_will_not_set_to_1_and_direction_will_set_to_back_if_the_engine_is_on_and_the_installed_gear_is_back_and_installed_speed_is_non_zero()
        //{
        //    //Arrange
        //    Car car = new Car();
        //    car.TurnOnEngine();
        //    car.SetGear(-1);
        //    car.SetSpeed(10);

        //    //Act
        //    bool isSetGear = car.SetGear(1);

        //    //Assert
        //    Assert.False(isSetGear);
        //    Assert.Equal(-1, car.GetGear());
        //    Assert.Equal(Car.Direction.back, car.GetDirection());
        //}
    }
}
