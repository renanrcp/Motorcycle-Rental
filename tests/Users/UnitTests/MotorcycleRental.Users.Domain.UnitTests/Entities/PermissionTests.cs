using MotorcycleRental.Users.Domain.Entities;

namespace MotorcycleRental.Users.Domain.UnitTests.Entities;

public class PermissionTests
{
    [Fact]
    public void ToEnum_ShouldReturnPermissionType()
    {
        // Arrange
        var expectedPermissionType = PermissionType.All;
        var permission = new Permission(Enum.GetName(expectedPermissionType)!);

        // Act
        var permissionType = permission.ToEnum();

        // Assert
        Assert.Equal(expectedPermissionType, permissionType);
    }

    [Fact]
    public void Create_ShouldReturnPermission()
    {
        // Arrange
        var expectedName = Enum.GetName(PermissionType.All)!;

        // Act
        var permissionResult = Permission.Create(expectedName);

        // Assert
        Assert.True(permissionResult.IsSuccess);
        Assert.Equal(expectedName, permissionResult.Value!.Name);
    }
}
