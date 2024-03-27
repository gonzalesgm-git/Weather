using Moq;
using Weather.Dto;
using Weather.Services;

namespace Weather.Tests
{
    public class RainfallServiceTests
    {
        private readonly Mock<IRainfallService> _rainfallService;

        public RainfallServiceTests()
        {
            _rainfallService = new Mock<IRainfallService>();
        }

        [Fact]
        public async Task Get_RainfallData_Tests()
        {
            _rainfallService.Setup(q => q.GetRainfallData(It.IsAny<string>()))
                 .ReturnsAsync(new BaseModel
                 {
                     Items = new List<Item>() { 
                         new Item { Id = "1", Label = "Item1", Measure = "Measure1", Station="1" },
                         new Item { Id = "2", Label = "Item2", Measure = "Measure2", Station="1" },
                         new Item { Id = "3", Label = "Item3", Measure = "Measure3", Station="1" }
                     },
                     Limit = 1,
                     Meta = new Meta { Comment = "Comment1", Documentation="Documentation1", Publisher="Publisher1"},


                 });

            var result = await _rainfallService.Object.GetRainfallData("1");

            Assert.Equal(3, result.Items.Count());

        }
    }
}