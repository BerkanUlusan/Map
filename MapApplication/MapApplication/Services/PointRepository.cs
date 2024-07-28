using MapApplication.Data;
using MapApplication.Services.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapApplication.Services
{
    public class PointRepository : IPointRepository
    {
        private readonly MapApplicationDbContext _context;

        public PointRepository(MapApplicationDbContext context)
        {
            _context = context;
        }

        public ApiResponse<List<Point>> GetAll()
        {
            try
            {
                var points = _context.Points.ToList();
                var response = new ApiResponse<List<Point>>(true, "Points retrieved successfully", points);
                return response;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                var errorResponse = new ApiResponse<List<Point>>(false, "Unexpected Error", null);
                return errorResponse;
            }
        }

        public ApiResponse<Point> GetById(int Id)
        {
            try
            {
                if (Id <= 0)
                {
                    var badResponse = new ApiResponse<Point>(false, "Invalid Id!", null);
                    return badResponse;
                }
                var point = _context.Points.FirstOrDefault(p => p.Id == Id);
                if (point == null)
                {
                    var notFoundResponse = new ApiResponse<Point>(false, "Not Found", null);
                    return notFoundResponse;
                }
                var response = new ApiResponse<Point>(true, "Point retrieved successfully", point);
                return response;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                var errorResponse = new ApiResponse<Point>(false, "Unexpected Error", null);
                return errorResponse;
            }
        }

        public ApiResponse<Point> Update(int id, Point point)
        {
            try
            {
                if (point == null || id != point.Id)
                {
                    var badResponse = new ApiResponse<Point>(false, "Invalid Id!", null);
                    return badResponse;
                }
                Point pointFromDb = _context.Points.FirstOrDefault(x => x.Id == id);
                if (pointFromDb == null)
                {
                    var notFoundResponse = new ApiResponse<Point>(false, "Not Found", null);
                    return notFoundResponse;
                }
                pointFromDb.X = point.X;
                pointFromDb.Y = point.Y;
                pointFromDb.Name = point.Name;
                _context.SaveChanges();
                var response = new ApiResponse<Point>(true, "Point updated successfully", pointFromDb);
                return response;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                var errorResponse = new ApiResponse<Point>(false, "Unexpected Error", null);
                return errorResponse;
            }
        }

        public ApiResponse<Point> Delete(int id)
        {
            try
            {
                var item = _context.Points.FirstOrDefault(u => u.Id == id);
                if (item == null)
                {
                    var notFoundResponse = new ApiResponse<Point>(false, "Not Found", null);
                    return notFoundResponse;
                }
                _context.Points.Remove(item);
                _context.SaveChanges();
                var response = new ApiResponse<Point>(true, "Point deleted successfully", item);
                return response;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                var errorResponse = new ApiResponse<Point>(false, "Unexpected Error", null);
                return errorResponse;
            }
        }

        public ApiResponse<Point> Add(Point point)
        {
            try
            {
                if (_context.Points.FirstOrDefault(x => x.Name.ToLower() == point.Name.ToLower()) != null)
                {
                    return new ApiResponse<Point>(false, "Point already exists!", point);
                }
                point.Id = _context.Points.OrderByDescending(x => x.Id).FirstOrDefault()?.Id + 1 ?? 1;
                _context.Points.Add(point);
                _context.SaveChanges();
                var response = new ApiResponse<Point>(true, "Point added successfully", point);
                return response;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                var errorResponse = new ApiResponse<Point>(false, $"Unexpected Error: {ex.Message}", null);
                return errorResponse;
            }
        }
    }
}
