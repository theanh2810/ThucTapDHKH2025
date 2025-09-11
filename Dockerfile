#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Sử dụng image ASP.NET Core runtime 8.0 làm base cho container chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Chạy container với user 'app' (tăng bảo mật, tránh chạy bằng root)
USER app

# Đặt thư mục làm việc mặc định trong container là /app
WORKDIR /app

# Mở cổng 8080 để ứng dụng lắng nghe (phù hợp với cấu hình ASP.NET Core mặc định trên Docker)
EXPOSE 8080

# Sử dụng image .NET SDK 8.0 để build ứng dụng (có đầy đủ công cụ build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Đặt biến môi trường cho cấu hình build (mặc định là Release)
ARG BUILD_CONFIGURATION=Release

# Đặt thư mục làm việc cho quá trình build là /src
WORKDIR /src

# Copy file csproj vào container để thực hiện restore trước (tối ưu cache)
COPY ["HeThongDatBan.csproj", "."]

# Khôi phục các package NuGet cho project
RUN dotnet restore "./HeThongDatBan.csproj"

# Copy toàn bộ mã nguồn vào container
COPY . .

# Đặt lại thư mục làm việc (có thể không cần thiết, nhưng đảm bảo đúng context)
WORKDIR "/src/."

# Build project với cấu hình đã chọn, output ra /app/build
RUN dotnet build "./HeThongDatBan.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Tạo stage publish từ kết quả build
FROM build AS publish

# Đặt lại biến cấu hình build (nếu cần)
ARG BUILD_CONFIGURATION=Release

# Publish project (tối ưu để chạy production), output ra /app/publish, không tạo file exe host
RUN dotnet publish "./HeThongDatBan.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Tạo stage cuối cùng từ base (runtime)
FROM base AS final

# Đặt thư mục làm việc là /app
WORKDIR /app

# Copy toàn bộ file đã publish từ stage publish vào container
COPY --from=publish /app/publish .

# Lệnh mặc định khi container khởi động: chạy ứng dụng .NET
ENTRYPOINT ["dotnet", "HeThongDatBan.dll"]