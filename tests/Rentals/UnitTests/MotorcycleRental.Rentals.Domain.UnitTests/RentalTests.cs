using MotorcycleRental.Rentals.Domain.Entities;
using MotorcycleRental.Rentals.Domain.Errors;

namespace MotorcycleRental.Rentals.Domain.UnitTests;

public class RentalTests
{
    [Theory]
    [InlineData([7])]
    [InlineData([15])]
    [InlineData([30])]
    [InlineData([45])]
    [InlineData([50])]
    public void Create_ShouldReturnRental_WhenRentalTypeExists(int days)
    {
        // Arrange
        var rental = RentalType.Create(days, 0).Value!;
        var expectedStartDate = DateTime.Now.AddDays(1).Date;
        var expectedEndDate = expectedStartDate.AddDays(days).Date;

        // Act
        var result = Rental.Create(rental, 1, 1);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(1, result.Value.MotorcycleId);
        Assert.Equal(1, result.Value.DelivererId);
        Assert.Equal(expectedStartDate, result.Value.StartDate);
        Assert.Equal(expectedEndDate, result.Value.ExpectedEndDate);
    }

    [Fact]
    public void Create_ShouldReturnError_WhenRentalIsNull()
    {
        // Arrange
        RentalType rentalType = null!;

        // Act
        var result = Rental.Create(rentalType, 1, 1);

        // Assert
        Assert.True(result.IsFaulted);
        Assert.Equal(RentalErrors.RentalTypeNotExists, result.Error);
    }

    [Fact]
    public void SetEndDate_ShouldSetEndDateDate()
    {
        // Arrange
        var rentalType = RentalType.Create(7, 30).Value!;
        var rental = Rental.Create(rentalType, 1, 1).Value!;
        var endDate = rental.ExpectedEndDate.AddDays(-2).AddHours(2);
        var expectedEndDate = rental.ExpectedEndDate.AddDays(-2).Date;

        // Act
        var result = rental.SetEndDate(endDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedEndDate, rental.EndDate);
    }

    [Fact]
    public void SetEndDate_ShouldReturnErrorIfDateLowerThanStartDate()
    {
        // Arrange
        var rentalType = RentalType.Create(7, 30).Value!;
        var rental = Rental.Create(rentalType, 1, 1).Value!;
        var endDate = rental.StartDate.AddDays(-2);

        // Act
        var result = rental.SetEndDate(endDate);

        // Assert
        Assert.True(result.IsFaulted);
        Assert.Equal(RentalErrors.EndDateLowerThanStartDate, result.Error!);
    }

    [Theory]
    [InlineData([7, 30, 210])]
    [InlineData([15, 28, 420])]
    [InlineData([30, 22, 660])]
    [InlineData([45, 20, 900])]
    [InlineData([50, 18, 900])]
    public void CalculateCost_ShouldReturnTotalRentalTypeCostWhenDoesntHaveEndDate(int days, decimal costPerDay, decimal expectedCost)
    {
        // Arrange
        var rentalType = RentalType.Create(days, costPerDay).Value!;
        var rental = Rental.Create(rentalType, 1, 1).Value!;
        rental.SetRentalType(rentalType);

        // Act
        var cost = rental.CalculateCost();

        // Assert
        Assert.True(cost.IsSuccess);
        Assert.Equal(expectedCost, cost.Value);
    }

    [Theory]
    [InlineData([7, 30, 210])]
    [InlineData([15, 28, 420])]
    [InlineData([30, 22, 660])]
    [InlineData([45, 20, 900])]
    [InlineData([50, 18, 900])]
    public void CalculateCost_ShouldReturnTotalRentalTypeCostEndDateIsEqualsToExpectedEndDate(int days, decimal costPerDay, decimal expectedCost)
    {
        // Arrange
        var rentalType = RentalType.Create(days, costPerDay).Value!;
        var rental = Rental.Create(rentalType, 1, 1).Value!;
        rental.SetRentalType(rentalType);

        rental.SetEndDate(DateTime.Now.AddDays(1).Date.AddDays(days));

        // Act
        var cost = rental.CalculateCost();

        // Assert
        Assert.True(cost.IsSuccess);
        Assert.Equal(expectedCost, cost.Value);
    }

    [Theory]
    [InlineData([7, 30, 6, 216])]
    [InlineData([15, 28, 13, 442.4])]
    [InlineData([30, 22, 27, 686.4])]
    [InlineData([45, 20, 40, 940])]
    [InlineData([50, 18, 43, 950.4])]
    public void CalculateCost_ShouldReturnTotalCostWithPackageFault_WhenEndDateIsLowerThanExpectedEndDate(int days, decimal costPerDay, int totalDays, decimal expectedCost)
    {
        // Arrange
        var rentalType = RentalType.Create(days, costPerDay).Value!;
        var rental = Rental.Create(rentalType, 1, 1).Value!;
        rental.SetRentalType(rentalType);

        rental.SetEndDate(DateTime.Now.AddDays(1).Date.AddDays(totalDays));

        // Act
        var cost = rental.CalculateCost();

        // Assert
        Assert.True(cost.IsSuccess);
        Assert.Equal(expectedCost, cost.Value);
    }

    [Theory]
    [InlineData([7, 30, 8, 260])]
    [InlineData([15, 28, 17, 520])]
    [InlineData([30, 22, 35, 910])]
    [InlineData([45, 20, 47, 1000])]
    [InlineData([50, 18, 60, 1400])]
    public void CalculateCost_ShouldReturnTotalCostWith50Fault_WhenEndDateIsHigherThanExpectedEndDate(int days, decimal costPerDay, int totalDays, decimal expectedCost)
    {
        // Arrange
        var rentalType = RentalType.Create(days, costPerDay).Value!;
        var rental = Rental.Create(rentalType, 1, 1).Value!;
        rental.SetRentalType(rentalType);

        rental.SetEndDate(DateTime.Now.AddDays(1).Date.AddDays(totalDays));

        // Act
        var cost = rental.CalculateCost();

        // Assert
        Assert.True(cost.IsSuccess);
        Assert.Equal(expectedCost, cost.Value);
    }
}
