using HeThongDatBan.Controllers;
using HeThongDatBan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HeThongDatBan.Services
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        //API thêm ảnh
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Vui lòng chọn một tệp ảnh!");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Chỉ cho phép tải lên tệp JPG, JPEG hoặc PNG!");
            }

            // Lấy thư mục wwwroot/Uploads
            var uploadPath = Path.Combine(_environment.WebRootPath, "Uploads");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Lấy tên file và tạo đường dẫn lưu
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //gọi hàm lưu csdl

            // Trả về URL ảnh
            var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{fileName}";
            return Ok(new { message = "Tải ảnh lên thành công!", fileUrl });
        }
        //API xóa ảnh
        [HttpDelete("image")]
        public IActionResult DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "Uploads", fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Ảnh không tồn tại!");

            System.IO.File.Delete(filePath);
            return Ok(new { message = "Ảnh đã được xóa!" });
        }

        //THÊM NHIỀU ẢNH
        [HttpPost("multiple-images")]
        public async Task<IActionResult> UploadMultipleImages(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("Vui lòng chọn ít nhất một tệp ảnh!");

            var uploadPath = Path.Combine(_environment.WebRootPath, "Uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var uploadedFiles = new List<string>();

            foreach (var file in files)
            {
                // Giới hạn định dạng ảnh (JPG, PNG, JPEG)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest($"Tệp {file.FileName} không hợp lệ! Chỉ cho phép JPG, JPEG hoặc PNG.");
                }

                // Đổi tên file để tránh trùng lặp
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadPath, fileName);

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Thêm đường dẫn file vào danh sách kết quả
                var fileUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{fileName}";
                uploadedFiles.Add(fileUrl);
            }

            return Ok(new { message = "Tải ảnh lên thành công!", fileUrls = uploadedFiles });
        }
        //XÓA NHIỀU ẢNH
        [HttpDelete("multiple-images")]
        public IActionResult DeleteMultipleImages([FromBody] List<string> fileNames)
        {
            if (fileNames == null || fileNames.Count == 0)
                return BadRequest("Vui lòng chọn ít nhất một ảnh để xóa!");

            var deletedFiles = new List<string>();

            foreach (var fileName in fileNames)
            {
                var filePath = Path.Combine(_environment.WebRootPath, "Uploads", fileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    deletedFiles.Add(fileName);
                }
            }

            return Ok(new { message = "Xóa ảnh thành công!", deletedFiles });
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> DangKyNhaHang([FromForm] DangKyNhaHangRequest model)
        {
            // Lấy UserId từ token JWT
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            // Upload ảnh nhà hàng
            //if(model.HinhAnhNhaHang != null)
            //{
            //    var restaurantImageIds = await UploadImagesAsync(model.HinhAnhNhaHang, 0);
            //    var menuImageIds = await UploadImagesAsync(model.HinhAnhThucDon, 1);
            //}


            // Upload ảnh thực đơn


            

            return Ok(new { message = "Đăng ký nhà hàng thành công!", model });
        }
    }
}
