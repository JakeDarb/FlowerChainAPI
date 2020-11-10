using Xunit;

public class testclass{

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    public void theoryOdd(int number){
        Assert.True(Program.isOdd(number));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    [InlineData(6)]
    public void theoryEven(int number2){
        Assert.True(Program.isEven(number2));
    }
   
   [Fact]
    public void passingAddTest(){

        Assert.Equal(4, Program.Add(2,2));

    }

    [Fact]
    public void failingAddTest(){
        Assert.NotEqual(5, Program.Add(2,2));
    }

    [Fact]

    public void passingMinusTest(){
        Assert.Equal(3, Program.Minus(10,7));
    }

    [Fact]

    public void failingMinusTest(){
        Assert.NotEqual(5, Program.Minus(20,6));
    }
}