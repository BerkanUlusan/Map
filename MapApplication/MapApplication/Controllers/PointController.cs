using MapApplication.Services;
using MapApplication.Services.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MapApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointRepository _pointrepository;
        public PointController(IPointRepository pointrepository)
        {
            _pointrepository = pointrepository;
        }

        [HttpGet]
        public ApiResponse<List<Point>> GetAll()
        {
            var response = _pointrepository.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public ApiResponse<Point> GetById(int id)
        {
            var response = _pointrepository.GetById(id);
            return response;
        }

        [HttpPut("{id}")]
        public ApiResponse Update(int id, Point point)
        {
            var response = _pointrepository.Update(id, point);
            return response;
        }

        [HttpDelete("Delete/{id}")]
        public ApiResponse<Point> Delete(int id)
        {
            var response = _pointrepository.Delete(id);
            return response;
        }

        [HttpPost]
        public ApiResponse<Point> Add(Point point)
        {
            var response = _pointrepository.Add(point);
            return response;
        }
    }
}
